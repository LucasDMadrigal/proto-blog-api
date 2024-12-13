﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proto_blog_api.DTOs;
using proto_blog_api.Models;

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
        public async Task<IEnumerable<PublicationDto>> Get() => await
            _context.Publications.Select(p => new PublicationDto 
            {  
                Id = p.Id, 
                Title = p.Title, 
                Content = p.Content
            }).ToListAsync();

        [HttpGet("{id:int}")]
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
        public async Task<ActionResult<PublicationDto>> Post(PublicationInsertDto publicationInsertDto)
        {
            var publication = new Publications();
            publication.Title = publicationInsertDto.Title;
            publication.Content = publicationInsertDto.Content;
            publication.Deleted = false;
            
            await _context.Publications.AddAsync(publication);
            await _context.SaveChangesAsync();

            var publicationDto = new PublicationDto();

            publicationDto.Id = publication.Id;
            publicationDto.Title = publication.Title;
            publicationDto.Content = publication.Content;

            return CreatedAtAction(nameof(GetById), new {id =  publication.Id}, publicationDto);
            
        }
    }
}