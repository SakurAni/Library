namespace SakurAni_Lib.Controllers {
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using SakurAni_Lib.Models;
    
    [Route("api/[controller]")]
    public class GenreController : Controller {

        private readonly IEnumerable<Genre> genreItems;

        // Konstruktor
        public GenreController() {
            genreItems = new List<Genre> {
                new Genre {
                    Id = "1",
                    Name = "Shounen Ai"
                },                
                new Genre {
                    Id = "2",
                    Name = "Shoujo Ai"
                }
            }; 
        }

        // GET api/genre
        [HttpGet]
        public IEnumerable<Genre> Get()
        {
            return genreItems;
        }

        [Route("{id}")]
        public Genre GetGenre(string id) 
        {
            var genre = (from g in genreItems
                    where g.Id.Equals(id)
                    select g).FirstOrDefault();

            return genre;
        }
    }
}