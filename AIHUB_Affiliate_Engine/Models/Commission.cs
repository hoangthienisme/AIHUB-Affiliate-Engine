using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AIHUB_Affiliate_Engine.Models
{
    public class Commission
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public Guid partner_id { get; set; }
        [ForeignKey(nameof(partner_id))]
        [JsonIgnore]
        public Partner? partner { get; set; }

        [Required]
        public Guid click_id { get; set; }
        [ForeignKey(nameof(click_id))]
        [JsonIgnore]
        public Click? click { get; set; }

        [Required, MaxLength(100)]
        public string order_id { get; set; } = null!;

        [MaxLength(20)]
        public string conversion_type { get; set; } = "sale"; // sale, lead, signup

        [Column(TypeName = "decimal(18,2)")]
        public decimal amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal commission_amount { get; set; }

        [MaxLength(20)]
        public string status { get; set; } = "pending"; // pending, approved, rejected, paid

        public DateTime? approved_at { get; set; }
        public DateTime? paid_at { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}
