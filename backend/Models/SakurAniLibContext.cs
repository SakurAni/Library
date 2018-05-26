// using System;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;

// namespace SakurAni_Lib.Models
// {
//     public class SakurAniLibContext : DbContext 
//     {
//         public SakurAniLibContext(DbContextOptions<SakurAniLibContext> options) :base(options)
//         { }

//         public DbSet<Book> Books {get; set;}

//         protected override void OnModelCreating(ModelBuilder builder)
//         {
//             builder.Entity<Isbn>().HasKey(m => m.)
//         }
//     }
// }