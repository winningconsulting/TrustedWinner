namespace TrustedWinner.Core;

/// <summary>
/// Represents the configuration for a draw, specifying the number of winners and substitutes per winner.
/// </summary>
/// <param name="Winners">The number of winners to select in the draw.</param>
/// <param name="SubstitutesPerWinner">The number of substitutes to select for each winner.</param>
public record Configuration(uint Winners, uint SubstitutesPerWinner);
