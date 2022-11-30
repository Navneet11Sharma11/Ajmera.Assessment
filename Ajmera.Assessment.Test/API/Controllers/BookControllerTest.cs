using Ajmera.Assessment.API.Controllers;
using Ajmera.Assessment.BL.Services;
using Ajmera.Assessment.Shared.Common;
using Ajmera.Assessment.Shared.DTO;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using Xunit;

namespace Ajmera.Assessment.Test.API.Controllers
{
    public class BookControllerTest
    {
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly Mock<ILogger<BookController>> _loggerMock;
        private BookMasterRequestDto _bookMasterRequestDto;

        public BookControllerTest()
        {
            _bookServiceMock = new Mock<IBookService>();
            _loggerMock = new Mock<ILogger<BookController>>();
        }

        [Fact]
        public async Task Get_BookById_Returns_OkResult()
        {
            // Arrange
            _ = _bookServiceMock.Setup(x => x.GetBookByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<BookMasterResponseDto>(new Shared.DTO.BookMasterResponseDto()
                {
                    Id = new Guid("54c4e684-0a6a-449d-b445-61ddd12ffd3d"),
                    Name = "Ajmera Book",
                    AuthorName = "Navneet Sharma"
                }));

            var sut = new BookController(_loggerMock.Object, _bookServiceMock.Object);
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
            _bookServiceMock.Verify(x => x.GetBookByIdAsync(bookId), Times.Once);
        }

        [Fact]
        public async Task Get_BookById_Returns_BadRequest_Result()
        {
            // Arrange
            _ = _bookServiceMock.Setup(x => x.GetBookByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<BookMasterResponseDto>(null));

            var sut = new BookController(_loggerMock.Object, _bookServiceMock.Object);
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
            _ = _bookServiceMock.Setup(x => x.GetBookByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<BookMasterResponseDto>(null));

            var sut = new BookController(_loggerMock.Object, _bookServiceMock.Object);
            var bookId = Guid.NewGuid();

            // Act
            var result = await sut.Get(bookId);
            var okResult = result as OkObjectResult;
            BookMasterResponseDto bookMasterOkResult = (BookMasterResponseDto)okResult?.Value;

            // Assert
            Assert.Null(okResult);
        }

        [Fact]
        public async Task AddSaveBook_SaveAsync_Passed()
        {
            // Arrange  
            _bookMasterRequestDto = new BookMasterRequestDto() { Name = "Rich Dad Poor Dad", AuthorName = "Robert Kiyosaki" };
            _bookServiceMock.Setup(_ => _.SaveBookAsync(_bookMasterRequestDto)).ReturnsAsync(new ResultDomain()
            {
                IsSuccess = true,
                Data = new BookMasterResponseDto()
                {
                    Id = new Guid("54c4e684-0a6a-449d-b445-61ddd12ffd3d"),
                    Name = "Rich Dad Poor Dad",
                    AuthorName = "Robert Kiyosaki"
                },
                TotalCount = 1,
            });
            var sut = new BookController(_loggerMock.Object, _bookServiceMock.Object);

            // Act  
            var result = await sut.Post(_bookMasterRequestDto);
            var okResult = result as OkObjectResult;
            ResultDomain resultDomain = (ResultDomain)okResult?.Value;
            BookMasterResponseDto bookMasterResponseDto = (BookMasterResponseDto)resultDomain?.Data;

            //Assert  
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(new Guid("54c4e684-0a6a-449d-b445-61ddd12ffd3d"), bookMasterResponseDto.Id);
        }

        [Fact]
        public async Task AddSaveBook_SaveAsync_Failed()
        {
            // Arrange  
            _bookMasterRequestDto = new BookMasterRequestDto() { Name = "Rich Dad Poor Dad", AuthorName = "Robert Kiyosaki" };
            _bookServiceMock.Setup(_ => _.SaveBookAsync(_bookMasterRequestDto)).ReturnsAsync(new ResultDomain()
            {
                IsSuccess = false,
                Data = null,
                TotalCount = 0,
            });
            var sut = new BookController(_loggerMock.Object, _bookServiceMock.Object);

            // Act  
            var result = await sut.Post(_bookMasterRequestDto);
            var okResult = result as OkObjectResult;
            ResultDomain resultDomain = (ResultDomain)okResult?.Value;
            BookMasterResponseDto bookMasterResponseDto = (BookMasterResponseDto)resultDomain?.Data;

            //Assert  
            Assert.Equal(0, resultDomain.TotalCount);
            Assert.Equal(false, resultDomain.IsSuccess);
        }
    }
}
