namespace SakurAni_Lib.Controllers {
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using SakurAni_Lib.Models;
    
    [Route("api/[controller]")]
    public class BookController : Controller {

        private readonly IEnumerable<Book> bookItems;

        // Konstruktor
        public BookController() {
            bookItems = new List<Book> {
                new Book {
                    Isbn = 2,
                    Title = "Mein zweites Buch",
                    Price = 19.99,
                    Author = new List<string>{"Der Görth", "Der andere Basti"},
                    Picture = "./hier/liegt/ein/bild.png",
                    SeriesNumber = 1,
                    Currency = "EUR",
                    Type = "Roman",
                    Genres = new List<string>{"Erotik", "Horror"}
                },                
                new Book {
                    Isbn = 1,
                    Title = "Mein erstes Buch",
                    Price = 12.99,
                    Author = new List<string>{"Der Görth", "Der andere Basti"},
                    Picture = "./hier/liegt/ein/bild.png",
                    SeriesNumber = 2,
                    Currency = "EUR",
                    Type = "Manga",
                    Genres = new List<string>{"Erotik", "Action"}
                }
            }; 
        }

        // GET api/book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            using(var context = new SakurAniLibContext())
            {
                
            }

            return bookItems;
        }

        [Route("{isbn}")]
        public Book GetBook(int isbn) 
        {
            var book = (from b in bookItems
                    where b.Isbn == isbn
                    select b).FirstOrDefault();

            return book;
        }
    }
}