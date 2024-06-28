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

        public List<Book> GetBookByName(string searchedName)
        {
            var books = new List<Book>();
            var searchedbooks = new List<Book>();
            books = _dbContext.Books.ToList();
            foreach(var book in books)
            {
                var bookName = book.Name;
                var result = true;
                for(var i = 0; i < searchedName.Length; i++)
                {
                    if (bookName[i] == searchedName[i])
                    {
                        continue;
                    }
                    result = false;
                    break;
                }
                if (result)
                {
                    searchedbooks.Add(book);
                }
            }
            return searchedbooks;
        }

        public List<Book> GetBooks()
        {
            var books = new List<Book>();
            books = _dbContext.Books.ToList();
            return books;
        }
    }
}
