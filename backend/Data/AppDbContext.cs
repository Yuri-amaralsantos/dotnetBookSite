using bookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace bookApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ReadBook> ReadBooks { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReadBook>()
                .HasOne(rb => rb.User)
                .WithMany()
                .HasForeignKey(rb => rb.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
