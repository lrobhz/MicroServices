using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using VirtualLibraryApi;
using VirtualLibraryApi.Models;
using VirtualLibraryApi.Auth;

namespace src.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class BasketController : ControllerBase
    {
        private readonly AuthService authService;
        public BasketController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet("{token}")]
        public ActionResult<Basket> ListBeskets(string token)
        {
            var user = authService.CheckToken(token);

            if(user == null)
            {
                return Conflict(new { message = $"Invalid user session token!"});
            }
            
            if(Startup.Baskets.ContainsKey(user.Id))
                return Startup.Baskets[user.Id];

            return NoContent();
        }

        [HttpGet("{token}/Books")]
        public ActionResult<IEnumerable<Book>> ListCartBooks(string token)
        {
            var user = authService.CheckToken(token);

            if(user == null)
            {
                return Conflict(new { message = $"Invalid user session token!"});
            }
            
            if(Startup.Baskets.ContainsKey(user.Id))
                return Startup.Baskets[user.Id].Books;

            return NoContent();
        }

        [HttpPost("{token}/AddBook/{bookId}")]
        public ActionResult AddBook(string token, long bookId)
        {
            var user = authService.CheckToken(token);

            if(user == null)
            {
                return Conflict(new { message = $"Invalid user session token!"});
            }

            if(!Startup.Baskets.ContainsKey(user.Id))
            {
                Startup.Baskets.Add(user.Id, new Basket() { BasketId = Startup.Baskets.Count() + 1, 
                     User = user, Books = new List<Book>() });
            }

            var book = Startup.Books.Where(b => b.BookId == bookId).SingleOrDefault();

            if(book == null)
                return Conflict(new { message = $"No book with id '{bookId}' was found."});

            Startup.Baskets[user.Id].Books.Add(book);

            return StatusCode(201); 
        }

        [HttpPost("{token}/RemoveBook/{bookId}")]
        public ActionResult RemoveBook(string token, long bookId)
        {
            var user = authService.CheckToken(token);

            if(user == null)
            {
                return Conflict(new { message = $"Invalid user session token!"});
            }

            if(!Startup.Baskets.ContainsKey(user.Id))
            {
                return Conflict(new { message = $"The user {user.Username} has no basket."});
            }

            var book = Startup.Baskets[user.Id].Books.Where(b => b.BookId == bookId).SingleOrDefault();

            if(book == null)
                return Conflict(new { message = $"No book with id '{bookId}' was found on {user.Username}'s basket."});

            Startup.Baskets[user.Id].Books.Remove(book);

            return Ok(); 
        }

        [HttpPost("{token}/ClearBasket")]
        public ActionResult ClearBasket(string token)
        {
            var user = authService.CheckToken(token);

            if(user == null)
            {
                return Conflict(new { message = $"Invalid user session token!"});
            }

            if(!Startup.Baskets.ContainsKey(user.Id))
            {
                return Conflict(new { message = $"The user {user.Username} has no basket."});
            }

            Startup.Baskets[user.Id].Books.Clear();

            return Ok();
        }
    }
}