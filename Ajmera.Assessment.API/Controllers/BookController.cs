using Ajmera.Assessment.BL.Services;
using Ajmera.Assessment.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ajmera.Assessment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet(Name = "GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookService.GetBooksAsync();

            _logger.LogTrace($"Get All Books From GetBooksAsync Method");

            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("BookById")]
        public async Task<IActionResult> Get(Guid bookId)
        {
            if (bookId == Guid.Empty)
                return BadRequest("bookId is invalid");

            var result = await _bookService.GetBookByIdAsync(bookId);

            _logger.LogTrace($"Passed bookId: {bookId} in GetBookAsync Method");

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost(Name = "SaveBook")]
        public async Task<IActionResult> Post([FromBody] BookMasterDto bookMasterDto)
        {
            if (bookMasterDto == null)
            {
                _logger.LogError("bookMasterDto object sent from client is null.");
                return BadRequest("bookMasterDto object is null");
            }

            var result = await _bookService.SaveBookAsync(bookMasterDto);

            _logger.LogTrace($"Passed bookMasterDto: {bookMasterDto.Name}-{bookMasterDto.AuthorName} in SaveBookAsync Method");

            return result == null ? NotFound() : Ok(result); ;
        }
    }
}