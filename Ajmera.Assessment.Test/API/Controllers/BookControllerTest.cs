using Ajmera.Assessment.API.Controllers;
using Ajmera.Assessment.BL.Services;
using Ajmera.Assessment.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ajmera.Assessment.Test.API.Controllers
{
    public class BookControllerTest
    {
        [Fact]
        public async Task Get_BookById_Returns_OkResult()
        {
            // Arrange
            Mock<IBookService> bookServiceMock = new Mock<IBookService>();
            Mock<ILogger<BookController>> loggerMock = new Mock<ILogger<BookController>>();

            _ = bookServiceMock.Setup(x => x.GetBookByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<BookMasterResponseDto>(new Shared.DTO.BookMasterResponseDto()
                {
                    Id = new Guid("54c4e684-0a6a-449d-b445-61ddd12ffd3d"),
                    Name = "Ajmera Book",
                    AuthorName = "Navneet Sharma"
                }));

            var sut = new BookController(loggerMock.Object, bookServiceMock.Object);
            var bookId = Guid.NewGuid();

            // Act
            var result = await sut.Get(bookId);
            var okResult = result as OkObjectResult;
            BookMasterResponseDto bookMasterOkResult = (BookMasterResponseDto)okResult?.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(new Guid("54c4e684-0a6a-449d-b445-61ddd12ffd3d"), bookMasterOkResult.Id);
            Assert.Equal("Ajmera Book", bookMasterOkResult.Name);
            Assert.Equal("Navneet Sharma", bookMasterOkResult.AuthorName);
            bookServiceMock.Verify(x => x.GetBookByIdAsync(bookId), Times.Once);
        }

        [Fact]
        public async Task Get_BookById_Returns_BadRequest_Result()
        {
            // Arrange
            Mock<IBookService> bookServiceMock = new Mock<IBookService>();
            Mock<ILogger<BookController>> loggerMock = new Mock<ILogger<BookController>>();

            _ = bookServiceMock.Setup(x => x.GetBookByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<BookMasterResponseDto>(null));

            var sut = new BookController(loggerMock.Object, bookServiceMock.Object);
            var bookId = Guid.Empty;

            // Act
            var result = await sut.Get(bookId);
            var okResult = result as OkObjectResult;
            BookMasterResponseDto bookMasterOkResult = (BookMasterResponseDto)okResult?.Value;

            // Assert
            Assert.Null(okResult);
        }

        [Fact]
        public async Task Get_BookById_Returns_NotFound_Result()
        {
            // Arrange
            Mock<IBookService> bookServiceMock = new Mock<IBookService>();
            Mock<ILogger<BookController>> loggerMock = new Mock<ILogger<BookController>>();

            _ = bookServiceMock.Setup(x => x.GetBookByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<BookMasterResponseDto>(null));

            var sut = new BookController(loggerMock.Object, bookServiceMock.Object);
            var bookId = Guid.NewGuid();

            // Act
            var result = await sut.Get(bookId);
            var okResult = result as OkObjectResult;
            BookMasterResponseDto bookMasterOkResult = (BookMasterResponseDto)okResult?.Value;

            // Assert
            Assert.Null(okResult);
        }
    }
}
