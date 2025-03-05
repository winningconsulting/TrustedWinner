using System.Text.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;

namespace TrustedWinner.Core.Tests;

public class DrawVerifierTests
{
    [Fact]
    public void Verify_WithValidJson_ReturnsTrue()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3" };

        var executor = new DrawExecutor(config, entries);
        executor.ExecuteDraw();
        var json = executor.GetAuditableJson();

        // Act
        var result = DrawVerifier.IsAuthentic(json);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Verify_WithInvalidJson_ThrowsJsonException()
    {
        // Arrange
        var json = "invalid json";

        // Act & Assert
        Assert.Throws<JsonException>(() => DrawVerifier.IsAuthentic(json));
    }

    [Fact]
    public void Verify_WithTamperedJson_ReturnsFalse()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3" };

        var executor = new DrawExecutor(config, entries);
        executor.ExecuteDraw();
        var json = executor.GetAuditableJson();

        // Tamper with the JSON
        var result = JsonSerializer.Deserialize<DrawResult>(json, DrawResult.SerializerOptions);
        var tamperedResult = new DrawResult
        {
            Version = result!.Version,
            Configuration = result.Configuration,
            Entries = result.Entries,
            Seed = result.Seed,
            Results = new[] { new[] { "tampered", result.Results[0][1], result.Results[0][2] } },
            Certificate = result.Certificate,
            Signature = result.Signature
        };

        var tamperedJson = JsonSerializer.Serialize(tamperedResult, DrawResult.SerializerOptions);

        // Act
        var isAuthentic = DrawVerifier.IsAuthentic(tamperedJson);

        // Assert
        Assert.False(isAuthentic);
    }

    [Fact]
    public void Verify_WithSignedJson_ReturnsTrue()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3" };

        var certificate = TestCertificateGenerator.GenerateCertificate();
        var executor = new DrawExecutor(config, entries, certificate);
        executor.ExecuteDraw();
        var json = executor.GetAuditableJson();

        // Act
        var result = DrawVerifier.IsAuthentic(json);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Verify_WithInvalidSignature_ReturnsFalse()
    {
        // Arrange
        var config = new Configuration(
            Winners: 1,
            SubstitutesPerWinner: 2);

        var entries = new[] { "entry1", "entry2", "entry3" };

        var certificate = TestCertificateGenerator.GenerateCertificate();
        var executor = new DrawExecutor(config, entries, certificate);
        executor.ExecuteDraw();
        var json = executor.GetAuditableJson();

        // Tamper with the signature
        var result = JsonSerializer.Deserialize<DrawResult>(json, DrawResult.SerializerOptions);
        var tamperedResult = new DrawResult
        {
            Version = result!.Version,
            Configuration = result.Configuration,
            Entries = result.Entries,
            Seed = result.Seed,
            Results = result.Results,
            Certificate = result.Certificate,
            Signature = "invalid signature"
        };

        var tamperedJson = JsonSerializer.Serialize(tamperedResult, DrawResult.SerializerOptions);

        // Act
        var isAuthentic = DrawVerifier.IsAuthentic(tamperedJson);

        // Assert
        Assert.False(isAuthentic);
    }
}
