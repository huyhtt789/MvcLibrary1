using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcLibrary.Models;
using System.Linq;

namespace MvcLibrary.Data
{
    public class MvcLibraryContext : IdentityDbContext<ApplicationUser>
    {
        public MvcLibraryContext(DbContextOptions<MvcLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BorrowDetail> BorrowDetails   { get; set; }
        public DbSet<Borrower> Borrowers   { get; set; }
        public DbSet<Borrowing> Borrowings  { get; set; }
        public DbSet<Genre> Genres  { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<PublishingCompany> PublishingCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            foreach (var foreignKey in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}