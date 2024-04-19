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
    public class usersController : ControllerBase
    {
        private readonly Ass2Prn231Context _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private int? pageSize = 10;
        private int? currentPage = 1;

        public usersController(Ass2Prn231Context context, UnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody] Login login)
        {
            var user = _unitOfWork.UserRepository.Get(m => m.Email.Equals(login.Email) && m.Password.Equals(login.Password)).FirstOrDefault();
            if (user == null) {
                return BadRequest("Login error: Email or Password incorrect");
            }

            var u = _mapper.Map<UserModel>(user);
            return Ok(u);
        }


        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_unitOfWork.UserRepository == null) {
                return NotFound();
            }
            var member = _unitOfWork.UserRepository.GetByID(id);

            if (member == null) {
                return NotFound();
            }
            var result = _mapper.Map<UserModel>(member);


            return Ok(result);
        }

        // PUT: api/users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserUpdateDTO user)
        {
            var u = _unitOfWork.UserRepository.GetByID(id);
            if (u == null) {
                return NotFound();
            }
            u.Id = user.Id;
            u.Email = user.Email;
            u.Password = user.Password;
            u.Source = user.Source;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.RoleId = user.RoleId;
            u.PubId = user.PubId;
            u.HireDate = user.HireDate;

            _unitOfWork.UserRepository.Update(u);

            try {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException) {
                if (!UserExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_unitOfWork.UserRepository == null) {
                return NotFound();
            }

           
            var c = _unitOfWork.UserRepository.GetByID(user.PubId.Value);
            if (c == null) {
                return NotFound("Publisher is null");
            }
            var p = _mapper.Map<User>(user);
            _unitOfWork.UserRepository.Insert(p);
            try {
                _unitOfWork.Save();
            }
            catch (DbUpdateException ex) {
                return BadRequest(ex);
            }

            return Ok(user);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_unitOfWork.UserRepository == null) {
                return NotFound();
            }
            var user = _unitOfWork.UserRepository.GetByID(id);
            if (user == null) {
                return NotFound();
            }

            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Save();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
