namespace AIHUB_Affiliate_Engine.DTOs
{
    public class CommissionDTO
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; } = "";
        public decimal Amount { get; set; }
        public decimal CommissionAmount { get; set; }
        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
