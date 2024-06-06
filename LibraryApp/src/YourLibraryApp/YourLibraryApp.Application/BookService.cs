using System;
using System.Collections.Generic;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Application
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            // Sadece AuthorId kontrolü yapacağız
            if (book.AuthorId <= 0)
                throw new ArgumentException("Invalid author ID");

            _bookRepository.AddBook(book);
        }

        public void UpdateBook(Book book)
        {
            // Kitap geçerliliği kontrolü
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null");

            // Sadece AuthorId kontrolü yapacağız
            if (book.AuthorId <= 0)
                throw new ArgumentException("Invalid author ID");

            _bookRepository.UpdateBook(book);
        }

        public void DeleteBook(int id)
        {
            // Geçerli kitap ID kontrolü
            if (id <= 0)
                throw new ArgumentException("Invalid book ID");

            _bookRepository.DeleteBook(id);
        }
    }
}