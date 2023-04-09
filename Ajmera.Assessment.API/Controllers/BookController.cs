using Ajmera.Assessment.API.ApiRoutes;
using Ajmera.Assessment.API.ApiUtilities;
using Ajmera.Assessment.API.Models;
using Ajmera.Assessment.BL.Services;
using Ajmera.Assessment.Shared.Common;
using Ajmera.Assessment.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Ajmera.Assessment.API.Controllers
{
    [ApiController]
    //[Authorize]
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

        //[HttpGet(Name = "ping")]
        //[Route("ping")]
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<ServiceResponse<string>> Ping(string ping)
        //{
            //return "Ping";
        //}
        

        /// <summary>
        /// Returns all the books in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = BookApiRoute.GetBooks)]
        [ProducesResponseType(typeof(IEnumerable<BookMasterResponseDto>), StatusCodes.Status200OK)]
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
                return BadRequest(result);
        }

        /// <summary>
        /// Returns the specific book in the system
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpGet(BookApiRoute.BookById)]
        public async Task<IActionResult> Get(Guid bookId)
        {
            if (bookId == Guid.Empty)
                return BadRequest(ConstantMessages.BookIdInvalidMessage);

            var result = await _bookService.GetBookByIdAsync(bookId);

            _logger.LogTrace($"Passed bookId: {bookId} in GetBookAsync Method");

            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Save new book in the system
        /// </summary>
        /// <param name="bookMasterRequestDto"></param>
        /// <returns></returns>
        [HttpPost(Name = BookApiRoute.SaveBook)]
        [ProducesResponseType(typeof(BookMasterResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] BookMasterRequestDto bookMasterRequestDto)
        {
            var result = new ResultDomain();
            result.IsSuccess = ModelState.IsValid;
            result.Errors = ModelState.SelectMany(x => x.Value.Errors.Select(s => s.ErrorMessage)).ToList();

            if (result.IsSuccess)
            {
                result = await _bookService.SaveBookAsync(bookMasterRequestDto);
                if (result.IsSuccess)
                    _logger.LogTrace($"Passed bookMasterDto: {bookMasterRequestDto.Name}-{bookMasterRequestDto.AuthorName} in SaveBookAsync Method");
                return Ok(result);
            }
            else
                return BadRequest(result);
        }
    }
}