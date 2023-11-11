using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sagau_Rares_Lab2.Models;

namespace Sagau_Rares_Lab2.Data
{
    public class Sagau_Rares_Lab2Context : DbContext
    {
        public Sagau_Rares_Lab2Context (DbContextOptions<Sagau_Rares_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Sagau_Rares_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Sagau_Rares_Lab2.Models.Publisher>? Publisher { get; set; }

        public DbSet<Sagau_Rares_Lab2.Models.Author>? Author { get; set; }

        public DbSet<Sagau_Rares_Lab2.Models.Category>? Category { get; set; }

        public DbSet<Sagau_Rares_Lab2.Models.Member>? Member { get; set; }

        public DbSet<Sagau_Rares_Lab2.Models.Borrowing>? Borrowing { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-one relationship between Book and Borrowing
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Borrowing) // Book has one Borrowing
                .WithOne(br => br.Book) // Borrowing has one Book
                .HasForeignKey<Borrowing>(br => br.BookID); // The FK in Borrowing is BookID
        }
    }
}
