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
    public class SeriesController : Controller {

        private string ConnectionString { get; set; }

        // Konstruktor
        public SeriesController()
        {
            this.ConnectionString = Utils.GetConnectionString();
        }
        
        // GET api/series
        [HttpGet]
        public async Task<IEnumerable<Series>> Get()
        {
            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                return await db.Series.ToListAsync();
            }
        }

        // [POST] api/series
        [HttpPost]
        public async Task<IActionResult> Create(Series series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                await db.Series.AddAsync(series);
                await db.SaveChangesAsync();

                return Created($"/api/series/{series.Id}", series);
            }
        }

        // [PUT] api/Series
        [HttpPut]
        public async Task<IActionResult> Update(Series series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new SakurAniLibContext(this.ConnectionString))
            {
                var updatedSeries = await db.Series.FirstOrDefaultAsync(s => s.Id == series.Id);

                if (updatedSeries == null) 
                {
                    return NotFound();
                }

                // update values
                updatedSeries.Id = series.Id;
                updatedSeries.Name = series.Name;
                updatedSeries.Amount = series.Amount;

                // Save
                await db.SaveChangesAsync();

                return Ok(series);
            }
        }
        // [DELETE] api/series/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using (var db = new SakurAniLibContext(this.ConnectionString))
            {
                var series = await db.Series.FirstOrDefaultAsync(s => s.Id == id);

                if (series == null) 
                {
                    return NotFound();
                }

                db.Series.Remove(series);
                await db.SaveChangesAsync();

                return new NoContentResult();
            }
        }
    }
}