using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proto_blog_api.DTOs;
using proto_blog_api.Models;

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
                Password = u.Password,
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
            var user = new User();

            user.Username = userInsertDto.Username;
            user.Password = userInsertDto.Password;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto();
            userDto.Username = user.Username;
            userDto.Id = user.Id;

            return CreatedAtAction(nameof(GetById), new { id = user.Id}, userDto);

        }

    }
}
