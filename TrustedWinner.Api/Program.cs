using Microsoft.EntityFrameworkCore;
using TrustedWinner.Api.Configuration;
using TrustedWinner.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CertificateSettings>(
    builder.Configuration.GetSection("CertificateSettings"));

// Add database context
builder.Services.AddDbContext<DrawDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowConfiguredOrigins",
        builder =>
        {
            builder.WithOrigins(allowedOrigins)
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Ensure database is created and migrations are applied
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DrawDbContext>();
    dbContext.Database.Migrate();
}

// Configure HTTPS
app.UseHttpsRedirection();
app.UseHsts(); // Add HTTP Strict Transport Security

// Use CORS
app.UseCors("AllowConfiguredOrigins");

// Use static files
app.UseStaticFiles();

// Use default files (index.html)
app.UseDefaultFiles();

app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Serve index.html for all non-API routes
app.MapFallbackToFile("index.html");

app.Run();
