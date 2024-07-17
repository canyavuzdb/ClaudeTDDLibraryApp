using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Infrastructure
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _dbContext.Authors.Include(a => a.Books);
        }

        public Author GetAuthorById(int id)
        {
            return _dbContext.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
        }

        public void AddAuthor(Author author)
        {
            Add(author);
        }

        public void UpdateAuthor(Author author)
        {
            Update(author);
        }

        public void DeleteAuthor(int id)
        {
            Delete(id);
        }

        public Task<Author> GetAuthorByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task AddAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }
    }
}