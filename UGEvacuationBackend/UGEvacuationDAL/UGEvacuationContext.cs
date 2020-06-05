using Microsoft.EntityFrameworkCore;
using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL
{
    public class UGEvacuationContext : DbContext
    {
        public UGEvacuationContext(DbContextOptions<UGEvacuationContext> options) : base(options)
        {
        }
        
        public DbSet<AppUser> AppUsers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().ToTable("AppUser");
        }
    }
}