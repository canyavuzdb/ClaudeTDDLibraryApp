using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;


namespace YourLibraryApp.Infrastructure
{
    // YourLibraryApp.Infrastructure projesinde
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AuthorDbContext _dbContext;

        public AuthorRepository(AuthorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _dbContext.Authors.ToList();
        }

        public Author GetAuthorById(int id)
        {
            return _dbContext.Authors.Find(id);
        }

        public void AddAuthor(Author author)
        {
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            _dbContext.Authors.Update(author);
            _dbContext.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var author = _dbContext.Authors.Find(id);
            if (author != null)
            {
                _dbContext.Authors.Remove(author);
                _dbContext.SaveChanges();
            }
        }
    }
}