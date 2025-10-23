namespace AIHUB_Affiliate_Engine.DTOs
{
    public class PayoutDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
