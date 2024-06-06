using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Infrastructure
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.PublicationYear)
                .IsRequired();

            builder.Property(b => b.Genre)
                .HasMaxLength(50);

            builder.HasOne<Author>()
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}