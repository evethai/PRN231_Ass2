using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json.Linq;
using Repository;
using Repository.Entity;
using Repository.Models;

namespace Ass2PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class booksController : ControllerBase
    {
        private readonly Ass2Prn231Context _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private int? pageSize = 10;
        private int? currentPage = 1;

        public booksController(Ass2Prn231Context context, UnitOfWork unitOfWork,IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooks([FromQuery] BookSearchModel search)
        {
            if (search.pageSize != null)
            {
                pageSize = search.pageSize;
            }
            if (search.currentPage != null)
            {
                currentPage = search.currentPage;
            }
            Expression<Func<Book, bool>> filter = p => p.IsActive == true;
            if (search.pubId.HasValue)
            {
                filter = filter.And(p => p.PubId == search.pubId);
            }
            if (search.title != null)
            {
                filter = filter.And(p => p.Title.Contains(search.title));
            }
            if (search.type != null)
            {
                filter = filter.And(p => p.Type.Contains(search.type));
            }
            if (search.minPrice.HasValue || search.maxPrice.HasValue)
            {
                if (filter == null)
                {
                    filter = p => true;
                }
                if (search.minPrice.HasValue)
                {
                    filter = filter.And(p => p.Price >= search.minPrice);
                }
                if (search.maxPrice.HasValue)
                {
                    filter = filter.And(p => p.Price <= search.maxPrice);
                }
            }
            if(search.minDate.HasValue || search.maxDate.HasValue)
            {
                if (filter == null)
                {
                    filter = p => true;
                }
                if (search.minDate.HasValue)
                {
                    filter = filter.And(p => p.Date >= search.minDate);
                }
                if (search.maxDate.HasValue)
                {
                    filter = filter.And(p => p.Date <= search.maxDate);
                }
            }
            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null;
            if(search.sortByPrice == true && search.descending == true)
            {
                orderBy = p => p.OrderByDescending(p=>p.Price);
            }else if(search.sortByPrice == true && search.descending == false)
            {
                orderBy = p => p.OrderBy(p => p.Price);
            }else if (search.sortByDate == true && search.descending == true)
            {
                orderBy = p => p.OrderByDescending(p => p.Date);
            }else if (search.sortByDate == true && search.descending == false)
            {
                orderBy = p => p.OrderBy(p => p.Date);
            }
            else if(search.sortByPrice == false && search.sortByDate == false && search.descending == true)
            {
                orderBy = p => p.OrderByDescending(p => p.Title);
            }
            else if (search.sortByPrice == false && search.sortByDate == false && search.descending == false)
            {
                orderBy = p => p.OrderBy(p => p.Title);
            }

            string includeProperties = "Pub";
            var list = _unitOfWork.BookRepository.Get(filter,orderBy,includeProperties,currentPage,pageSize).ToList();
            var result = _mapper.Map<IEnumerable<BookModel>>(list);

            return Ok(result);
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'Ass2Prn231Context.Books'  is null.");
          }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
