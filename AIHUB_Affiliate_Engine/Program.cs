using AIHUB_Affiliate_Engine.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AffiliateDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AffiliateDb"))
);

var app = builder.Build();

// --------------------
// ✅ Always enable Swagger (for Render)
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Disable HTTPS redirect (Render handles HTTPS at proxy)
app.UseAuthorization();
app.MapControllers();

// ✅ Allow Render port binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

// ✅ Optional: redirect root to Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
