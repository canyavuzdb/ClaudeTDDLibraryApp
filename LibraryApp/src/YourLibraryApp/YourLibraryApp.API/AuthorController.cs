using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;


namespace YourLibraryApp.API
{
    public class AuthorController
    {
        [ApiController]
        [Route("[controller]")]
        public class AuthorsController : ControllerBase
        {
            private readonly IAuthorService _authorService;

            public AuthorsController(IAuthorService authorService)
            {
                _authorService = authorService;
            }

            [HttpGet]
            public IEnumerable<Author> GetAuthors()
            {
                return _authorService.GetAllAuthors();
            }
        }
    }
}