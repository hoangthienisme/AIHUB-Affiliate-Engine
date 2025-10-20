using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AIHUB_Affiliate_Engine.Data
{
    public class AffiliateDbContextFactory : IDesignTimeDbContextFactory<AffiliateDbContext>
    {
        public AffiliateDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // thư mục chứa appsettings.json
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AffiliateDbContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("AffiliateDb"));

            return new AffiliateDbContext(optionsBuilder.Options);
        }
    }
}
