using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SakurAni_Lib.Models.Database;

namespace SakurAni_Lib.Models
{
    public class SakurAniLibContext : DbContext 
    {
        public SakurAniLibContext(string _conString) {
            this.ConnectionString = _conString;
        }

        private string ConnectionString { get; set; }

        public DbSet<DbBook> Book { get; set; }
        public DbSet<DbAuthor> Author { get; set; }
        public DbSet<DbBookAuthor> Book_Author { get; set; }
        // public DbSet<Genre> Genre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
          => optionBuilder.UseMySql(ConnectionString);
    }
}