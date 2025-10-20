using AIHUB_Affiliate_Engine.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AIHUB_Affiliate_Engine.Data
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Ghi log chuẩn vào bảng Logs
        /// </summary>
        public static async Task AddLogAsync(this AffiliateDbContext db, string action, string entity, string description)
        {
            var log = new Log
            {
                id = Guid.NewGuid(),
                action = action,
                entity = entity,
                description = description,
                created_at = DateTime.UtcNow
            };

            db.Logs.Add(log);
            await db.SaveChangesAsync();
        }
    }
}
