using Core.Entites;
using Core.Entities;
using LibraryBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repo.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<BookBorrowing> BookBorrowings { get; set; }
        public DbSet<BookReservation> BookReservations { get; set; }
    }
}