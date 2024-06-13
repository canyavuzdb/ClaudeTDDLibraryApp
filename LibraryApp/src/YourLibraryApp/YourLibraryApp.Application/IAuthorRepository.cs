using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Application
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetAuthorById(int id);
        // Author GetAuthorByName(string name);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int id);
    }
}