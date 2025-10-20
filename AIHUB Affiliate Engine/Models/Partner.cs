using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIHUB_Affiliate_Engine.Models
{
    public class Partner
    {
        [Key]
        public Guid id { get; set; } // ID duy nhất của partner

        [Required, MaxLength(100)]
        public string name { get; set; } = null!;

        [Required, MaxLength(100)]
        public string email { get; set; } = null!;

        [MaxLength(20)]
        public string phone { get; set; } = null!;

        [Required, MaxLength(50)]
        public string affiliate_code { get; set; } = null!;

        [MaxLength(255)]
        public string tracking_url { get; set; } = null!;

        [MaxLength(20)]
        public string status { get; set; } = "active"; // active, inactive, banned

        public int total_clicks { get; set; } = 0;
        public int total_conversions { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal total_commission { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal pending_commission { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal paid_commission { get; set; } = 0;

        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }

        // Navigation
        public ICollection<Click>? clicks { get; set; }
        public ICollection<Commission>? commissions { get; set; }
        public ICollection<Payout>? payouts { get; set; }
    }
}
