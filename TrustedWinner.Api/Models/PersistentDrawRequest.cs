namespace TrustedWinner.Api.Models;

public class PersistentDrawRequest : DrawRequest
{
    public string ContestId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
} 