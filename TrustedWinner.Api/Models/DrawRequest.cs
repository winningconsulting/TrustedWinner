namespace TrustedWinner.Api.Models;

public class DrawRequest
{
    public List<string> Entries { get; set; } = new List<string>();
    public TrustedWinner.Core.Configuration Configuration { get; set; } = new(1, 0);
    public string AdditionalEntropy { get; set; } = String.Empty;
} 