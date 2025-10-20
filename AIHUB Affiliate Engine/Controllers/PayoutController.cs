using AIHUB_Affiliate_Engine.Data;
using AIHUB_Affiliate_Engine.DTOs;
using AIHUB_Affiliate_Engine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PayoutsController : ControllerBase
{
    private readonly AffiliateDbContext _db;
    public PayoutsController(AffiliateDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PayoutDTO>>> GetAll()
    {
        var payouts = await _db.Payouts
            .Select(p => new PayoutDTO
            {
                Id = p.id,
                Amount = p.amount,
                Method = p.method,
                Status = p.status,
                CreatedAt = p.created_at
            }).ToListAsync();

        return Ok(payouts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PayoutDTO>> Get(Guid id)
    {
        var payout = await _db.Payouts
            .Where(p => p.id == id)
            .Select(p => new PayoutDTO
            {
                Id = p.id,
                Amount = p.amount,
                Method = p.method,
                Status = p.status,
                CreatedAt = p.created_at
            }).FirstOrDefaultAsync();

        if (payout == null) return NotFound();
        return Ok(payout);
    }

    [HttpPost]
    public async Task<ActionResult<PayoutDTO>> Create([FromBody] Payout payout)
    {
        payout.id = Guid.NewGuid();
        payout.created_at = DateTime.UtcNow;

        _db.Payouts.Add(payout);
        await _db.SaveChangesAsync();
        await _db.AddLogAsync(
            action: "Create",
            entity: "Payout",
            description: $"Created payout {payout.id}"
        );

        var dto = new PayoutDTO
        {
            Id = payout.id,
            Amount = payout.amount,
            Method = payout.method,
            Status = payout.status,
            CreatedAt = payout.created_at
        };
        return CreatedAtAction(nameof(Get), new { id = payout.id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Payout updated)
    {
        var payout = await _db.Payouts.FindAsync(id);
        if (payout == null) return NotFound();

        payout.partner_id = updated.partner_id;
        payout.amount = updated.amount;
        payout.method = updated.method;
        payout.account_info = updated.account_info;
        payout.status = updated.status;
        payout.note = updated.note;
        payout.approved_by = updated.approved_by;
        payout.updated_at = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        await _db.AddLogAsync(
            action: "Update",
            entity: "Payout",
            description: $"Updated payout {id}"
        );

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var payout = await _db.Payouts.FindAsync(id);
        if (payout == null) return NotFound();

        _db.Payouts.Remove(payout);
        await _db.SaveChangesAsync();
        await _db.AddLogAsync(
            action: "Delete",
            entity: "Payout",
            description: $"Deleted payout {id}"
        );

        return NoContent();
    }
}
