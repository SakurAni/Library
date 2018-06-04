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
    public class BookController : Controller {
        private string ConnectionString { get; set; }

        public BookController()
        {
            this.ConnectionString = Utils.GetConnectionString();
        }

        // [GET] api/book
        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                return await db.Book.ToListAsync();
            }
        }

        // [GET] api/book/{isbn}
        [Route("{isbn}")]
        public async Task<IActionResult> GetBook(string isbn) 
        {
            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                var book = await db.Book.FirstOrDefaultAsync(b => b.Isbn == isbn);

                if(book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
        }

        // [GET] api/book/author/{id}
        [Route("/author/{id}")]
        public async Task<IActionResult> GetBooksByAuthor(string id)
        {
            using (var db = new SakurAniLibContext(this.ConnectionString))
            {
                var booksByAuthor = new List<Book>();

                // Query BookAuthor Info from Intersection Table
                var bookAuthorList =  await (from b in db.Book_Author
                                            where b.AuthorId == id
                                            select b).ToListAsync();

                if(bookAuthorList.Count == 0)
                {
                    return NoContent();
                }

                // Get detailed Book info for each isbn
                foreach (var bookAuthor in bookAuthorList)
                {
                    var book = await db.Book.FirstOrDefaultAsync(b => b.Isbn == bookAuthor.Isbn);

                    booksByAuthor.Add(book);
                }

                return Ok(booksByAuthor);
            }
        }

        // [POST] api/book
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {

            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                await db.Book.AddAsync(book);
                await db.SaveChangesAsync();

                return Created($"/api/book/{book.Isbn}", book);
            }
        }

        // [PUT] api/book
        [HttpPut]
        public async Task<IActionResult> Update(Book book)
        {
            if (!ModelState.IsValid)
            {
                
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                var updatedBook = await db.Book.FirstOrDefaultAsync(b => b.Isbn == book.Isbn);

                if (updatedBook == null) 
                {
                    return NotFound();
                }

                // update values
                updatedBook.Isbn = book.Isbn;
                updatedBook.Title = book.Title;
                updatedBook.Price = book.Price;
                updatedBook.Picture = book.Picture;
                updatedBook.SeriesNumber = book.SeriesNumber;
                updatedBook.Currency = book.Currency;
                updatedBook.Type = book.Type;

                // Save
                await db.SaveChangesAsync();

                return Ok(book);
            }
        }

        // [DELETE] api/book/{isbn}
        [HttpDelete]
        [Route("{isbn}")]
        public async Task<IActionResult> Delete(string isbn)
        {
            using (var db = new SakurAniLibContext(this.ConnectionString))
            {
                var book = await db.Book.FirstOrDefaultAsync(b => b.Isbn == isbn);

                if (book == null) 
                {
                    return NotFound();
                }

                db.Book.Remove(book);
                await db.SaveChangesAsync();

                return new NoContentResult();
            }
        }
    }
}