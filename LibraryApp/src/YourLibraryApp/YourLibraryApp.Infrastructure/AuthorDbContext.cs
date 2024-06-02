using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourLibraryApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace YourLibraryApp.Infrastructure
{
   public class AuthorDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Veritabanı bağlantı stringinizi burada ayarlayın
        optionsBuilder.UseSqlServer(@"Connection String;Database=YourLibraryDatabase");
    }
}

}