using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GoogleBooksService : IGoogleBooksService
{
    private readonly BooksService _booksService;
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;

    public GoogleBooksService(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        _booksService = new BooksService(new BaseClientService.Initializer());
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> SearchBooks(string query, int maxResults)
    {
        var request = _booksService.Volumes.List(query);
        request.MaxResults = maxResults;
        var response = await request.ExecuteAsync();

        var books = new List<Book>();
        foreach (var item in response.Items)
        {
            var volumeInfo = item.VolumeInfo;
            var author = await GetOrCreateAuthor(volumeInfo.Authors);

            var book = new Book
            {
                Title = volumeInfo.Title,
                AuthorId = author?.Id,
                PublicationYear = GetPublicationYear(volumeInfo.PublishedDate),
                Genre = string.Join(", ", volumeInfo.Categories ?? new List<string>()),
                Author = author  // Bu, API yanıtında yazarın bilgilerini de içerir
            };
            books.Add(book);
        }

        return books;
    }

    public async Task SeedBooksFromGoogleBooks(string query, int maxResults)
    {
        var request = _booksService.Volumes.List(query);
        request.MaxResults = maxResults;
        var response = await request.ExecuteAsync();

        foreach (var volumeInfo in response.Items.Select(item => item.VolumeInfo))
        {
            var author = await GetOrCreateAuthor(volumeInfo.Authors);
            int? publicationYear = GetPublicationYear(volumeInfo.PublishedDate);

            if (author != null)
            {
                var book = new Book
                {
                    Title = volumeInfo.Title,
                    AuthorId = author.Id,
                    PublicationYear = publicationYear,
                    Genre = string.Join(", ", volumeInfo.Categories ?? Enumerable.Empty<string>())
                };
                _bookRepository.AddBook(book);
            }
            else
            {
                Console.WriteLine($"Author could not be created for book: {volumeInfo.Title}");
            }
        }
    }

    private async Task<Author> GetOrCreateAuthor(IList<string> authorNames)
    {
        if (authorNames == null || !authorNames.Any())
            return null;

        var authorName = string.Join(", ", authorNames);
        var existingAuthor = _authorRepository.GetAllAuthors().FirstOrDefault(a => a.Name == authorName);
        if (existingAuthor != null)
            return existingAuthor;

        var newAuthor = new Author { Name = authorName };
        _authorRepository.AddAuthor(newAuthor);
        return newAuthor;
    }

    private int? GetPublicationYear(string publishedDate)
    {
        if (DateTime.TryParse(publishedDate, out DateTime date))
        {
            return date.Year;
        }
        return null;
    }
}