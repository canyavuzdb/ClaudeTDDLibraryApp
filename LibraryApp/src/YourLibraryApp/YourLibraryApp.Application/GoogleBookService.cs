// using Google.Apis.Books.v1;
// using Google.Apis.Books.v1.Data;
// using Google.Apis.Services;
// using YourLibraryApp.Application;
// using YourLibraryApp.Domain;
// using YourLibraryApp.Infrastructure;

// public class GoogleBooksService
// {
//     private readonly BooksService _booksService;
//     private readonly IAuthorRepository _authorRepository;
//     private readonly IBookRepository _bookRepository;

//     public GoogleBooksService(IAuthorRepository authorRepository, IBookRepository bookRepository)
//     {
//         _booksService = new BooksService(new BaseClientService.Initializer());
//         _authorRepository = authorRepository;
//         _bookRepository = bookRepository;
//     }

//     public async Task SeedBooksFromGoogleBooks(string query, int maxResults)
//     {
//         var request = _booksService.Volumes.List(query);
//         request.MaxResults = maxResults;

//         var response = await request.ExecuteAsync();

//         foreach (var volumeInfo in response.Items.Select(item => item.VolumeInfo))
//         {
//             var author = await GetOrCreateAuthor(volumeInfo.Authors);
//             DateTime publishedDate;
//             int? publicationYear = null;

//             if (DateTime.TryParse(volumeInfo.PublishedDate, out publishedDate))
//             {
//                 publicationYear = publishedDate.Year;
//             }

//             var book = new Book
//             {
//                 Title = volumeInfo.Title,
//                 AuthorId = author?.Id,
//                 PublicationYear = publicationYear,
//                 Genre = string.Join(", ", volumeInfo.Categories ?? Enumerable.Empty<string>())
//             };

//             _bookRepository.AddBook(book);
//         }
//     }

//   private async Task<Author> GetOrCreateAuthor(IList<string> authorNames)
// {
//     if (authorNames == null || !authorNames.Any())
//         return null;

//     var authorName = string.Join(", ", authorNames);
//     var existingAuthor = _authorRepository.GetAllAuthors().FirstOrDefault(a => a.Name == authorName);

//     if (existingAuthor != null)
//         return existingAuthor;

//     var newAuthor = new Author { Name = authorName };
//     _authorRepository.AddAuthor(newAuthor);
//     return newAuthor;
// }
// }