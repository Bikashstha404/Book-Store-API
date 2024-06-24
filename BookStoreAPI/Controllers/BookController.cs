using BookStoreAPI.ViewModels;
using BookStoreApplication.Interface;
using BookStoreDomain;
using BookStoreInfrastructure.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _iBook;

        public BookController(IBook iBook)
        {
            _iBook = iBook;
        }

        [HttpPost("Add")]
        public IActionResult AddBook(AddBookViewModel addBookViewModel)
        {
            Book book = new Book
            {
                Name = addBookViewModel.Name,
                Price = addBookViewModel.Price,
                Author = addBookViewModel.Author,
                Genre = addBookViewModel.Genre,
            };
            _iBook.AddBook(book);
            return Ok();
        }

        [HttpGet("List")]
        public IActionResult ListBooks()
        {
            var books = _iBook.GetBooks();

            var bookList = new List<ListBookViewModel>();
            foreach (var book in books)
            {
                bookList.Add(new ListBookViewModel()
                {
                    Name = book.Name,
                    Author = book.Author,
                    Genre = book.Genre,
                    Price = book.Price
                });
            }
            return Ok(bookList);
        }
    }
}
