using System.Text.Json;
using System.Text.Json.Nodes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace TrustedWinner.Core;

/// <summary>
/// Main class of the library.
/// Executes a draw based on the provided configuration and entries
/// </summary>
public class DrawExecutor
{
    private readonly Configuration _configuration;
    private readonly string[] _entries;
    private readonly X509Certificate2? _signingCertificate;
    private string[][]? _results;
    private Seed? _seed;
    private bool _drawExecuted;

    /// <summary>
    /// Creates a new DrawExecutor instance with the given configuration, entries, and optional signing certificate.
    /// </summary>
    /// <param name="configuration">The configuration for the draw.</param>
    /// <param name="entries">The entries to select from.</param>
    /// <param name="signingCertificate">Optional certificate to use for signing the draw results.</param>
    public DrawExecutor(Configuration configuration, string[] entries, X509Certificate2? signingCertificate = null)
    {
        _configuration = configuration;
        _entries = entries;
        
        if (signingCertificate != null)
        {
            // Check if the certificate has a private key
            using var rsa = signingCertificate.GetRSAPrivateKey();
            if (rsa == null)
            {
                throw new InvalidOperationException("The provided certificate does not have a private key suitable for signing.");
            }
            _signingCertificate = signingCertificate;
        }

        ValidateEntries(_entries);
    }

    /// <summary>
    /// Executes the draw with optional additional entropy.
    /// </summary>
    /// <param name="additionalEntropy">Optional additional entropy to use for seed generation.</param>
    /// <returns>A two-dimensional array containing the draw results.</returns>
    /// <exception cref="InvalidOperationException">Thrown if draw has already been executed.</exception>
    public string[][] ExecuteDraw(string additionalEntropy = "")
    {
        EnsureDrawNotExecuted();
        var seed = SeedGenerator.Generate(additionalEntropy);
        _seed = seed;
        _results = ExecuteDrawWithSeed(seed);
        _drawExecuted = true;
        return _results;
    }

    /// <summary>
    /// Simulates a draw with a specific seed.
    /// </summary>
    /// <param name="seed">The seed to use for the draw simulation.</param>
    /// <returns>A two-dimensional array containing the draw results.</returns>
    /// <exception cref="InvalidOperationException">Thrown if draw has already been executed.</exception>
    public string[][] SimulateDraw(Seed seed)
    {
        EnsureDrawNotExecuted();
        _seed = seed;
        _results = ExecuteDrawWithSeed(seed);
        _drawExecuted = true;
        return _results;
    }

    /// <summary>
    /// Gets the results of the draw as a two-dimensional array.
    /// The first dimension represents each winner, and the second dimension contains the winner and their substitutes.
    /// </summary>
    /// <returns>A two-dimensional array containing the draw results.</returns>
    /// <exception cref="InvalidOperationException">Thrown if draw has not been executed yet.</exception>
    public string[][] GetResults()
    {
        if (!_drawExecuted || _results == null)
        {
            throw new InvalidOperationException("Draw has not been executed yet. Call ExecuteDraw() or SimulateDraw() first.");
        }
        return _results;
    }

    /// <summary>
    /// Gets the complete auditable draw result as a JSON string containing all inputs and outputs.
    /// The JSON includes the version, configuration, entries, seed, and results.
    /// </summary>
    /// <returns>A JSON string containing all draw details in a format suitable for auditing.</returns>
    /// <exception cref="InvalidOperationException">Thrown if draw has not been executed yet.</exception>
    public string GetAuditableJson()
    {
        if (!_drawExecuted || _results == null || !_seed.HasValue)
        {
            throw new InvalidOperationException("Draw has not been executed yet. Call ExecuteDraw() or SimulateDraw() first.");
        }

        var drawResult = new DrawResult
        {
            Version = ProjectInfo.Version,
            Configuration = _configuration,
            Entries = _entries,
            Seed = _seed.Value,
            Results = _results,
            Certificate = _signingCertificate?.ExportCertificatePem(),
            Signature = _signingCertificate != null ? SignSelectedEntries(_results, _signingCertificate) : null
        };

        return JsonSerializer.Serialize(drawResult, DrawResult.SerializerOptions);
    }

    private string[][] ExecuteDrawWithSeed(Seed seed)
    {
        var algorithm = new SelectionAlgorithm(
            seed: seed.ToString(),
            entries: _entries);

        var results = new string[_configuration.Winners][];
        for (uint i = 0; i < _configuration.Winners; i++)
        {
            results[i] = algorithm.SelectWinnerWithSubstitutes(_configuration.SubstitutesPerWinner);
        }

        return results;
    }

    private void EnsureDrawNotExecuted()
    {
        if (_drawExecuted)
        {
            throw new InvalidOperationException("Draw has already been executed for this instance.");
        }
    }

    private static string SignSelectedEntries(string[][] results, X509Certificate2 signingCertificate)
    {
        return ResultSigner.SignResults(results, signingCertificate);
    }

    private static void ValidateEntries(string[] entries)
    {
        if (entries == null || entries.Length == 0)
        {
            throw new InvalidOperationException("Entries cannot be empty");
        }

        // Check for duplicates
        if (entries.Length != entries.Distinct().Count())
        {
            throw new InvalidOperationException("Duplicate entries found");
        }
    }
} 