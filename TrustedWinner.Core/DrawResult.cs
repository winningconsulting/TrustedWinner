using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace TrustedWinner.Core;

/// <summary>
/// Represents the result of a draw, including all inputs and outputs.
/// </summary>
public class DrawResult
{
    /// <summary>
    /// JSON serialization options for DrawResult serialization and deserialization.
    /// </summary>
    public static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// The version of TrustedWinner.Core that was used to generate this draw result.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("version")]
    public required string Version { get; init; }

    /// <summary>
    /// The configuration used for the draw.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("configuration")]
    public required Configuration Configuration { get; init; }

    /// <summary>
    /// The entries that were used in the draw.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("entries")]
    public required string[] Entries { get; init; }

    /// <summary>
    /// The seed used for random selection.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("seed")]
    public required Seed Seed { get; init; }

    /// <summary>
    /// The results of the draw. Each element is an array containing a winner and their substitutes.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("results")]
    public required string[][] Results { get; init; }

    /// <summary>
    /// The public certificate used to sign the draw result, if any.
    /// </summary>
    [JsonPropertyName("certificate")]
    public string? Certificate { get; init; }

    /// <summary>
    /// The signature of the draw result, if signed with a certificate.
    /// </summary>
    [JsonPropertyName("signature")]
    public string? Signature { get; init; }
} 