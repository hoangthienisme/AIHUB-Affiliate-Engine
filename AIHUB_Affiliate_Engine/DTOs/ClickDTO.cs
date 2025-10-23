namespace AIHUB_Affiliate_Engine.DTOs
{
    public class ClickDTO
    {
        public Guid Id { get; set; }
        public string AffiliateCode { get; set; } = "";
        public string IpAddress { get; set; } = "";
        public string LandingPage { get; set; } = ""; 
        public DateTime CreatedAt { get; set; }
    }
}
