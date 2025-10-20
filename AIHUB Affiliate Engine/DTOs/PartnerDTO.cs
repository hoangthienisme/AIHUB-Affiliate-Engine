namespace AIHUB_Affiliate_Engine.DTOs
{
    public class PartnerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string AffiliateCode { get; set; } = "";
        public string Status { get; set; } = "";
        public List<ClickDTO> Clicks { get; set; } = new();
        public List<CommissionDTO> Commissions { get; set; } = new();
        public List<PayoutDTO> Payouts { get; set; } = new();
    }
}
