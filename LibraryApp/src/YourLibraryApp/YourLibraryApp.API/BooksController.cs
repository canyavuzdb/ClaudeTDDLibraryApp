using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;

namespace YourLibraryApp.API
{
   // BooksController.cs
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IEnumerable<Book> GetAllBooks()
    {
        return _bookService.GetAllBooks();
    }

    [HttpGet("{id}")]
    public Book GetBookById(int id)
    {
        return _bookService.GetBookById(id);
    }

    [HttpPost]
    public IActionResult AddBook(Book book)
    {
        _bookService.AddBook(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        _bookService.UpdateBook(book);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var bookToDelete = _bookService.GetBookById(id);
        if (bookToDelete == null)
        {
            return NotFound();
        }

        _bookService.DeleteBook(id);
        return NoContent();
    }
}
}