using Microsoft.EntityFrameworkCore;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Infrastructure
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext;

        public BookRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _dbContext.Books.Include(b => b.Author).ToList();
        }

        public Book GetBookById(int id)
        {
            return _dbContext.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
        }
    }
}