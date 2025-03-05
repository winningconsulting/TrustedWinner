using System.Text.Json;
using TrustedWinner.Core;

namespace TrustedWinner.Api.Models;

public class AuditableResult
{
    public Guid DrawId { get; set; }
    public string Json { get; set; } = string.Empty;

    public static AuditableResult FromDrawResult(Guid drawId, DrawResult result)
    {
        return new AuditableResult
        {
            DrawId = drawId,
            Json = JsonSerializer.Serialize(result, DrawResult.SerializerOptions)
        };
    }
} 