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
    public class Book_GenreController : Controller {
        private string ConnectionString { get; set; }
        
        // Konstruktor
        public Book_GenreController()
        {
            this.ConnectionString = Utils.GetConnectionString();
        }

        // [POST] api/book_genre
        [HttpPost]
        public async Task<IActionResult> Create(Book_Genre book_genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                await db.Book_Genre.AddAsync(book_genre);
                await db.SaveChangesAsync();

                return Created($"/api/book_genre/{book_genre.Id}", book_genre);
            }
        }

        // [DELETE] api/book_genre/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var db = new SakurAniLibContext(this.ConnectionString))
            {
                var book_genre = await db.Book_Genre.FirstOrDefaultAsync(b => b.Id == id);

                if (book_genre == null) 
                {
                    return NotFound();
                }

                db.Book_Genre.Remove(book_genre);
                await db.SaveChangesAsync();

                return new NoContentResult();
            }
        }
    }
}