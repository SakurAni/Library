using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SakurAni_Lib.Models 
{
    public class SakurAniLibContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
            => optionBuilder.UseMySql(@"server=localhost;userid=damienbod;password=1234;database=damienbod;");
    }
}