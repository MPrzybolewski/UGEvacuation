using Microsoft.EntityFrameworkCore;
using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL
{
    public class UGEvacuationContext : DbContext
    {
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Evacuation> Evacuations { get; set; }
        public DbSet<EvacuationNode> EvacuationsNodes { get; set; }
        public DbSet<EvacuationNodeAppUser> EvacuationsNodesAppUsers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>().ToTable("AdminUser");
            modelBuilder.Entity<AppUser>().ToTable("AppUser");
            modelBuilder.Entity<Evacuation>().ToTable("Evacuation");
            modelBuilder.Entity<EvacuationNode>().ToTable("EvacuationNode");
            modelBuilder.Entity<EvacuationNodeAppUser>().ToTable("EvacuationNodeAppUser");
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=46.41.140.29;User Id=SA;Password=UGEvacuation123;Database=UGEvacuation");
        }
    }
}