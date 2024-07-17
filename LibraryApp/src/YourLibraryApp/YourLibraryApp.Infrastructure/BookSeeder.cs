using System.Threading.Tasks;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Infrastructure.Data
{
    public class BookSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IOpenLibraryService _openLibraryService;

        public BookSeeder(ApplicationDbContext context, IOpenLibraryService openLibraryService)
        {
            _context = context;
            _openLibraryService = openLibraryService;
        }

        public async Task SeedBooksAsync(int numberOfBooks = 1000)
        {
            var queries = new[] { "fiction", "science", "history", "biography" };
            var booksPerQuery = numberOfBooks / queries.Length;

            foreach (var query in queries)
            {
                var books = await _openLibraryService.SearchBooksAsync(query, booksPerQuery);
                await _context.Books.AddRangeAsync(books);
            }

            await _context.SaveChangesAsync();
        }
    }
}