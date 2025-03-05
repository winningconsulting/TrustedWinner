using System;
using TrustedWinner.Core;

namespace TrustedWinner.Api.Models;

public class Draw
{
    public Guid Id { get; set; }
    public string ContestId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime SeedTimestamp { get; set; }
    public string SeedRandomPart { get; set; } = string.Empty;
    public string SeedAdditionalEntropy { get; set; } = string.Empty;
    public string[][] Results { get; set; } = Array.Empty<string[]>();

    public static Draw FromDrawResult(string contestId, string title, DrawResult result)
    {
        return new Draw
        {
            Id = Guid.NewGuid(),
            ContestId = contestId,
            Title = title,
            SeedTimestamp = result.Seed.Timestamp,
            SeedRandomPart = result.Seed.RandomPart,
            SeedAdditionalEntropy = result.Seed.AdditionalEntropy,
            Results = result.Results
        };
    }
}
