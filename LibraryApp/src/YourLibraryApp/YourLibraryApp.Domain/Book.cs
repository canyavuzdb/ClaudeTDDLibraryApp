using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourLibraryApp.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; } // Yeni eklenen özellik
        public Author Author { get; set; }
        public int PublicationYear { get; set; }
        public string Genre { get; set; }
    }
}