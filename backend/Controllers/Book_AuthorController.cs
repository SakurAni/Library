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
    public class Book_AuthorController : Controller {
        private string ConnectionString { get; set; }
        
        // Konstruktor
        public Book_AuthorController()
        {
            this.ConnectionString = Utils.GetConnectionString();
        }

        // [POST] api/book_author
        [HttpPost]
        public async Task<IActionResult> Create(Book_Author book_author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                await db.Book_Author.AddAsync(book_author);
                await db.SaveChangesAsync();

                return Created($"/api/book_author/{book_author.Id}", book_author);
            }
        }

        // [DELETE] api/book_author/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var db = new SakurAniLibContext(this.ConnectionString))
            {
                var book_author = await db.Book_Author.FirstOrDefaultAsync(b => b.Id == id);

                if (book_author == null) 
                {
                    return NotFound();
                }

                db.Book_Author.Remove(book_author);
                await db.SaveChangesAsync();

                return new NoContentResult();
            }
        }
    }
}