using AIHUB_Affiliate_Engine.Data;
using AIHUB_Affiliate_Engine.DTOs;
using AIHUB_Affiliate_Engine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ClickController : ControllerBase
{
    private readonly AffiliateDbContext _db;
    public ClickController(AffiliateDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClickDTO>>> GetAll()
    {
        var clicks = await _db.Clicks
            .Select(c => new ClickDTO
            {
                Id = c.id,
                AffiliateCode = c.affiliate_code,
                IpAddress = c.ip_address,
                LandingPage = c.landing_page,
                CreatedAt = c.created_at
            }).ToListAsync();

        return Ok(clicks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClickDTO>> Get(Guid id)
    {
        var c = await _db.Clicks
            .Where(x => x.id == id)
            .Select(c => new ClickDTO
            {
                Id = c.id,
                AffiliateCode = c.affiliate_code,
                IpAddress = c.ip_address,
                LandingPage = c.landing_page,
                CreatedAt = c.created_at
            }).FirstOrDefaultAsync();

        if (c == null) return NotFound();
        return Ok(c);
    }

    [HttpPost]
    public async Task<ActionResult<ClickDTO>> Create([FromBody] Click click)
    {
        click.id = Guid.NewGuid();
        click.created_at = DateTime.UtcNow;

        _db.Clicks.Add(click);
        await _db.SaveChangesAsync();

        await _db.AddLogAsync(
            action: "Create",
            entity: "Click",
            description: $"Created click {click.id}"
        );

        var dto = new ClickDTO
        {
            Id = click.id,
            AffiliateCode = click.affiliate_code,
            IpAddress = click.ip_address,
            LandingPage = click.landing_page,
            CreatedAt = click.created_at
        };
        return CreatedAtAction(nameof(Get), new { id = click.id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Click updated)
    {
        var click = await _db.Clicks.FindAsync(id);
        if (click == null) return NotFound();

        click.partner_id = updated.partner_id;
        click.affiliate_code = updated.affiliate_code;
        click.ip_address = updated.ip_address;
        click.user_agent = updated.user_agent;
        click.referer_url = updated.referer_url;
        click.landing_page = updated.landing_page;
        click.session_id = updated.session_id;

        await _db.SaveChangesAsync();
        await _db.AddLogAsync(
            action: "Update",
            entity: "Click",
            description: $"Updated click {id}"
        );

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var click = await _db.Clicks.FindAsync(id);
        if (click == null) return NotFound();

        _db.Clicks.Remove(click);
        await _db.SaveChangesAsync();

        await _db.AddLogAsync(
            action: "Delete",
            entity: "Click",
            description: $"Deleted click {id}"
        );

        return NoContent();
    }
}
