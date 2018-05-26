namespace SakurAni_Lib.Controllers {
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using SakurAni_Lib.Models;
    
    [Route("api/[controller]")]
    public class AuthorController : Controller {

        private readonly IEnumerable<Author> authorItems;

        // Konstruktor
        public AuthorController() {
            authorItems = new List<Author> {
                new Author {
                    Id = "1",
                    Name = "Sebastian Leissner",
                    IsArtist = false
                },                
                new Author {
                    Id = "2",
                    Name = "Sebastian Gierth",
                    IsArtist = true
                }
            }; 
        }

        // GET api/genre
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            return authorItems;
        }

        [Route("{id}")]
        public Author GetAuthor(string id) 
        {
            var author = (from a in authorItems
                    where a.Id.Equals(id)
                    select a).FirstOrDefault();

            return author;
        }
    }
}