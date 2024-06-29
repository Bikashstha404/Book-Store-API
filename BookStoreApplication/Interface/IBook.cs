using BookStoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApplication.Interface
{
    public interface IBook
    {
        bool AddBook(Book book);

        List<Book> GetBooks();

        List<Book> GetBookByName(string name);

        List<Book> GetBookByPriceRange(int lowPrice, int highPrice);
    }
}
