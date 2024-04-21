using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Repository.Entity;
using Repository;
using Repository.Models.AuthorModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Ass2PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorsController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAuthorModel>>> GetAuthors([FromQuery] int? page = 1)
        {
            const int pageSize = 10;
            var skip = (page.Value - 1) * pageSize;

            var authors = _unitOfWork.AuthorRepository.Get(
                filter: null, // No need for IsActive filter if it's not available
                orderBy: null,
                includeProperties: "",
                pageIndex: page,
                pageSize: pageSize
            );

            var authorModels = _mapper.Map<IEnumerable<GetAuthorModel>>(authors);

            return Ok(authorModels);
        }

        // GET: api/authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAuthorModel>> GetAuthor(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorModel = _mapper.Map<GetAuthorModel>(author);

            return Ok(authorModel);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult<GetAuthorModel>> PostAuthor([FromBody] GetAuthorModel authorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = _mapper.Map<Author>(authorModel);

            _unitOfWork.AuthorRepository.Insert(author);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, authorModel);
        }

        // PUT: api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, [FromBody] GetAuthorModel authorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAuthor = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

            if (existingAuthor == null)
            {
                return NotFound();
            }

            _mapper.Map(authorModel, existingAuthor);

            _unitOfWork.AuthorRepository.Update(existingAuthor);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            _unitOfWork.AuthorRepository.Delete(author);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        // GET: api/authors/search?name={name}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<GetAuthorModel>>> SearchAuthorsByName([FromQuery] string name)
        {
            // Nếu tên tác giả để tìm kiếm là null hoặc rỗng, trả về BadRequest
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Tên tác giả không được trống.");
            }

            // Tìm kiếm tác giả theo tên
            var authors = _unitOfWork.AuthorRepository.Get(filter: a => a.FirstName.Contains(name),
                                                            orderBy: null,
                                                            includeProperties: "",
                                                            pageIndex: null,
                                                            pageSize: null);


            // Nếu không tìm thấy tác giả nào, trả về NotFound
            if (authors == null || !authors.Any())
            {
                return NotFound("Không tìm thấy tác giả.");
            }

            var authorModels = _mapper.Map<IEnumerable<GetAuthorModel>>(authors);

            return Ok(authorModels);
        }
    }
}
