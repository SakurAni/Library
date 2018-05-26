namespace SakurAni_Lib.Controllers {
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using SakurAni_Lib.Models;
    
    [Route("api/[controller]")]
    public class SeriesController : Controller {

        private readonly IEnumerable<Series> seriesItems;

        // Konstruktor
        public SeriesController() {
            seriesItems = new List<Series> {
                new Series {
                    Id = "1",
                    Name = "Erste Buecher",
                    Amount = 10
                },                
                new Series {
                    Id = "2",
                    Name = "Fortsetzungen",
                    Amount = 3
                }
            }; 
        }

        // GET api/genre
        [HttpGet]
        public IEnumerable<Series> Get()
        {
            return seriesItems;
        }

        [Route("{id}")]
        public Series GetSeries(string id) 
        {
            var series = (from s in seriesItems
                    where s.Id.Equals(id)
                    select s).FirstOrDefault();

            return series;
        }
    }
}