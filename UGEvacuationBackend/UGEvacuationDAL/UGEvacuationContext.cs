using Microsoft.EntityFrameworkCore;
using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL
{
    public class UGEvacuationContext : DbContext
    {
        public UGEvacuationContext(DbContextOptions<UGEvacuationContext> options) : base(options)
        {
        }
        
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>().ToTable("AdminUser");
            modelBuilder.Entity<AppUser>().ToTable("AppUser");
        }
    }
}