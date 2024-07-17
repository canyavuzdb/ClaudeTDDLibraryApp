using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Infrastructure.Services
{
    public class OpenLibraryService : IOpenLibraryService
    {
        private readonly HttpClient _httpClient;

        public OpenLibraryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Book>> SearchBooksAsync(string query, int maxResults = 40)
        {
            var url = $"http://openlibrary.org/search.json?q={Uri.EscapeDataString(query)}&limit={maxResults}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var searchResult = JsonSerializer.Deserialize<OpenLibrarySearchResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return searchResult?.Docs.Select(doc => new Book
            {
                Title = doc.Title,
                PublicationYear = doc.FirstPublishYear,
                Genre = doc.Subject?.FirstOrDefault() ?? "Unknown Genre",
                // AuthorId ve Author property'leri burada set edilmiyor,
                // çünkü önce Author'ı veritabanına eklemeniz gerekiyor.
            }).ToList() ?? new List<Book>();
        }
    }

    public class OpenLibrarySearchResult
    {
        public List<OpenLibraryDoc> Docs { get; set; }
    }

    public class OpenLibraryDoc
    {
        public string Title { get; set; }
        public List<string> AuthorName { get; set; }
        public List<string> Isbn { get; set; }
        public int? FirstPublishYear { get; set; }
        public List<string> Subject { get; set; }
    }
}