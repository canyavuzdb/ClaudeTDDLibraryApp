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
    }
}
