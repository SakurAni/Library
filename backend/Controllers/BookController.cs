using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SakurAni_Lib.Models;
using SakurAni_Lib.Models.Database;
using SakurAni_Lib.Helper;

namespace SakurAni_Lib.Controllers { 
    [Route("api/[controller]")]
    public class BookController : Controller {
        private string ConnectionString { get; set; }

        public BookController()
        {
            this.ConnectionString = Utils.GetConnectionString();
        }

        // GET api/book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                var dbBookList = db.Book.ToList();
                var books = new List<Book>();

                foreach(var b in dbBookList)
                {
                    // Query authors
                    var bookAuthors = (from ba in db.Book_Author
                                        where ba.Isbn == b.Isbn
                                        select ba).ToList();

                    var book = new Book {
                        Isbn = b.Isbn,
                        Title = b.Title,
                        Price = b.Price,
                        Picture = b.Picture,
                        SeriesNumber = b.SeriesNumber,
                        Currency = b.Currency,
                        Type = b.Type
                    };

                    var authorList = new List<Author>();

                    foreach(var ba in bookAuthors)
                    {
                        var author = (from a in db.Author
                                      where a.Id == ba.AuthorId
                                      select a).FirstOrDefault();

                        authorList.Add(new Author{
                            Id = author.Id,
                            Name = author.Name,
                            IsArtist = author.IsArtist
                        });
                    }

                    book.Author = authorList;

                    books.Add(book);
                }

                return books;
            }
        }

        [Route("{isbn}")]
        public Book GetBook(int isbn) 
        {
            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                return null;
                // return (from b in db.Book
                //         where b.Isbn == isbn
                //         select b).FirstOrDefault();
            }
        }

        // TODO: RFP
        [Route("connectionString")]
        public string GetConnectionString()
        {
            return this.ConnectionString;
        }
    }
}