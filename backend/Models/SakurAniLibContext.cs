using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SakurAni_Lib.Models
{
    public class SakurAniLibContext : DbContext 
    {
        public SakurAniLibContext(string _conString) {
            this.ConnectionString = _conString;
        }

        private string ConnectionString { get; set; }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Book_Genre> Book_Genre { get; set; }
        public DbSet<Book_Author> Book_Author { get; set; }
        public DbSet<Series_Book> Series_Book { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
          => optionBuilder.UseMySql(ConnectionString);
    }
}