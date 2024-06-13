using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Application
{
   public interface IGoogleBooksService
{
    Task<IEnumerable<Book>> SearchBooks(string query, int maxResults);
    Task SeedBooksFromGoogleBooks(string query, int maxResults);
}
}