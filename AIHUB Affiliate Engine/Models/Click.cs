using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AIHUB_Affiliate_Engine.Models
{
    public class Click
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public Guid partner_id { get; set; }
        [ForeignKey(nameof(partner_id))]
        [JsonIgnore]
        public Partner? partner { get; set; }

        [Required, MaxLength(50)]
        public string affiliate_code { get; set; } = null!;

        [Required, MaxLength(50)]
        public string ip_address { get; set; } = null!;

        [MaxLength(200)]
        public string user_agent { get; set; } = null!;

        [MaxLength(200)]
        public string referer_url { get; set; } = null!;

        [MaxLength(200)]
        public string landing_page { get; set; } = null!;

        [MaxLength(100)]
        public string? session_id { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;
    }
}
