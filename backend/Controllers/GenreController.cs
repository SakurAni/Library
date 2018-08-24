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
    public class GenreController : Controller {
        private string ConnectionString { get; set; }

        // Konstruktor
        public GenreController()
        {
            this.ConnectionString = Utils.GetConnectionString();
        }

        // [GET] api/genre
        [HttpGet]
        public async Task<IEnumerable<Genre>> Get()
        {
            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                return await db.Genre.ToListAsync();
            }
        }

        // [POST] api/genre
        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                await db.Genre.AddAsync(genre);
                await db.SaveChangesAsync();

                return Created($"/api/genre/{genre.Id}", genre);
            }
        }

        // [PUT] api/genre
        [HttpPut]
        public async Task<IActionResult> Update(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                var updatedGenre = await db.Genre.FirstOrDefaultAsync(g => g.Id == genre.Id);

                if (updatedGenre == null) 
                {
                    return NotFound();
                }

                // update values
                updatedGenre.Id = genre.Id;
                updatedGenre.Name = genre.Name;

                // Save
                await db.SaveChangesAsync();

                return Ok(genre);
            }
        }

        // [DELETE] api/genre/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using (var db = new SakurAniLibContext(this.ConnectionString))
            {
                var genre = await db.Genre.FirstOrDefaultAsync(g => g.Id == id);

                if (genre == null) 
                {
                    return NotFound();
                }

                db.Genre.Remove(genre);
                await db.SaveChangesAsync();

                return new NoContentResult();
            }
        }
    }
}