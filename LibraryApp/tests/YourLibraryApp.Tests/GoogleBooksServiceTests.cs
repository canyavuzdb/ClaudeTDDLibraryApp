using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using YourLibraryApp.Application;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Tests
{
    public class GoogleBooksServiceTests
    {
        [Fact]
        public async Task SearchBooks_ShouldReturnBooks()
        {
            // Arrange
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            var mockBookRepository = new Mock<IBookRepository>();

            var service = new GoogleBooksService(mockAuthorRepository.Object, mockBookRepository.Object);

            // Act
            var result = await service.SearchBooks("test", 5);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task SeedBooksFromGoogleBooks_ShouldAddBooks()
        {
            // Arrange
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            var mockBookRepository = new Mock<IBookRepository>();

            mockAuthorRepository.Setup(repo => repo.GetAllAuthors())
                .Returns(new List<Author>());

            var service = new GoogleBooksService(mockAuthorRepository.Object, mockBookRepository.Object);

            // Act
            await service.SeedBooksFromGoogleBooks("test", 5);

            // Assert
            mockBookRepository.Verify(repo => repo.AddBook(It.IsAny<Book>()), Times.AtLeastOnce());
        }
    }
}