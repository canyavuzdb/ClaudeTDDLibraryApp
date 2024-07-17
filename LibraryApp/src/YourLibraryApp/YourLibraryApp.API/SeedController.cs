using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourLibraryApp.Application;

namespace YourLibraryApp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly IOpenLibraryService _openLibraryService;

        public SeedController(IOpenLibraryService openLibraryService)
        {
            _openLibraryService = openLibraryService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks(string query, int maxResults = 40)
        {
            var books = await _openLibraryService.SearchBooksAsync(query, maxResults);
            return Ok(books);
        }
    }
}