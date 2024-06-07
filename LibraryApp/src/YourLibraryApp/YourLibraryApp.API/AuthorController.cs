using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;

namespace YourLibraryApp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorService.GetAllAuthors();
        }

        [HttpGet("{id}")]
        public Author GetAuthorById(int id)
        {
            return _authorService.GetAuthorById(id);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _authorService.AddAuthor(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] Author author)
        {
            if (id != author.Id)
                return BadRequest();

            _authorService.UpdateAuthor(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var authorToDelete = _authorService.GetAuthorById(id);
            if (authorToDelete == null)
                return NotFound();

            _authorService.DeleteAuthor(id);
            return NoContent();
        }
    }
}