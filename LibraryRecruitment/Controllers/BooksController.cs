using AutoMapper;
using Library.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController(
    IBooksRepository booksRepository,
    ILogger<BooksController> logger,
    IMapper mapper)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDTO>>> Get()
    {
        try
        {
            var books = await booksRepository.GetAllAsync();
            List<BookDTO> response = [];
            foreach (var bookEntity in books)
            {
                var book = mapper.Map<BookDTO>(bookEntity);
                book.AvailableCount = await booksRepository.GetAvailableCountAsync(bookEntity.Isbn);

                response.Add(book);
            }

            return Ok(response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occurred");
            return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
        }
    }

    [HttpPost("{isbn}/lend")]
    public async Task<ActionResult> Lend([FromRoute] string isbn)
    {
        try
        {
            var canLend = await booksRepository.CanLendAsync(isbn);
            if (!canLend)
            {
                logger.LogInformation("Cannot lend book {isbn}", isbn);
                return BadRequest("Cannot lend this book");
            }

            await booksRepository.LendAsync(isbn);
            return Ok();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occurred");
            return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
        }
    }

    [HttpPost("{isbn}/return")]
    public async Task<ActionResult> Return([FromRoute] string isbn)
    {
        try
        {
            await booksRepository.ReturnAsync(isbn);
            return Ok();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occurred");
            return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
        }
    }
}