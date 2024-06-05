using System;
using System.Collections.Generic;
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
            // Ad alanının boş olmaması kontrolü
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("Author name cannot be empty or whitespace.");

            // Biyografi alanının boş olmaması kontrolü (eğer Biography özelliği varsa)
            if (string.IsNullOrWhiteSpace(author.Biography))
                throw new ArgumentException("Author biography cannot be empty or whitespace.");

            _authorRepository.AddAuthor(author);
        }

        public void UpdateAuthor(Author author)
        {
            // Yazar nesnesinin geçerliliği kontrolü
            if (author == null)
                throw new ArgumentNullException(nameof(author), "Author cannot be null");

            // Yazar ID'sinin geçerliliği kontrolü
            if (author.Id <= 0)
                throw new ArgumentException("Invalid author ID");

            _authorRepository.UpdateAuthor(author);
        }

        public void DeleteAuthor(int id)
        {
            // Yazar ID'sinin geçerliliği kontrolü
            if (id <= 0)
                throw new ArgumentException("Invalid author ID");

            _authorRepository.DeleteAuthor(id);
        }
    }
}