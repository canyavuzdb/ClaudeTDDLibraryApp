using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using YourLibraryApp.Domain;
using YourLibraryApp.Application;

namespace YourLibraryApp.Infrastructure
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _dbContext.Books.Include(b => b.Author);
        }

        public Book GetBookById(int id)
        {
            return _dbContext.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            Add(book);
        }

        public void UpdateBook(Book book)
        {
            Update(book);
        }

        public void DeleteBook(int id)
        {
            Delete(id);
        }

        public Task<Book> GetBookByTitleAndAuthorIdAsync(string title, int? authorId)
        {
            return _dbContext.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Title == title && b.AuthorId == authorId);

        }

        public Task AddBookAsync(Book book)
        {
            Add(book);
            return Task.CompletedTask;
        }
    }
}   