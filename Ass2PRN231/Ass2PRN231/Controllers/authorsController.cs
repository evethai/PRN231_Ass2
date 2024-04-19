using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using AutoMapper;
using Repository;
using Repository.Models;

namespace Ass2PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authorsController : ControllerBase
    {
        private readonly Ass2Prn231Context _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private int? pageSize = 10;
        private int? currentPage = 1;

        public authorsController(Ass2Prn231Context context, UnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        #region Get All Authors List
        /*        // GET: api/authors
                [HttpGet]
                public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
                {
                  if (_context.Authors == null)
                  {
                      return NotFound();
                  }
                    return await _context.Authors.ToListAsync();
                }*/
        /*        [HttpGet]
                public async Task<ActionResult<IEnumerable<AuthorModel>>> GetAuthors([FromQuery] bool includeBookAuthors = false, [FromQuery] bool includeBooks = false)
                {
                    try
                    {
                        IQueryable<Author> query = _context.Authors;

                        if (includeBookAuthors)
                        {
                            query = query.Include(a => a.BookAuthors);
                            if (includeBooks)
                            {
                                query = query.Include(a => a.BookAuthors).ThenInclude(ba => ba.Book);
                            }
                        }
                        else if (includeBooks)
                        {
                            // Trường hợp chỉ include thông tin về sách mà không include thông tin về tác giả sách
                            return BadRequest("Cannot include Books without including BookAuthors.");
                        }

                        var authors = await query.ToListAsync();

                        if (authors == null)
                        {
                            return NotFound();
                        }

                        if (!includeBookAuthors && !includeBooks)
                        {
                            // Trường hợp không bao gồm thông tin về sách hoặc tác giả sách
                            var simplifiedAuthors = authors.Select(author => new AuthorModel
                            {
                                Id = author.Id,
                                LastName = author.LastName,
                                FirstName = author.FirstName,
                                Phone = author.Phone,
                                Address = author.Address,
                                City = author.City,
                                State = author.State,
                                Zip = author.Zip,
                                Email = author.Email
                            }).ToList();

                            return Ok(simplifiedAuthors);
                        }

                        return Ok(authors.Select(a => _mapper.Map<AuthorModel>(a)));
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi và trả về phản hồi lỗi 500 kèm theo thông báo
                        return StatusCode(500, "Internal server error: " + ex.Message);
                    }
                }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> GetAuthors([FromQuery] bool includeBookAuthors = false, [FromQuery] bool includeBooks = false)
        {
            try
            {
                IQueryable<Author> query = _context.Authors;

                if (includeBookAuthors)
                {
                    query = query.Include(a => a.BookAuthors);
                    if (includeBooks)
                    {
                        query = query.Include(a => a.BookAuthors).ThenInclude(ba => ba.Book);
                    }
                }
                else if (includeBooks)
                {
                    // Trường hợp chỉ include thông tin về sách mà không include thông tin về tác giả sách
                    return BadRequest("Cannot include Books without including BookAuthors.");
                }

                var authors = await query.ToListAsync();

                if (authors == null)
                {
                    return NotFound();
                }

                if (!includeBookAuthors && !includeBooks)
                {
                    // Trường hợp không bao gồm thông tin về sách hoặc tác giả sách
                    var simplifiedAuthors = authors.Select(author => new AuthorModel
                    {
                        Id = author.Id,
                        LastName = author.LastName,
                        FirstName = author.FirstName,
                        Phone = author.Phone,
                        Address = author.Address,
                        City = author.City,
                        State = author.State,
                        Zip = author.Zip,
                        Email = author.Email
                    }).ToList();

                    return Ok(simplifiedAuthors);
                }

                // Ánh xạ từ Author sang AuthorModel
                var authorModels = _mapper.Map<List<AuthorModel>>(authors);

                if (includeBookAuthors)
                {
                    // Đổ dữ liệu từ BookAuthors vào AuthorModel
                    foreach (var authorModel in authorModels)
                    {
                        var author = authors.FirstOrDefault(a => a.Id == authorModel.Id);
                        if (author != null)
                        {
                            authorModel.BookAuthors = _mapper.Map<List<BookAuthorModel>>(author.BookAuthors);
                        }
                    }
                }

                if (includeBooks)
                {
                    // Đổ dữ liệu từ Books vào BookAuthors trong AuthorModel
                    foreach (var authorModel in authorModels)
                    {
                        foreach (var bookAuthor in authorModel.BookAuthors)
                        {
                            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookAuthor.BookId);
                            if (book != null)
                            {
                                bookAuthor.Book = _mapper.Map<BookModel>(book);
                            }
                        }
                    }
                }

                return Ok(authorModels);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về phản hồi lỗi 500 kèm theo thông báo
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }




        #endregion


        #region Get Author by Id
        // GET: api/authors/5
        /*        [HttpGet("{id}")]
                public async Task<ActionResult<Author>> GetAuthor(int id)
                {
                  if (_context.Authors == null)
                  {
                      return NotFound();
                  }
                    var author = await _context.Authors.FindAsync(id);

                    if (author == null)
                    {
                        return NotFound();
                    }

                    return author;
                }*/
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id, [FromQuery] bool includeBookAuthors = false, [FromQuery] bool includeBooks = false)
        {
            try
            {
                IQueryable<Author> query = _context.Authors;

                if (includeBookAuthors)
                {
                    query = query.Include(a => a.BookAuthors);
                    if (includeBooks)
                    {
                        query = query.Include(a => a.BookAuthors).ThenInclude(ba => ba.Book);
                    }
                }
                else if (includeBooks)
                {
                    // Trường hợp chỉ include thông tin về sách mà không include thông tin về tác giả sách
                    return BadRequest("Cannot include Books without including BookAuthors.");
                }

                var author = await query.FirstOrDefaultAsync(a => a.Id == id);

                if (author == null)
                {
                    return NotFound();
                }

                if (!includeBookAuthors && !includeBooks)
                {
                    // Trường hợp không bao gồm thông tin về sách hoặc tác giả sách
                    var simplifiedAuthor = new Author
                    {
                        Id = author.Id,
                        LastName = author.LastName,
                        FirstName = author.FirstName,
                        Phone = author.Phone,
                        Address = author.Address,
                        City = author.City,
                        State = author.State,
                        Zip = author.Zip,
                        Email = author.Email
                    };

                    return Ok(simplifiedAuthor);
                }

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                return Ok(JsonSerializer.Serialize(author, options));
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về phản hồi lỗi 500 kèm theo thông báo
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }*/
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorModel>> GetAuthorById(int id, [FromQuery] bool includeBookAuthors = false, [FromQuery] bool includeBooks = false)
        {
            try
            {
                IQueryable<Author> query = _context.Authors;

                if (includeBookAuthors)
                {
                    query = query.Include(a => a.BookAuthors);
                    if (includeBooks)
                    {
                        query = query.Include(a => a.BookAuthors).ThenInclude(ba => ba.Book);
                    }
                }
                else if (includeBooks)
                {
                    // Trường hợp chỉ include thông tin về sách mà không include thông tin về tác giả sách
                    return BadRequest("Cannot include Books without including BookAuthors.");
                }

                var author = await query.FirstOrDefaultAsync(a => a.Id == id);

                if (author == null)
                {
                    return NotFound();
                }

                if (!includeBookAuthors && !includeBooks)
                {
                    // Trường hợp không bao gồm thông tin về sách hoặc tác giả sách
                    var simplifiedAuthor = _mapper.Map<AuthorModel>(author);
                    return Ok(simplifiedAuthor);
                }

                // Ánh xạ từ Author sang AuthorModel
                var authorModel = _mapper.Map<AuthorModel>(author);

                if (includeBookAuthors)
                {
                    // Đổ dữ liệu từ BookAuthors vào AuthorModel
                    authorModel.BookAuthors = _mapper.Map<List<BookAuthorModel>>(author.BookAuthors);
                }

                if (includeBooks)
                {
                    // Đổ dữ liệu từ Books vào BookAuthors trong AuthorModel
                    foreach (var bookAuthor in authorModel.BookAuthors)
                    {
                        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookAuthor.BookId);
                        if (book != null)
                        {
                            bookAuthor.Book = _mapper.Map<BookModel>(book);
                        }
                    }
                }

                return Ok(authorModel);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về phản hồi lỗi 500 kèm theo thông báo
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        #endregion


        #region Update Author By Id
        // PUT: api/authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*  [HttpPut("{id}")]
          public async Task<IActionResult> PutAuthor(int id, Author author)
          {
              if (id != author.Id)
              {
                  return BadRequest();
              }

              _context.Entry(author).State = EntityState.Modified;

              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!AuthorExists(id))
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

        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutAuthor(int id, Author author)
         {
             if (id != author.Id)
             {
                 return BadRequest("Author ID mismatch.");
             }

             // Truy vấn tác giả từ cơ sở dữ liệu
             var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

             if (existingAuthor == null)
             {
                 return NotFound("Author not found.");
             }

             // Kiểm tra xem số điện thoại mới có đúng định dạng hay không
             if (!string.IsNullOrEmpty(author.Phone) && !IsValidPhoneNumber(author.Phone))
             {
                 return BadRequest("Invalid phone number format.");
             }

             // Kiểm tra xem số điện thoại mới đã tồn tại trong cơ sở dữ liệu hay chưa
             var duplicatePhoneNumber = await _context.Authors.AnyAsync(a => a.Id != id && a.Phone == author.Phone);
             if (duplicatePhoneNumber)
             {
                 return BadRequest("Phone number already exists.");
             }

             // Kiểm tra xem email có được nhập và có đúng định dạng hay không
             if (string.IsNullOrWhiteSpace(author.Email) || !IsValidEmail(author.Email))
             {
                 return BadRequest("Invalid email format.");
             }

             // Kiểm tra xem email mới đã tồn tại trong cơ sở dữ liệu hay chưa
             var duplicateEmail = await _context.Authors.AnyAsync(a => a.Id != id && a.Email == author.Email);
             if (duplicateEmail)
             {
                 return BadRequest("Email already exists.");
             }

             // Cập nhật thông tin của tác giả
             _context.Entry(existingAuthor).CurrentValues.SetValues(author);

             try
             {
                 // Lưu thay đổi vào cơ sở dữ liệu
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!AuthorExists(id))
                 {
                     return NotFound("Author not found.");
                 }
                 else
                 {
                     throw;
                 }
             }

             return NoContent();
         }*/

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorModel authorModel)
        {
            if (id != authorModel.Id)
            {
                return BadRequest("Author ID mismatch.");
            }

            // Truy vấn tác giả từ cơ sở dữ liệu
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (existingAuthor == null)
            {
                return NotFound("Author not found.");
            }

            // Kiểm tra xem số điện thoại mới có đúng định dạng hay không
            if (!string.IsNullOrEmpty(authorModel.Phone) && !IsValidPhoneNumber(authorModel.Phone))
            {
                return BadRequest("Invalid phone number format.");
            }

            // Kiểm tra xem số điện thoại mới đã tồn tại trong cơ sở dữ liệu hay chưa
            var duplicatePhoneNumber = await _context.Authors.AnyAsync(a => a.Id != id && a.Phone == authorModel.Phone);
            if (duplicatePhoneNumber)
            {
                return BadRequest("Phone number already exists.");
            }

            // Kiểm tra xem email có được nhập và có đúng định dạng hay không
            if (string.IsNullOrWhiteSpace(authorModel.Email) || !IsValidEmail(authorModel.Email))
            {
                return BadRequest("Invalid email format.");
            }

            // Kiểm tra xem email mới đã tồn tại trong cơ sở dữ liệu hay chưa
            var duplicateEmail = await _context.Authors.AnyAsync(a => a.Id != id && a.Email == authorModel.Email);
            if (duplicateEmail)
            {
                return BadRequest("Email already exists.");
            }

            // Ánh xạ từ AuthorModel sang Author
            var updatedAuthor = _mapper.Map<Author>(authorModel);

            // Cập nhật thông tin của tác giả
            _context.Entry(existingAuthor).CurrentValues.SetValues(updatedAuthor);

            try
            {
                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound("Author not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        #endregion


        #region Add New Author
        // POST: api/authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*        [HttpPost]
                public async Task<ActionResult<Author>> PostAuthor(Author author)
                {
                  if (_context.Authors == null)
                  {
                      return Problem("Entity set 'Ass2Prn231Context.Authors'  is null.");
                  }
                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
                }*/

        /*        [HttpPost]
                public async Task<ActionResult<Author>> PostAuthor(Author author)
                {
                    try
                    {
                        // Kiểm tra xem đã có tác giả nào trùng thông tin chưa
                        var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a =>
                            a.FirstName == author.FirstName &&
                            a.LastName == author.LastName &&
                            a.Phone == author.Phone);

                        if (existingAuthor != null)
                        {
                            // Trả về lỗi khi thông tin tác giả trùng
                            ModelState.AddModelError("DuplicateAuthor", $"Author with the same first name '{author.FirstName}', last name '{author.LastName}', and phone '{author.Phone}' already exists.");
                            return ValidationProblem(ModelState);
                        }

                        _context.Authors.Add(author);
                        await _context.SaveChangesAsync();

                        // Trả về phản hồi thành công và thông tin của tác giả đã được thêm vào cơ sở dữ liệu
                        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi và trả về phản hồi lỗi 500 kèm theo thông báo
                        return StatusCode(500, "Internal server error: " + ex.Message);
                    }
                }*/

        [HttpPost]
        public async Task<ActionResult<AuthorModel>> PostAuthor(AddNewAuthorModel author)
        {
            try
            {
                // Kiểm tra xem đã có tác giả nào trùng họ và tên hay không
                var existingAuthorByName = await _context.Authors.FirstOrDefaultAsync(a =>
                    a.FirstName == author.FirstName &&
                    a.LastName == author.LastName);

                if (existingAuthorByName != null)
                {
                    // Trả về lỗi khi tên tác giả đã tồn tại
                    ModelState.AddModelError("DuplicateAuthorName", $"Author with the same first name '{author.FirstName}' and last name '{author.LastName}' already exists.");
                }
                // Kiểm tra xem đã có tác giả nào trùng số điện thoại hay không
                var existingAuthorByPhone = await _context.Authors.FirstOrDefaultAsync(a =>
                    a.Phone == author.Phone);

                if (existingAuthorByPhone != null)
                {
                    // Trả về lỗi khi số điện thoại của tác giả đã tồn tại
                    ModelState.AddModelError("DuplicateAuthorPhone", $"Author with the same phone number '{author.Phone}' already exists.");
                }
                if (!string.IsNullOrEmpty(author.Phone) && author.Phone.Length > 12)
                {
                    ModelState.AddModelError("PhoneLength", "Phone number must not exceed 12 characters.");
                }
                if (!string.IsNullOrEmpty(author.Phone) && !IsValidPhoneNumber(author.Phone))
                {
                    ModelState.AddModelError("InvalidPhoneNumber", "Invalid phone number format.");
                }
                // Kiểm tra xem đã có tác giả nào trùng email hay không
                var existingAuthorByEmail = await _context.Authors.FirstOrDefaultAsync(a =>
                    a.Email == author.Email);

                if (existingAuthorByEmail != null)
                {
                    // Trả về lỗi khi email của tác giả đã tồn tại
                    ModelState.AddModelError("DuplicateAuthorEmail", $"Author with the same email '{author.Email}' already exists.");
                } else
                // Kiểm tra xem email có được nhập vào không và có đúng định dạng không
                if (string.IsNullOrEmpty(author.Email))
                {
                    ModelState.AddModelError("EmailRequired", "Email is required.");
                }
                else if (!IsValidEmail(author.Email))
                {
                    ModelState.AddModelError("InvalidEmailFormat", "Invalid email format.");
                }

                //////////////////////////////////////////////////////////////

                //Nếu đếm thấy có lỗi thì sẽ trả về lỗi luôn
                if (ModelState.ErrorCount > 0)
                {
                    return ValidationProblem(ModelState);
                }

                // Ánh xạ từ AddNewAuthorModel sang Author
                var newAuthor = _mapper.Map<Author>(author);

                _context.Authors.Add(newAuthor);
                await _context.SaveChangesAsync();

                // Trả về phản hồi thành công và thông tin của tác giả đã được thêm vào cơ sở dữ liệu
                var addedAuthorModel = _mapper.Map<AuthorModel>(newAuthor);
                return CreatedAtAction(nameof(GetAuthorById), new { id = addedAuthorModel.Id }, addedAuthorModel);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về phản hồi lỗi 500 kèm theo thông báo
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }



        #endregion


        #region Delete Author By Id
        // DELETE: api/authors/5
        /*        [HttpDelete("{id}")]
                public async Task<IActionResult> DeleteAuthor(int id)
                {
                    if (_context.Authors == null)
                    {
                        return NotFound();
                    }
                    var author = await _context.Authors.FindAsync(id);
                    if (author == null)
                    {
                        return NotFound();
                    }

                    _context.Authors.Remove(author);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }*/


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                // Kiểm tra xem tác giả có tồn tại không
                var authorToDelete = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
                if (authorToDelete == null)
                {
                    return NotFound("Author not found.");
                }

                // Ánh xạ tác giả sang AuthorModel để xóa các thông tin liên quan
                var authorDto = _mapper.Map<AuthorModel>(authorToDelete);

                // Xóa các thông tin liên quan từ bảng BookAuthorModel
                foreach (var bookAuthor in authorDto.BookAuthors)
                {
                    var bookAuthorToDelete = await _context.BookAuthors.FindAsync(bookAuthor.Id);
                    if (bookAuthorToDelete != null)
                    {
                        _context.BookAuthors.Remove(bookAuthorToDelete);
                    }
                }

                // Lấy danh sách ID của các sách để xóa
                var bookIds = authorDto.BookAuthors.Select(ba => ba.BookId).ToList();

                // Xóa các sách từ bảng BookModel
                foreach (var bookId in bookIds)
                {
                    var bookToDelete = await _context.Books.FindAsync(bookId);
                    if (bookToDelete != null)
                    {
                        _context.Books.Remove(bookToDelete);
                    }
                }

                // Xóa tác giả từ bảng AuthorModel
                _context.Authors.Remove(authorToDelete);

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();

                return Ok("Author deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to delete author: " + ex.Message);
            }
        }


        #endregion

        #region Side Method
        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Biểu thức chính quy để kiểm tra số điện thoại
            string pattern = @"^[0-9()+-]*$";

            // Kiểm tra xem số điện thoại có khớp với biểu thức chính quy không
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }
            #endregion
    }
}
