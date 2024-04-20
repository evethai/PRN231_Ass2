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
        //public async Task<ActionResult<IEnumerable<BookModel>>> GetBooks([FromQuery] BookSearchModel search)
        public async Task<ActionResult<BookModelResponse>> GetBooks([FromQuery] BookSearchModel search)
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
            var books = _unitOfWork.BookRepository.Get(filter,orderBy,includeProperties,currentPage,pageSize).ToList();
            var list = _mapper.Map<IEnumerable<BookModel>>(books);
            var total = _unitOfWork.BookRepository.Get(filter).Count();
            BookModelResponse result = new BookModelResponse();
            result.total = total;
            result.currentPage = currentPage.Value;
            result.books = list.ToList();

            return Ok(result);
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetBook(int id)
        {

            var book = _unitOfWork.BookRepository.GetByID(id);
            if (book == null || book.IsActive == false)
            {
                return NotFound();
            }
            var result = _mapper.Map<BookModel>(book);
            return Ok(result);
        }

        // PUT: api/books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromForm] BookUpdateModel newBook)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var oldBook = _unitOfWork.BookRepository.GetByID(id);
            if(oldBook == null)
            {
                return NotFound();
            }
            oldBook.Title = newBook.Title;
            oldBook.Price = newBook.Price;
            oldBook.Type = newBook.Type;
            oldBook.PubId = newBook.PubId;
            oldBook.Advance = newBook.Advance;
            oldBook.Royalty = newBook.Royalty;
            oldBook.YtdSales = newBook.YtdSales;
            oldBook.Notes = newBook.Notes;

            try
            {
                _unitOfWork.BookRepository.Update(oldBook);
                _unitOfWork.Save();
                return Ok(newBook);
                //return Ok("Update successful.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

        }

        // POST: api/books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookUpdateModel>> PostBook([FromForm] BookUpdateModel book)
        {
          if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var newBook = _mapper.Map<Book>(book);
            newBook.IsActive = true;
            newBook.Date = DateTime.Now;
            try
            {
                _unitOfWork.BookRepository.Insert(newBook);
                _unitOfWork.Save();
                return Ok(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {

            var book = _unitOfWork.BookRepository.GetByID(id);
            if(book == null)
            {
                return NotFound("Id is not existed!");
            }
            book.IsActive = false;
            _unitOfWork.BookRepository.Update(book);
            _unitOfWork.Save();
            return Ok("Delete successful.");
        }
    }
}
