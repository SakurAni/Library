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
    public class Series_BookController : Controller {
        private string ConnectionString { get; set; }
        
        // Konstruktor
        public Series_BookController()
        {
            this.ConnectionString = Utils.GetConnectionString();
        }

        // [POST] api/series_book
        [HttpPost]
        public async Task<IActionResult> Create(Series_Book series_book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                await db.Series_Book.AddAsync(series_book);
                await db.SaveChangesAsync();

                return Created($"/api/series_book/{series_book.Id}", series_book);
            }
        }

        // [DELETE] api/series_book/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var db = new SakurAniLibContext(this.ConnectionString))
            {
                var series_book = await db.Series_Book.FirstOrDefaultAsync(b => b.Id == id);

                if (series_book == null) 
                {
                    return NotFound();
                }

                db.Series_Book.Remove(series_book);
                await db.SaveChangesAsync();

                return new NoContentResult();
            }
        }
    }
}