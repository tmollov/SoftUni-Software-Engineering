﻿using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data
{
    public class BookLibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Borrower> Borrowers { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookBorrowers> BookBorrowerses { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<MovieBorrowers> MovieBorrowerses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SqlServerConstants.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookBorrowers>()
                .HasOne(e => e.Book)
                .WithMany(b => b.Borrowerses)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<BookBorrowers>()
                .HasOne(e => e.Borrower)
                .WithMany(b => b.BorrowersedBooks)
                .HasForeignKey(b => b.BorrowerId);

            modelBuilder.Entity<BookBorrowers>()
                .HasKey(e => new {e.BookId, e.BorrowerId, e.BorrowDate});

            modelBuilder.Entity<MovieBorrowers>()
                .HasOne(e => e.Movie)
                .WithMany(b => b.Borrowerses)
                .HasForeignKey(b => b.MovieId);

            modelBuilder.Entity<MovieBorrowers>()
                .HasOne(e => e.Borrower)
                .WithMany(b => b.BorrowersedMovies)
                .HasForeignKey(b => b.BorrowerId);

            modelBuilder.Entity<MovieBorrowers>()
                .HasKey(e => new { e.MovieId, e.BorrowerId, e.BorrowDate });
            base.OnModelCreating(modelBuilder);
        }
    }
}
