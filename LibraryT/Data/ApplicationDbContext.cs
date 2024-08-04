using LibraryT.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryT.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : DbContext(options)
    {
        private readonly IConfiguration _configuration = configuration;
        
        public DbSet<LibraryModel> Libraries { get; set; }
        public DbSet<ShelfModel> Shelves { get; set; }
        public DbSet<BookSetModel> BookSets { get; set; }
        public DbSet<BookModel> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryModel>()
                .HasMany(lib => lib.Shelves)
                .WithOne(shel => shel.Library)
                .HasForeignKey(shel => shel.LibraryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShelfModel>()
                .HasMany(shel => shel.BookSets)
                .WithOne(bookset => bookset.Shelf)
                .HasForeignKey(bookset => bookset.ShelfId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookSetModel>()
                .HasMany(bookset => bookset.Books)
                .WithOne(book => book.BookSet)
                .HasForeignKey(book => book.BookSetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
