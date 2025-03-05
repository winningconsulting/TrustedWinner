using Microsoft.EntityFrameworkCore;
using TrustedWinner.Api.Models;
using System.Text.Json;

namespace TrustedWinner.Api.Data;

public class DrawDbContext : DbContext
{
    private static readonly JsonSerializerOptions _jsonOptions = new();

    public DrawDbContext(DbContextOptions<DrawDbContext> options) : base(options)
    {
    }

    public DbSet<Draw> Draws { get; set; }
    public DbSet<AuditableResult> AuditableResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Draw>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.ContestId, e.Title }).IsUnique();
            
            // Configure array property to be stored as JSON
            entity.Property(e => e.Results)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, _jsonOptions),
                    v => JsonSerializer.Deserialize<string[][]>(v, _jsonOptions) ?? Array.Empty<string[]>());

            // Configure one-to-one relationship with AuditableResult
            entity.HasOne<AuditableResult>()
                .WithOne()
                .HasForeignKey<AuditableResult>(ar => ar.DrawId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AuditableResult>(entity =>
        {
            entity.HasKey(e => e.DrawId);
        });
    }
} 