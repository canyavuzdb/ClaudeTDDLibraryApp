using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Application
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository; // Yeni eklenen

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
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
            if (book.AuthorId <= 0)
                throw new ArgumentException("Invalid author ID");
            _bookRepository.AddBook(book);
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null");
            if (book.AuthorId <= 0)
                throw new ArgumentException("Invalid author ID");
            _bookRepository.UpdateBook(book);
        }

        public void DeleteBook(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid book ID");
            _bookRepository.DeleteBook(id);
        }

        // Yeni eklenen metod
        public async Task<int> AddBooksAsync(IEnumerable<Book> books)
        {
            if (books == null)
                throw new ArgumentNullException(nameof(books), "Books collection cannot be null");

            var addedBooks = 0;

            foreach (var book in books)
            {
                if (book.Author != null && book.Author.Id == 0)
                {
                    var existingAuthor = await _authorRepository.GetAuthorByNameAsync(book.Author.Name);
                    if (existingAuthor == null)
                    {
                        await _authorRepository.AddAuthorAsync(book.Author);
                    }
                    else
                    {
                        book.Author = existingAuthor;
                    }
                }

                if (book.AuthorId <= 0)
                    book.AuthorId = book.Author.Id;

                var existingBook = await _bookRepository.GetBookByTitleAndAuthorIdAsync(book.Title, book.AuthorId);
                if (existingBook == null)
                {
                    await _bookRepository.AddBookAsync(book);
                    addedBooks++;
                }
            }

            return addedBooks;
        }
    }
}