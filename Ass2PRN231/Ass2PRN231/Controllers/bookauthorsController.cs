using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Entity;
using Repository.Models;
using Repository.Models.BookAuthorModelFolder;

namespace Ass2PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bookauthorsController : ControllerBase
    {
        private readonly Ass2Prn231Context _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public bookauthorsController(Ass2Prn231Context context, UnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Get All Book Authors

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllBookAuthorModel>>> GetBookAuthors()
        {
            try
            {
                var bookAuthors = _unitOfWork.BookAuthorRepository.Get();
                var bookAuthorModels = _mapper.Map<IEnumerable<BookAuthor>, IEnumerable<GetAllBookAuthorModel>>(bookAuthors);

                return Ok(bookAuthorModels);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region Get Book Author By Id

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAllBookAuthorModel>> GetBookAuthorById(int id)
        {
            try
            {
                var bookAuthor = _unitOfWork.BookAuthorRepository.GetByID(id);

                if (bookAuthor == null)
                {
                    return NotFound();
                }

                var bookAuthorModel = _mapper.Map<GetAllBookAuthorModel>(bookAuthor);

                return Ok(bookAuthorModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region Update Book Author

        [HttpPut("{id}")]
        public IActionResult UpdateBookAuthor(int id, AddNewBookAuthorModel model)
        {
            var bookAuthor = _context.BookAuthors.FirstOrDefault(ba => ba.Id == id);

            if (bookAuthor == null)
            {
                return NotFound("BookAuthor not found.");
            }

            if (model.BookId == null || model.AuthorId == null)
            {
                return BadRequest("BookId and AuthorId must be provided.");
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == model.BookId);
            var author = _context.Authors.FirstOrDefault(a => a.Id == model.AuthorId);

            if (book == null || author == null)
            {
                return NotFound("Book or Author not found.");
            }

            bookAuthor.BookId = model.BookId.Value;
            bookAuthor.AuthorId = model.AuthorId.Value;
            bookAuthor.AuthorOrder = model.AuthorOrder;
            bookAuthor.RoyaltyPer = model.RoyaltyPer;

            _context.SaveChanges();

            return Ok("BookAuthor updated successfully.");
        }

        #endregion

        #region Create New Book Author

        [HttpPost]
        public IActionResult AddNewBookAuthor(AddNewBookAuthorModel model)
        {
            if (model.BookId == null || model.AuthorId == null)
            {
                return BadRequest("BookId and AuthorId must be provided.");
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == model.BookId);
            var author = _context.Authors.FirstOrDefault(a => a.Id == model.AuthorId);

            if (book == null || author == null)
            {
                return NotFound("Book or Author not found.");
            }

            var newBookAuthor = new BookAuthor
            {
                BookId = model.BookId.Value,
                AuthorId = model.AuthorId.Value,
                AuthorOrder = model.AuthorOrder,
                RoyaltyPer = model.RoyaltyPer
            };

            _context.BookAuthors.Add(newBookAuthor);
            _context.SaveChanges();

            return Ok("BookAuthor added successfully.");
        }

        #endregion

        #region Delete Book Author 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAuthor(int id)
        {
            var bookAuthor = await _context.BookAuthors.FindAsync(id);
            if (bookAuthor == null)
            {
                return NotFound("Id doesn't exits!");
            }

            _context.BookAuthors.Remove(bookAuthor);
            await _context.SaveChangesAsync();

            return Ok("Delete Success.");
        }

        #endregion
    }
}
