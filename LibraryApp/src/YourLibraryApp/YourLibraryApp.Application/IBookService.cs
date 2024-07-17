using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Application
{
    // IBookService.cs
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);

        Task<int> AddBooksAsync(IEnumerable<Book> books);

    }
}