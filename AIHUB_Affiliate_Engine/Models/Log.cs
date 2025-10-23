using System.ComponentModel.DataAnnotations;

namespace AIHUB_Affiliate_Engine.Models
{
    public class Log
    {
        [Key]
        public Guid id { get; set; }
        public string action { get; set; } = "";
        public string entity { get; set; } = "";
        public string description { get; set; } = "";
        public DateTime created_at { get; set; } = DateTime.UtcNow;
    }
}
