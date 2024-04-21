using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Entity;
using Repository.Models;

namespace Ass2PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        private int? pageSize = 10;
        private int? currentPage = 1;

        public usersController( UnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody] Login login)
        {
            var user = _unitOfWork.UserRepository.Get(m => m.Email.Equals(login.Email) && m.Password.Equals(login.Password)).FirstOrDefault();
            if (user == null) {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid email or password"
                });
            }
            //config token
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Login successfully",
                Data = GenerateToken(user)
            });
            
        }
        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                    new Claim("Name", user.FirstName + " "+ user.LastName),
                    new Claim ("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return  jwtTokenHandler.WriteToken(token);
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
            return (_unitOfWork.UserRepository.Get()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
