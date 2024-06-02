using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Application
{
    public class AuthorService : IAuthorService
    {

        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        public Author GetAuthorById(int id)
        {
            return _authorRepository.GetAuthorById(id);
        }

        public void AddAuthor(Author author)
        {
            // Eğer gerekli ise, yazar ekleme öncesi bazı iş kuralları veya doğrulama işlemleri yapılabilir

            _authorRepository.AddAuthor(author);
        }

        public void UpdateAuthor(Author author)
        {
            // Eğer gerekli ise, yazar güncelleme öncesi bazı iş kuralları veya doğrulama işlemleri yapılabilir

            _authorRepository.UpdateAuthor(author);
        }

        public void DeleteAuthor(int id)
        {
            // Eğer gerekli ise, yazar silme öncesi bazı iş kuralları veya doğrulama işlemleri yapılabilir

            _authorRepository.DeleteAuthor(id);
        }
    }
}