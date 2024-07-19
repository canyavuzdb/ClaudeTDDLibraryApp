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
    public class OpenLibraryBooksController : ControllerBase
    {
        private readonly IOpenLibraryService _openLibraryService;
        private readonly IBookService _bookService;

        public OpenLibraryBooksController(IOpenLibraryService openLibraryService, IBookService bookService)
        {
            _openLibraryService = openLibraryService;
            _bookService = bookService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks(string query, int maxResults = 40)
        {
            var books = await _openLibraryService.SearchBooksAsync(query, maxResults);
            return Ok(books);
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedBooks(string query, int maxResults = 40)
        {
            var books = await _openLibraryService.SearchBooksAsync(query, maxResults);
            var addedBooks = await _bookService.AddBooksAsync(books);
            return Ok($"{addedBooks} books have been added to the database.");
        }
    }
}