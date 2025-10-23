using AIHUB_Affiliate_Engine.Data;
using AIHUB_Affiliate_Engine.DTOs;
using AIHUB_Affiliate_Engine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CommissionsController : ControllerBase
{
    private readonly AffiliateDbContext _db;
    public CommissionsController(AffiliateDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommissionDTO>>> GetAll()
    {
        var commissions = await _db.Commissions
            .Select(c => new CommissionDTO
            {
                Id = c.id,
                OrderId = c.order_id,
                Amount = c.amount,
                CommissionAmount = c.commission_amount,
                Status = c.status,
                CreatedAt = c.created_at
            }).ToListAsync();
        return Ok(commissions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommissionDTO>> Get(Guid id)
    {
        var commission = await _db.Commissions
            .Where(c => c.id == id)
            .Select(c => new CommissionDTO
            {
                Id = c.id,
                OrderId = c.order_id,
                Amount = c.amount,
                CommissionAmount = c.commission_amount,
                Status = c.status,
                CreatedAt = c.created_at
            }).FirstOrDefaultAsync();

        if (commission == null) return NotFound();
        return Ok(commission);
    }

    [HttpPost]
    public async Task<ActionResult<CommissionDTO>> Create([FromBody] Commission commission)
    {
        commission.id = Guid.NewGuid();
        commission.created_at = DateTime.UtcNow;

        _db.Commissions.Add(commission);
        await _db.SaveChangesAsync();

        await _db.AddLogAsync(
            action: "Create",
            entity: "Commission",
            description: $"Created commission {commission.id}"
        );

        var dto = new CommissionDTO
        {
            Id = commission.id,
            OrderId = commission.order_id,
            Amount = commission.amount,
            CommissionAmount = commission.commission_amount,
            Status = commission.status,
            CreatedAt = commission.created_at
        };

        return CreatedAtAction(nameof(Get), new { id = commission.id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Commission updated)
    {
        var commission = await _db.Commissions.FindAsync(id);
        if (commission == null) return NotFound();

        commission.partner_id = updated.partner_id;
        commission.click_id = updated.click_id;
        commission.order_id = updated.order_id;
        commission.conversion_type = updated.conversion_type;
        commission.amount = updated.amount;
        commission.commission_amount = updated.commission_amount;
        commission.status = updated.status;
        commission.approved_at = updated.approved_at;
        commission.paid_at = updated.paid_at;

        await _db.SaveChangesAsync();
        await _db.AddLogAsync(
            action: "Update",
            entity: "Commission",
            description: $"Updated commission {id}"
        );

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var commission = await _db.Commissions.FindAsync(id);
        if (commission == null) return NotFound();

        _db.Commissions.Remove(commission);
        await _db.SaveChangesAsync();
        await _db.AddLogAsync(
            action: "Delete",
            entity: "Commission",
            description: $"Deleted commission {id}"
        );

        return NoContent();
    }
}
