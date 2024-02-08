using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samurai_V2_.Net_8.DTOs;
using Samurai_V2_.Net_8.IRepository;

namespace Samurai_V2_.Net_8.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookStoreController : ControllerBase
    {
         private static IBookRepo _bookRepo;
        
        private readonly ILogger<BookStoreController> _logger;

        public BookStoreController(ILogger<BookStoreController> logger, IBookRepo bookRepo)
        {
            _logger = logger;
            _bookRepo = bookRepo;
        }

        [HttpPost("g", Name = "CreateNew")]
        public async Task<IActionResult> CreateNew(BookDto b)
        {
            try
            {
                var createdBook = await _bookRepo.CreateBook(b);


                return StatusCode(StatusCodes.Status201Created, createdBook);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPut("/", Name = "Update")]
        public async Task<IActionResult> Update(int id,BookDto b)
        {
            try
            {
                var createdBook = await _bookRepo.updateBooks(id,b);

                if (createdBook == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "book with id:"+ id + "was not found");
                }
                else
                {
                  return Ok(createdBook);
                }
              
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
