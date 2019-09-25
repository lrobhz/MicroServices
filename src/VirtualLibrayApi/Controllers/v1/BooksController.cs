using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtualLibraryApi.Models;

namespace VirtualLibraryApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Startup.Books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(long id)
        {
            var book = Startup.Books.Where(b => b.BookId == id).SingleOrDefault();
            if(book != null)
                return Ok(book);

            return NoContent(); 
        }

        [HttpGet("ByName/{name}")]
        public ActionResult<IEnumerable<Book>> GetBookByName(string name)
        {
            var books = Startup.Books.Where(b => b.BookName.Equals(name)).ToList();

            if(books != null && books.Count > 0)
                return Ok(books);

            return NoContent();
        }

        [HttpGet("ByAuthor/{author}")]
        public ActionResult<IEnumerable<Book>> GetBookByAuthor(string author)
        {
            var books = Startup.Books.Where(b => b.Author.Equals(author)).ToList();

            if(books != null && books.Count > 0)
                return Ok(books);

            return NoContent();
        }

        [HttpPost("PostReview")]
        public ActionResult PostReview([FromBody] BookReview bookReview)
        {
            var book = Startup.Books.Where(b => b.BookId == bookReview.BookId).SingleOrDefault();
            if(book == null)
                return Conflict(new { message = $"No book with id '{bookReview.BookId}' was found."});

            Startup.BookReviews.Add(bookReview);

            return StatusCode(201);
        }

        [HttpGet("{bookId}/Reviews")]
        public ActionResult<IEnumerable<BookReview>> GetAllReviews(long bookId)
        {
            var result = Startup.BookReviews.Where(br => br.BookId == bookId).ToList();

            if(result != null && result.Count > 0)
                return result;

            return NoContent();
        }
    }
}