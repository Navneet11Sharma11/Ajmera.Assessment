using Ajmera.Assessment.API.ApiRoutes;
using Ajmera.Assessment.API.Models;
using Ajmera.Assessment.BL.Services;
using Ajmera.Assessment.Shared.Common;
using Ajmera.Assessment.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ajmera.Assessment.API.Controllers
{
    [ApiController]
    [Route(BookApiRoute.ControllerName)]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet(Name = BookApiRoute.GetBooks)]
        [ProducesResponseType(typeof(IEnumerable<BookMasterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetBooks()
        {
            var result = new ResultDomain();
            result.IsSuccess = ModelState.IsValid;
            result.Errors = ModelState.SelectMany(x => x.Value.Errors.Select(s => s.ErrorMessage)).ToList();

            if (result.IsSuccess)
            {
                result = await _bookService.GetBooksAsync();
                if (result.IsSuccess)
                    _logger.LogTrace($"Get All Books From GetBooksAsync Method");
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet(BookApiRoute.BookById)]
        public async Task<IActionResult> Get(Guid bookId)
        {
            if (bookId == Guid.Empty)
                return BadRequest(ConstantMessages.BookIdInvalidMessage);

            var result = await _bookService.GetBookByIdAsync(bookId);

            _logger.LogTrace($"Passed bookId: {bookId} in GetBookAsync Method");

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost(Name = BookApiRoute.SaveBook)]
        [ProducesResponseType(typeof(BookMasterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] BookMasterDto bookMasterDto)
        {
            var result = new ResultDomain();
            result.IsSuccess = ModelState.IsValid;
            result.Errors = ModelState.SelectMany(x => x.Value.Errors.Select(s => s.ErrorMessage)).ToList();

            if (result.IsSuccess)
            {
                result = await _bookService.SaveBookAsync(bookMasterDto);
                if (result.IsSuccess)
                    _logger.LogTrace($"Passed bookMasterDto: {bookMasterDto.Name}-{bookMasterDto.AuthorName} in SaveBookAsync Method");
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}