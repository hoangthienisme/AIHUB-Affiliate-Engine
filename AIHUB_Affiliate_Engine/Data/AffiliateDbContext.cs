using AIHUB_Affiliate_Engine.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
namespace AIHUB_Affiliate_Engine.Data
{
    public class AffiliateDbContext : DbContext
    {
        public AffiliateDbContext(DbContextOptions<AffiliateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Partner> Partners { get; set; }
        public DbSet<Click> Clicks { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<Payout> Payouts { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
