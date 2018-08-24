using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SakurAni_Lib.Helper;
using SakurAni_Lib.Models;

namespace SakurAni_Lib.Controllers {
    [Route("api/[controller]")]
    public class AuthorController : Controller {
        private string ConnectionString { get; set; }

        // Konstruktor
        public AuthorController()
        {
            this.ConnectionString = Utils.GetConnectionString();
        }
        
        // [GET] api/author
        [HttpGet]
        public async Task<IEnumerable<Author>> Get()
        {
            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                return await db.Author.ToListAsync();
            }
        }

        // [GET] api/author/{id}
        [Route("{id}")]
        public async Task<IActionResult> GetAuthor(string id) 
        {
            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                var author = await db.Author.FirstOrDefaultAsync(a => a.Id == id);

                if(author == null)
                {
                    return NotFound();
                }

                return Ok(author);
            }
        }

        // [POST] api/author
        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                await db.Author.AddAsync(author);
                await db.SaveChangesAsync();

                return Created($"/api/author/{author.Id}", author);
            }
        }

        // [PUT] api/Author
        [HttpPut]
        public async Task<IActionResult> Update(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                var updatedAuthor = await db.Author.FirstOrDefaultAsync(a => a.Id == author.Id);

                if (updatedAuthor == null) 
                {
                    return NotFound();
                }

                // update values
                updatedAuthor.Id = author.Id;
                updatedAuthor.Name = author.Name;
                updatedAuthor.IsArtist = author.IsArtist;

                // Save
                await db.SaveChangesAsync();

                return Ok(author);
            }
        }
      
        // [DELETE] api/author/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using (var db = new SakurAniLibContext(this.ConnectionString))
            {
                var author = await db.Author.FirstOrDefaultAsync(a => a.Id == id);

                if (author == null) 
                {
                    return NotFound();
                }

                db.Author.Remove(author);
                await db.SaveChangesAsync();

                return new NoContentResult();
            }
        }
    }
}