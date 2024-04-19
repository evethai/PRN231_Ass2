using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Entity;
using Repository.Models;

namespace Ass2PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publishersController : ControllerBase
    {
        private readonly Ass2Prn231Context _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private int? pageSize = 10;
        private int? currentPage = 1;

        public publishersController(Ass2Prn231Context context, UnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers([FromQuery] SearchPublisherModel search)
        {
            if (search.pageSize != null) {
                pageSize = search.pageSize;
            }
            if (search.currentPage != null) {
                currentPage = search.currentPage;
            }
            Expression<Func<Publisher, bool>> filter = null;
            if (search.name != null) {
                filter = filter.And(p => p.Name == search.name);
            }
            if (search.city != null) {
                filter = filter.And(p => p.City.Contains(search.city));
            }
            if (search.country != null) {
                filter = filter.And(p => p.Country.Contains(search.country));
            }
            var publishers = _unitOfWork.PublisherRepository.Get(filter, null, "", currentPage, pageSize).ToList();
            var result = _mapper.Map<IEnumerable<PublisherModel>>(publishers);

            return Ok(result);

        }

        // GET: api/publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            if (_unitOfWork.PublisherRepository == null) {
                return NotFound();
            }
            var publisher = _unitOfWork.PublisherRepository.GetByID(id);

            if (publisher == null) {
                return NotFound();
            }
            var result = _mapper.Map<PublisherModel>(publisher);
           ;

            return Ok(result);
        }

        // PUT: api/publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
        {
            var p = _unitOfWork.PublisherRepository.GetByID(id);
            if (p == null) {
                return NotFound();
            }

            p.Id = publisher.Id;
            p.Name = publisher.Name;
            p.City = publisher.City;
            p.Country = publisher.Country;
            p.State = publisher.State;

            _unitOfWork.PublisherRepository.Update(p);

            try {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException) {
                if (!PublisherExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
            if (_unitOfWork.PublisherRepository == null) {
                return NotFound();
            }

            var p = _mapper.Map<Publisher>(publisher);
            _unitOfWork.PublisherRepository.Insert(p);
            try {
                _unitOfWork.Save();
            }
            catch (DbUpdateException ex) {
                return BadRequest(ex);
            }

            return Ok(publisher);
        }

        // DELETE: api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            if (_unitOfWork.PublisherRepository == null) {
                return NotFound();
            }
            var publisher = _unitOfWork.PublisherRepository.GetByID(id);
            if (publisher == null) {
                return NotFound();
            }

            _unitOfWork.PublisherRepository.Delete(publisher);
            _unitOfWork.Save();

            return NoContent();
        }

        private bool PublisherExists(int id)
        {
            return (_context.Publishers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
