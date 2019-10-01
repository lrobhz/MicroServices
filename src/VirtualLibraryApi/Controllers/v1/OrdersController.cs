using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VirtualLibraryApi;
using VirtualLibraryApi.Auth;
using VirtualLibraryApi.Payment;
using VirtualLibraryApi.Models;
using VirtualLibraryApi.Models.Enums;

namespace VirtualLibraryApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly CreditCardService creditCardService; 

        public OrdersController(AuthService authService, CreditCardService creditCardService)
        {
            this.authService = authService;
            this.creditCardService = creditCardService;
        }
        
        [HttpGet("{token}")]
        public ActionResult<IEnumerable<Order>> GetOrders(string token)
        {
            var user = authService.CheckToken(token);

            if(user == null)
                return Conflict(new { message = $"Invalid user session token!"});

            var orders = Startup.Orders.Where(o => o.User.Id.Equals(user.Id));

            if(orders == null || orders.Count() == 0)
                return NoContent();   

            return Ok(orders);
        }

        [HttpGet("{orderId}/status")]
        public ActionResult<OrderStatus> GetOrderStatus(int orderId)
        {
             if(!Startup.Orders.Any(o => o.OrderId == orderId))
                return Conflict(new { message = "Order not found"});

            var order = Startup.Orders.Where(o => o.OrderId == orderId).SingleOrDefault();

            return order.OrderStatus;
        }

        [HttpPost("Create/{token}/{basketId}")]
        public ActionResult Create(string token, int basketId)
        {
            var user = authService.CheckToken(token);

            if(user == null)
                return Conflict(new { message = $"Invalid user session token!"});

            if(!Startup.Baskets.ContainsKey(user.Id))
                return Conflict(new { message = "User has no basket"});

            if(Startup.Baskets[user.Id].BasketId != basketId)
                return Conflict(new { message = "Invalid Basket Id"});

            var order = new Order
            {
                OrderId = Startup.Orders.Count + 1,
                OrderDate = DateTime.Now,
                User = user,
                Basket = Startup.Baskets[user.Id],
                OrderStatus = OrderStatus.Created
            };

            return StatusCode(201); 
        }

        [HttpPost("Checkout/{orderId}")]
        public ActionResult Checkout(int orderId)
        {
            if(!Startup.Orders.Any(o => o.OrderId == orderId))
                return Conflict(new { message = "Order not found"});

            var order = Startup.Orders.Where(o => o.OrderId == orderId).SingleOrDefault();


            order.OrderStatus = OrderStatus.ReadyForShip;

            return Ok(); 
        }
    }
}