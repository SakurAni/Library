using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SakurAni_Lib.Models
{
    public class SakurAniLibContext : DbContext 
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
          => optionBuilder.UseMySql(@"server=localhost;userid=damienbod;password=1234;database=damienbod;");
    }
}