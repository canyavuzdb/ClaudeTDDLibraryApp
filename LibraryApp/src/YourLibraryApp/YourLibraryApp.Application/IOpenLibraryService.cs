using System.Collections.Generic;
using System.Threading.Tasks;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Application
{
    public interface IOpenLibraryService
    {
        Task<List<Book>> SearchBooksAsync(string query, int maxResults = 40);
    }
}