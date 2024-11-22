using BookStoreAPI.ViewModels;
using BookStoreDomain;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // Get all books
        [HttpGet]
        public IActionResult GetBooks(string fileName)
        {
            string filePath = GetFilePath(fileName + ".txt");
            List<Book> books = LoadBooksFromFile(filePath);
            return Ok(books);
        }

        // Get a book by Id
        [HttpGet("{id}")]
        public IActionResult GetBook(int id, string fileName)
        {
            string filePath = GetFilePath(fileName);
            List<Book> books = LoadBooksFromFile(filePath);
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            return Ok(book);
        }

        // Add a new book
        [HttpPost("Add")]
        public IActionResult AddBook([FromBody] AddBookViewModel addBookViewModel)
        {
            string filePath = GetFilePath(addBookViewModel.FileName + ".txt"); // Use the file name from the request
            List<Book> books = LoadBooksFromFile(filePath);

            // Determine the next available ID
            int nextId = books.Any() ? books.Max(b => b.Id) + 1 : 1;

            Book book = new Book
            {
                Id = nextId,
                Name = addBookViewModel.Name,
                Price = addBookViewModel.Price,
                Author = addBookViewModel.Author,
                Genre = addBookViewModel.Genre,
            };

            books.Add(book);

            // Save the updated list back to the file
            SaveBooksToFile(filePath, books);

            return Ok("Book added successfully");
        }

        // Update an existing book
        [HttpPut("Update/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] AddBookViewModel addBookViewModel)
        {
            string filePath = GetFilePath(addBookViewModel.FileName); // Use the file name from the request
            List<Book> books = LoadBooksFromFile(filePath);
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            // Update the book properties
            book.Name = addBookViewModel.Name;
            book.Price = addBookViewModel.Price;
            book.Author = addBookViewModel.Author;
            book.Genre = addBookViewModel.Genre;

            // Save the updated list back to the file
            SaveBooksToFile(filePath, books);

            return Ok("Book updated successfully");
        }

        // Delete a book
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteBook(int id, string fileName)
        {
            string filePath = GetFilePath(fileName); // Use the file name from the request
            List<Book> books = LoadBooksFromFile(filePath);
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            // Remove the book from the list
            books.Remove(book);

            // Save the updated list back to the file
            SaveBooksToFile(filePath, books);

            return Ok("Book deleted successfully");
        }

        // Helper method to get the file path based on the file name
        private string GetFilePath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), fileName);
        }

        // Helper method to load books from file
        private List<Book> LoadBooksFromFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                string existingData = System.IO.File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Book>>(existingData) ?? new List<Book>();
            }
            return new List<Book>();
        }

        // Helper method to save books to file
        private void SaveBooksToFile(string filePath, List<Book> books)
        {
            string jsonData = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filePath, jsonData);
        }
    }
}
