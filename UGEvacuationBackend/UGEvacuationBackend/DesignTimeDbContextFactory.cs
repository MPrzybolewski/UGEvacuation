using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using UGEvacuationDAL;

namespace UGEvacuationBackend
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UGEvacuationContext>
    {
        public UGEvacuationContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
 
            var builder = new DbContextOptionsBuilder<UGEvacuationContext>();
 
            var connectionString = configuration.GetConnectionString("UGEvacuationContext");
 
            builder.UseSqlServer(connectionString);
 
            return new UGEvacuationContext(builder.Options);
        }
    }
}