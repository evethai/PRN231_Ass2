using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Entity;
using Repository.Models;

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

        #region Get All Book Author

        /*        // GET: api/bookauthors
                [HttpGet]
                public async Task<ActionResult<IEnumerable<BookAuthor>>> GetBookAuthors()
                {
                  if (_context.BookAuthors == null)
                  {
                      return NotFound();
                  }
                    return await _context.BookAuthors.ToListAsync();
                }*/

        // GET: api/bookauthors
        /* [HttpGet]
         public async Task<ActionResult<IEnumerable<BookAuthorModel>>> GetBookAuthors()
         {
             try
             {
                 // Lấy toàn bộ danh sách các bản ghi từ Repository
                 var bookAuthors = _unitOfWork.BookAuthorRepository.Get();

                 // Ánh xạ danh sách các bản ghi từ BookAuthorEntity sang BookAuthorModel
                 var bookAuthorModels = _mapper.Map<IEnumerable<BookAuthor>, IEnumerable<BookAuthorModel>>(bookAuthors);

                 // Trả về danh sách đã ánh xạ
                 return Ok(bookAuthorModels);
             }
             catch (Exception ex)
             {
                 // Xử lý ngoại lệ và trả về lỗi 500
                 return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
             }
         }*/


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookAuthorModel>>> GetBookAuthors([FromQuery] bool includeBooks = false)
        {
            try
            {
                // Lấy toàn bộ danh sách các bản ghi từ Repository
                var bookAuthors = _unitOfWork.BookAuthorRepository.Get();

                // Ánh xạ danh sách các bản ghi từ BookAuthorEntity sang BookAuthorModel
                var bookAuthorModels = _mapper.Map<IEnumerable<BookAuthor>, IEnumerable<BookAuthorModel>>(bookAuthors);

                if (includeBooks)
                {
                    // Lấy thông tin về sách từ cơ sở dữ liệu và ánh xạ sang BookModel
                    foreach (var bookAuthorModel in bookAuthorModels)
                    {
                        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookAuthorModel.BookId);
                        if (book != null)
                        {
                            bookAuthorModel.Book = _mapper.Map<BookModel>(book);
                        }
                    }
                }

                // Trả về danh sách đã ánh xạ
                return Ok(bookAuthorModels);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về lỗi 500
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }



        #endregion


        #region Get Book Author By Id

        // GET: api/bookauthors/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<BookAuthor>> GetBookAuthor(int id)
        {
          if (_context.BookAuthors == null)
          {
              return NotFound();
          }
            var bookAuthor = await _context.BookAuthors.FindAsync(id);

            if (bookAuthor == null)
            {
                return NotFound();
            }

            return bookAuthor;
        }*/
        [HttpGet("{id}")]
        public async Task<ActionResult<BookAuthorModel>> GetBookAuthorById(int id, [FromQuery] bool includeBook = false)
        {
            try
            {
                // Lấy thông tin của một BookAuthor dựa trên Id từ cơ sở dữ liệu
                var bookAuthor = _unitOfWork.BookAuthorRepository.GetByID(id);

                if (bookAuthor == null)
                {
                    return NotFound();
                }

                // Ánh xạ thông tin từ BookAuthorEntity sang BookAuthorModel
                var bookAuthorModel = _mapper.Map<BookAuthorModel>(bookAuthor);

                if (includeBook)
                {
                    // Nếu includeBook được đặt là true, lấy thông tin về sách từ cơ sở dữ liệu và ánh xạ sang BookModel
                    var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookAuthor.BookId);
                    if (book != null)
                    {
                        bookAuthorModel.Book = _mapper.Map<BookModel>(book);
                    }
                }

                // Trả về thông tin của BookAuthor đã ánh xạ
                return Ok(bookAuthorModel);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về lỗi 500
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }




        #endregion


        #region Update Book Author
        // PUT: api/bookauthors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutBookAuthor(int id, BookAuthor bookAuthor)
         {
             if (id != bookAuthor.Id)
             {
                 return BadRequest();
             }

             _context.Entry(bookAuthor).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!BookAuthorExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return NoContent();
         }*/

        [HttpPut("{id}")]
        public IActionResult UpdateBookAuthor(int id, AddNewBookAuthorModel model)
        {
            // Find the BookAuthor by id
            var bookAuthor = _context.BookAuthors.FirstOrDefault(ba => ba.Id == id);

            // If the BookAuthor is not found, return NotFound
            if (bookAuthor == null)
            {
                return NotFound("BookAuthor not found.");
            }

            // Check if BookId and AuthorId are provided
            if (model.BookId == null || model.AuthorId == null)
            {
                return BadRequest("BookId and AuthorId must be provided.");
            }

            // Retrieve the book and author from the database
            var book = _context.Books.FirstOrDefault(b => b.Id == model.BookId);
            var author = _context.Authors.FirstOrDefault(a => a.Id == model.AuthorId);

            // If either book or author is not found, return NotFound
            if (book == null || author == null)
            {
                return NotFound("Book or Author not found.");
            }

            // Update the properties of the BookAuthor
            bookAuthor.BookId = model.BookId.Value;
            bookAuthor.AuthorId = model.AuthorId.Value;
            bookAuthor.AuthorOrder = model.AuthorOrder;
            bookAuthor.RoyaltyPer = model.RoyaltyPer;

            // Save changes to the database
            _context.SaveChanges();

            // Return success response
            return Ok("BookAuthor updated successfully.");
        }


        #endregion


        #region Create New Book Author
        // POST: api/bookauthors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*        [HttpPost]
                public async Task<ActionResult<BookAuthor>> PostBookAuthor(BookAuthor bookAuthor)
                {
                  if (_context.BookAuthors == null)
                  {
                      return Problem("Entity set 'Ass2Prn231Context.BookAuthors'  is null.");
                  }
                    _context.BookAuthors.Add(bookAuthor);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetBookAuthor", new { id = bookAuthor.Id }, bookAuthor);
                }*/
        [HttpPost]
        public IActionResult AddNewBookAuthor(AddNewBookAuthorModel model)
        {
            // Check if BookId and AuthorId are provided
            if (model.BookId == null || model.AuthorId == null)
            {
                return BadRequest("BookId and AuthorId must be provided.");
            }

            // Retrieve the book and author from the database
            var book = _context.Books.FirstOrDefault(b => b.Id == model.BookId);
            var author = _context.Authors.FirstOrDefault(a => a.Id == model.AuthorId);

            // If either book or author is not found, return NotFound
            if (book == null || author == null)
            {
                return NotFound("Book or Author not found.");
            }

            // Create a new BookAuthor instance
            var newBookAuthor = new BookAuthor
            {
                BookId = model.BookId.Value,
                AuthorId = model.AuthorId.Value,
                AuthorOrder = model.AuthorOrder,
                RoyaltyPer = model.RoyaltyPer
            };

            // Add the new BookAuthor to the database
            _context.BookAuthors.Add(newBookAuthor);
            _context.SaveChanges();

            // Return success response
            return Ok("BookAuthor added successfully.");
        }




        #endregion


        #region Delete Book Author 
        // DELETE: api/bookauthors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAuthor(int id)
        {
            if (_context.BookAuthors == null)
            {
                return NotFound();
            }
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




        #region Side Method
        private bool BookAuthorExists(int id)
        {
            return (_context.BookAuthors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}
