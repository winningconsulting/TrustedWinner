using System.Text.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;

namespace TrustedWinner.Core.Tests;

public class DrawExecutorTests
{
    [Fact]
    public void Execute_GeneratesValidDrawResult()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3" };

        // Act
        var executor = new DrawExecutor(config, entries);
        executor.ExecuteDraw();
        var json = executor.GetAuditableJson();
        var result = JsonSerializer.Deserialize<DrawResult>(json, DrawResult.SerializerOptions);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(config, result.Configuration);
        Assert.Equal(entries, result.Entries);
        Assert.NotEqual(default, result.Seed.Timestamp);
        Assert.NotNull(result.Seed.RandomPart);
        var results = Assert.Single(result.Results);
        Assert.Equal(3, results.Length); // 1 winner + 2 substitutes
    }

    [Fact]
    public void ExecuteWithSeed_ProducesSameResultsForSameSeed()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3", "entry4", "entry5", "entry6", "entry7", "entry8", "entry9", "entry10" };

        var seed = SeedGenerator.Generate();

        // Act
        var executor1 = new DrawExecutor(config, entries);
        executor1.SimulateDraw(seed);
        var executor2 = new DrawExecutor(config, entries);
        executor2.SimulateDraw(seed);
        var result1 = executor1.GetResults();
        var result2 = executor2.GetResults();

        // Assert
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void ExecuteWithSeed_WithDifferentSeeds_ProducesDifferentResults()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3", "entry4", "entry5", "entry6", "entry7", "entry8" };

        var seed1 = SeedGenerator.Generate();
        var seed2 = SeedGenerator.Generate();

        // Act
        var executor1 = new DrawExecutor(config, entries);
        executor1.SimulateDraw(seed1);
        var executor2 = new DrawExecutor(config, entries);
        executor2.SimulateDraw(seed2);
        var result1 = executor1.GetResults();
        var result2 = executor2.GetResults();

        // Assert
        Assert.NotEqual(result1, result2);
    }

    [Fact]
    public void Execute_WithMultipleWinners_GeneratesCorrectNumberOfResults()
    {
        // Arrange
        var config = new Configuration(
            Winners: 2,
            SubstitutesPerWinner: 1);

        var entries = new[] { "entry1", "entry2", "entry3", "entry4", "entry5", "entry6", "entry7", "entry8", "entry9", "entry10" };

        // Act
        var executor = new DrawExecutor(config, entries);
        executor.ExecuteDraw();
        var result = executor.GetResults();

        // Assert
        Assert.Equal(2, result.Length);
        Assert.All(result, winnerGroup => Assert.Equal(2, winnerGroup.Length));
    }

    [Fact]
    public void ExecuteWithSeed_WithInvalidEntries_ThrowsInvalidOperationException()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 1);

        var entries = new[] { "entry1", "entry1" }; // Duplicate entry

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => new DrawExecutor(config, entries));
    }

    [Fact]
    public void GetAuditableJson_GeneratesValidJson()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3" };

        var executor = new DrawExecutor(config, entries);
        executor.ExecuteDraw();
        // Act
        var json = executor.GetAuditableJson();

        // Assert
        Assert.True(IsValidJson(json));
        Assert.Contains("version", json.ToLower());
        Assert.Contains("configuration", json.ToLower());
        Assert.Contains("entries", json.ToLower());
        Assert.Contains("seed", json.ToLower());
        Assert.Contains("results", json.ToLower());
    }

    [Fact]
    public void GetAuditableJson_WithCertificate_GeneratesSignedJson()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3" };

        // Create a test certificate
        var certificate = TestCertificateGenerator.GenerateCertificate();
        var executor = new DrawExecutor(config, entries, certificate);
        executor.ExecuteDraw();

        // Act
        var json = executor.GetAuditableJson();
        var result = JsonSerializer.Deserialize<DrawResult>(json, DrawResult.SerializerOptions);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Certificate);
        Assert.NotNull(result.Signature);
        Assert.True(DrawVerifier.IsAuthentic(json));
    }

    [Fact]
    public void GetAuditableJson_WithCertificate_ThrowsIfCertificateHasNoPrivateKey()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3" };

        // Create a test certificate without private key
        var certificate = TestCertificateGenerator.GenerateCertificateWithoutPrivateKey();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => new DrawExecutor(config, entries, certificate));
    }

    private static bool IsValidJson(string json)
    {
        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
} 