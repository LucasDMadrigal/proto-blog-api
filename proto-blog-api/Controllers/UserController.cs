using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proto_blog_api.DTOs;
using proto_blog_api.Models;
using proto_blog_api.Utils;

namespace proto_blog_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private StoreContext _context;

        public UserController(StoreContext storeContext)
        {
            _context = storeContext;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get() => await
            _context.Users.Select(u => new UserDto()
            {
                Id = u.Id,
                Username = u.Username,
                //Password = u.Password,
                Role = u.Role

            }).ToListAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto();
            userDto.Id = user.Id;
            userDto.Id = user.Id;
            user.Role = userDto.Role;

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post(UserInsertDto userInsertDto)
        {

            var userExist = await _context.Users.AnyAsync(u => u.Username == userInsertDto.Username);

            if (userExist)
            {
                return BadRequest(new { Message = "user exist" });
            }

            PasswordUtils.CreatePasswordHash(userInsertDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User();
            user.Username = userInsertDto.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto();
            userDto.Id = user.Id;
            userDto.Username = user.Username;
            userDto.Role = user.Role;

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, userDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDto>> Update(int id, UserUpdateDto userUpdateDto)
        {
            var userToUpdate = await _context.Users.FindAsync(id);

            if(userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.Username = userUpdateDto.Username;
            userToUpdate.Role = userUpdateDto.Role;

            await _context.SaveChangesAsync();

            var userDto = new UserDto();
            userDto.Id = userToUpdate.Id;
            userDto.Username = userToUpdate.Username;
            userDto.Role = userToUpdate.Role;

            return Ok(userDto);
        }
    }
}
