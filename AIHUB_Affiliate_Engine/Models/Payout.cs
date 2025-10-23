using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AIHUB_Affiliate_Engine.Models
{
    public class Payout
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public Guid partner_id { get; set; }
        [ForeignKey(nameof(partner_id))]
        [JsonIgnore]
        public Partner? partner { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal amount { get; set; }

        [MaxLength(30)]
        public string method { get; set; } = "bank_transfer"; // bank_transfer, paypal, crypto

        [Required]
        [Column(TypeName = "text")]
        public string account_info { get; set; } = "{}";

        [MaxLength(20)]
        public string status { get; set; } = "pending"; // pending, completed, failed

        [MaxLength(200)]
        public string? note { get; set; }

        [MaxLength(100)]
        public string? approved_by { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
    }
}
