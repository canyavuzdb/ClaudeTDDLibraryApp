using Microsoft.AspNetCore.Mvc;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace YourLibraryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleBooksController : ControllerBase
    {
        private readonly IGoogleBooksService _googleBooksService;

        public GoogleBooksController(IGoogleBooksService googleBooksService)
        {
            _googleBooksService = googleBooksService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks([FromQuery] string query, [FromQuery] int maxResults = 10)
        {
            var books = await _googleBooksService.SearchBooks(query, maxResults);
            return Ok(books);
        }

        // Opsiyonel: Veritabanına kaydetme işlemi için POST metodu
        [HttpPost("seed")]
        public async Task<IActionResult> SeedBooksFromGoogleBooks([FromBody] GoogleBooksSeedRequest request)
        {
            await _googleBooksService.SeedBooksFromGoogleBooks(request.Query, request.MaxResults);
            return Ok("Books seeded successfully");
        }
    }

    public class GoogleBooksSeedRequest
    {
        public string Query { get; set; }
        public int MaxResults { get; set; }
    }
}