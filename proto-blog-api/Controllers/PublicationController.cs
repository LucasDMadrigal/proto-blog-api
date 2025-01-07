using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proto_blog_api.DTOs;
using proto_blog_api.Models;
using proto_blog_api.Utils;
using System.Linq;

namespace proto_blog_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private StoreContext _context;
        public PublicationController(StoreContext context)
        {
            _context = context;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<PublicationDto>> Get() => await
            _context.Publications.Select(p => new PublicationDto 
            {  
                Id = p.Id, 
                Title = p.Title, 
                Content = p.Content
            }).ToListAsync();

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<PublicationDto>> GetById(int id)
        {
            var publication = await _context.Publications.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
             var publicationDto = new PublicationDto
            {
                Id = publication.Id,
                Title = publication.Title,
                Content = publication.Content
            };

            return Ok(publicationDto);
        }

        [HttpPost]
        [Authorize(Roles =nameof(UserRole.Author))]
        public async Task<ActionResult<PublicationDto>> Post(PublicationInsertDto publicationInsertDto)
        {
            var publication = new Publications();
            publication.Title = publicationInsertDto.Title;
            publication.Content = publicationInsertDto.Content;

            if (publicationInsertDto.AuthorIds != null && publicationInsertDto.AuthorIds.Any())
            {
                var existingUsers = await _context.Users.Where( u =>
                publicationInsertDto.AuthorIds.Contains(u.Id)).ToListAsync();

                var existingUsersIds = existingUsers.Select(u => u.Id);

                var invalidUsers = publicationInsertDto.AuthorIds.Except(existingUsersIds);

                if (invalidUsers.Any())
                {
                    return BadRequest(
                        new
                        {
                            Message = "Algunos Ids no son validos",
                            invalidAuthors = invalidUsers
                        });
                }
                publication.Authors = existingUsers;
            }

            await _context.Publications.AddAsync(publication);
            await _context.SaveChangesAsync();

            var publicationDto = new PublicationDto();
            publicationDto.Title = publication.Title;
            publicationDto.Content = publication.Content;
            publicationDto.Id = publication.Id;

            return CreatedAtAction(nameof(GetById), new { id = publication.Id }, publicationDto);
        }
    }
}
