using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourLibraryApp.Domain
{
    // Author.cs
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }

}
