using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardService.Models;
using CreditCardService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly AuthService authService;
        public CreditCardController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("{userToken}/create")]
        public ActionResult<CreditCard> CreateCreditCard(string userToken, [FromBody] CreditCardDTO creditCardDTO)
        {
            var user = authService.CheckToken(userToken);

            if(user == null)
                return Conflict(new { message = $"Invalid user session token!"});

            if(creditCardDTO == null)
                return Conflict(new { message = $"Invalid credit card data!"});

            var creditCard = new CreditCard()
            {
                Number = creditCardDTO.CreditCardNumber,
                ExpirationDate = creditCardDTO.CreditCardExpirationDate,
                SecurityCode = creditCardDTO.CreditCardSecurityCode,
                CreditCardToken = Guid.NewGuid().ToString()
            };

            if(!Startup.Users.ContainsKey(user.Id))
            {
                user.CreditCards = new List<CreditCard>();
                Startup.Users.Add(user.Id, user);
            }
            Startup.Users[user.Id].CreditCards.Add(creditCard);

            return Ok(creditCard); 
        }

        [HttpPost("{userToken}/remove/{creditCardToken}")]
        public ActionResult RemoveCreditCard(string userToken, string creditCardToken)
        {
            var user = authService.CheckToken(userToken);

            if(user == null)
                return Conflict(new { message = $"Invalid user session token!"});

            var creditCard = Startup.Users[user.Id].CreditCards
                .Where(c => c.CreditCardToken.Equals(creditCardToken))
                .SingleOrDefault();

            if(creditCard == null)
                return Conflict(new { message = $"Invalid credit card data!"});

            Startup.Users[user.Id].CreditCards.Remove(creditCard);

            return Ok("Credit card removed"); 
        }

        [HttpPost("pay")]
        public ActionResult Pay([FromBody] PaymentDTO paymentDTO)
        {
            if(paymentDTO == null || !Startup.Users.ContainsKey(paymentDTO.UserId) ||
                !!Startup.Users[paymentDTO.UserId].CreditCards.Any(c => c.CreditCardToken.Equals(paymentDTO.CreditCardToken)))
                return Conflict(new { message = "Invalid payment data!"});

            var creditCard = Startup.Users[paymentDTO.UserId].CreditCards
                .Where(c => c.CreditCardToken.Equals(paymentDTO.CreditCardToken))
                .SingleOrDefault();

            if(paymentDTO.Value > creditCard.Limit)
                return Conflict(new { message = "Payment is bigger than credit card limit!"});

            return Ok();
        }
    }
}