using BookStoreApplication.Interface;
using BookStoreDomain;
using BookStoreInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreInfrastructure.Implementation
{
    public class BookImplementation : IBook
    {
        private readonly BookDbContext _dbContext;

        public BookImplementation(BookDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public bool AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Book> GetBooks()
        {
            var books = new List<Book>();
            books = _dbContext.Books.ToList();
            return books;
        }
    }
}
