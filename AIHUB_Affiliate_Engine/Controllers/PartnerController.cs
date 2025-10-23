using AIHUB_Affiliate_Engine.Data;
using AIHUB_Affiliate_Engine.DTOs;
using AIHUB_Affiliate_Engine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class PartnersController : ControllerBase
{
    private readonly AffiliateDbContext _db;
    public PartnersController(AffiliateDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartnerDTO>>> GetAll()
    {
        var partners = await _db.Partners
            .Include(p => p.clicks)
            .Include(p => p.commissions)
            .Include(p => p.payouts)
            .Select(p => new PartnerDTO
            {
                Id = p.id,
                Name = p.name,
                Email = p.email,
                AffiliateCode = p.affiliate_code,
                Status = p.status,
                Clicks = p.clicks.Select(c => new ClickDTO
                {
                    Id = c.id,
                    AffiliateCode = c.affiliate_code,
                    IpAddress = c.ip_address,
                    LandingPage = c.landing_page,
                    CreatedAt = c.created_at
                }).ToList(),
                Commissions = p.commissions.Select(c => new CommissionDTO
                {
                    Id = c.id,
                    OrderId = c.order_id,
                    Amount = c.amount,
                    CommissionAmount = c.commission_amount,
                    Status = c.status,
                    CreatedAt = c.created_at
                }).ToList(),
                Payouts = p.payouts.Select(po => new PayoutDTO
                {
                    Id = po.id,
                    Amount = po.amount,
                    Method = po.method,
                    Status = po.status,
                    CreatedAt = po.created_at
                }).ToList()
            }).ToListAsync();

        return Ok(partners);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartnerDTO>> Get(Guid id)
    {
        var partner = await _db.Partners
            .Include(p => p.clicks)
            .Include(p => p.commissions)
            .Include(p => p.payouts)
            .Where(p => p.id == id)
            .Select(p => new PartnerDTO
            {
                Id = p.id,
                Name = p.name,
                Email = p.email,
                AffiliateCode = p.affiliate_code,
                Status = p.status,
                Clicks = p.clicks.Select(c => new ClickDTO
                {
                    Id = c.id,
                    AffiliateCode = c.affiliate_code,
                    IpAddress = c.ip_address,
                    LandingPage = c.landing_page,
                    CreatedAt = c.created_at
                }).ToList(),
                Commissions = p.commissions.Select(c => new CommissionDTO
                {
                    Id = c.id,
                    OrderId = c.order_id,
                    Amount = c.amount,
                    CommissionAmount = c.commission_amount,
                    Status = c.status,
                    CreatedAt = c.created_at
                }).ToList(),
                Payouts = p.payouts.Select(po => new PayoutDTO
                {
                    Id = po.id,
                    Amount = po.amount,
                    Method = po.method,
                    Status = po.status,
                    CreatedAt = po.created_at
                }).ToList()
            }).FirstOrDefaultAsync();

        if (partner == null) return NotFound();
        return Ok(partner);
    }

    [HttpPost]
    public async Task<ActionResult<PartnerDTO>> Create([FromBody] Partner partner)
    {
        partner.id = Guid.NewGuid();
        partner.created_at = DateTime.UtcNow;

        _db.Partners.Add(partner);
        await _db.SaveChangesAsync();

        await _db.AddLogAsync(
            action: "Create",
            entity: "Partner",
            description: $"Created partner {partner.id}"
        );

        return CreatedAtAction(nameof(Get), new { id = partner.id }, partner);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Partner updated)
    {
        var partner = await _db.Partners.FindAsync(id);
        if (partner == null) return NotFound();

        partner.name = updated.name;
        partner.email = updated.email;
        partner.phone = updated.phone;
        partner.affiliate_code = updated.affiliate_code;
        partner.tracking_url = updated.tracking_url;
        partner.status = updated.status;
        partner.updated_at = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        await _db.AddLogAsync(
            action: "Update",
            entity: "Partner",
            description: $"Updated partner {id}"
        );

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var partner = await _db.Partners.FindAsync(id);
        if (partner == null) return NotFound();

        _db.Partners.Remove(partner);
        await _db.SaveChangesAsync();
        await _db.AddLogAsync(
            action: "Delete",
            entity: "Partner",
            description: $"Deleted partner {id}"
        );

        return NoContent();
    }
}
