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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetPublishers([FromQuery] SearchUserModel search)
        {
            if (search.pageSize != null) {
                pageSize = search.pageSize;
            }
            if (search.currentPage != null) {
                currentPage = search.currentPage;
            }
            Expression<Func<User, bool>> filter = null;
            if (search.email != null) {
                filter = filter.And(p => p.Email == search.email);
            }
            if (search.minDate.HasValue || search.maxDate.HasValue) {
                if (filter == null) {
                    filter = p => true;
                }
                if (search.minDate.HasValue) {
                    filter = filter.And(p => p.HireDate >= search.minDate);
                }
                if (search.maxDate.HasValue) {
                    filter = filter.And(p => p.HireDate <= search.maxDate);
                }
            }
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null;
            var publishers = _unitOfWork.UserRepository.Get(filter, null, "", currentPage, pageSize).ToList();
            var result = _mapper.Map<IEnumerable<UserModel>>(publishers);

            return Ok(result);

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
        public async Task<IActionResult> PutUser(int id, UserUpdateModel user)
        {
            if (ModelState.IsValid == false) {
                return BadRequest(ModelState);
            }
            var u = _unitOfWork.UserRepository.GetByID(id);
            if (u == null) {
                return NotFound();
            }
            
            u.Email = user.Email;
            u.Password = user.Password;
            u.Source = user.Source;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.RoleId = user.RoleId;
            u.PubId = user.PubId;
            u.HireDate = user.HireDate;

           

            try {
                _unitOfWork.UserRepository.Update(u);
                _unitOfWork.Save();
                return  Ok(u);
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
        public async Task<ActionResult<UserUpdateModel>> PostUser([FromForm] UserUpdateModel user)
        {

            if (ModelState.IsValid == false) {
                return BadRequest(ModelState);
            }
            var u = _mapper.Map<User>(user);

            try {
                _unitOfWork.UserRepository.Insert(u);
                _unitOfWork.Save();
                return Ok(user);
            }
            catch(DbUpdateConcurrencyException) {
                return BadRequest(ModelState);
            }
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
