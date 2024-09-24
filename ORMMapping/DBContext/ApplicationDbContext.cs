using Microsoft.EntityFrameworkCore;
using ORMMapping.Entity;

namespace ORMMapping.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity
                =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name)
                .IsRequired();

                entity.HasOne(a => a.Publisher)
                .WithOne(p => p.Author)
                .HasForeignKey<Publisher>(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
              
            });

            modelBuilder.Entity<Book>(entity
               =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Title).IsRequired();

                entity.HasOne(a => a.Author)
                .WithMany(p=>p.books)
                .HasForeignKey(a=>a.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Publisher>(entity
                =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired();
            });

            modelBuilder.Entity<BookPublisher>(entity
                =>
            {
                entity.HasKey(bp => new { bp.PublisherId, bp.BookId });
                entity.HasOne(bp => bp.Book)
                .WithMany(b => b.BookPublisher)
                .HasForeignKey(bp => bp.BookId)
                .OnDelete(DeleteBehavior.NoAction);


                entity.HasOne(bp=>bp.publisher)
                .WithMany(b=>b.BookPublishers)
                .HasForeignKey(bp=>bp.PublisherId)
                .OnDelete(DeleteBehavior.NoAction);
            });


        }
    }
}
