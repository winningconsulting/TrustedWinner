#### [TrustedWinner.Core](index.md 'index')

## TrustedWinner.Core Assembly
### Namespaces

<a name='TrustedWinner.Core'></a>

## TrustedWinner.Core Namespace
### Classes

<a name='TrustedWinner.Core.Configuration'></a>

## Configuration Class

Represents the configuration for a draw, specifying the number of winners and substitutes per winner.

```csharp
public class Configuration :
System.IEquatable<TrustedWinner.Core.Configuration>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; Configuration

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[Configuration](index.md#TrustedWinner.Core.Configuration 'TrustedWinner.Core.Configuration')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Constructors

<a name='TrustedWinner.Core.Configuration.Configuration(uint,uint)'></a>

## Configuration(uint, uint) Constructor

Represents the configuration for a draw, specifying the number of winners and substitutes per winner.

```csharp
public Configuration(uint Winners, uint SubstitutesPerWinner);
```
#### Parameters

<a name='TrustedWinner.Core.Configuration.Configuration(uint,uint).Winners'></a>

`Winners` [System.UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32 'System.UInt32')

The number of winners to select in the draw.

<a name='TrustedWinner.Core.Configuration.Configuration(uint,uint).SubstitutesPerWinner'></a>

`SubstitutesPerWinner` [System.UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32 'System.UInt32')

The number of substitutes to select for each winner.
### Properties

<a name='TrustedWinner.Core.Configuration.SubstitutesPerWinner'></a>

## Configuration.SubstitutesPerWinner Property

The number of substitutes to select for each winner.

```csharp
public uint SubstitutesPerWinner { get; set; }
```

#### Property Value
[System.UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32 'System.UInt32')

<a name='TrustedWinner.Core.Configuration.Winners'></a>

## Configuration.Winners Property

The number of winners to select in the draw.

```csharp
public uint Winners { get; set; }
```

#### Property Value
[System.UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32 'System.UInt32')

<a name='TrustedWinner.Core.DrawExecutor'></a>

## DrawExecutor Class

Main class of the library.  
Executes a draw based on the provided configuration and entries

```csharp
public class DrawExecutor
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; DrawExecutor
### Constructors

<a name='TrustedWinner.Core.DrawExecutor.DrawExecutor(TrustedWinner.Core.Configuration,string[],System.Security.Cryptography.X509Certificates.X509Certificate2)'></a>

## DrawExecutor(Configuration, string[], X509Certificate2) Constructor

Creates a new DrawExecutor instance with the given configuration, entries, and optional signing certificate.

```csharp
public DrawExecutor(TrustedWinner.Core.Configuration configuration, string[] entries, System.Security.Cryptography.X509Certificates.X509Certificate2? signingCertificate=null);
```
#### Parameters

<a name='TrustedWinner.Core.DrawExecutor.DrawExecutor(TrustedWinner.Core.Configuration,string[],System.Security.Cryptography.X509Certificates.X509Certificate2).configuration'></a>

`configuration` [Configuration](index.md#TrustedWinner.Core.Configuration 'TrustedWinner.Core.Configuration')

The configuration for the draw.

<a name='TrustedWinner.Core.DrawExecutor.DrawExecutor(TrustedWinner.Core.Configuration,string[],System.Security.Cryptography.X509Certificates.X509Certificate2).entries'></a>

`entries` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

The entries to select from.

<a name='TrustedWinner.Core.DrawExecutor.DrawExecutor(TrustedWinner.Core.Configuration,string[],System.Security.Cryptography.X509Certificates.X509Certificate2).signingCertificate'></a>

`signingCertificate` [System.Security.Cryptography.X509Certificates.X509Certificate2](https://docs.microsoft.com/en-us/dotnet/api/System.Security.Cryptography.X509Certificates.X509Certificate2 'System.Security.Cryptography.X509Certificates.X509Certificate2')

Optional certificate to use for signing the draw results.
### Methods

<a name='TrustedWinner.Core.DrawExecutor.ExecuteDraw(string)'></a>

## DrawExecutor.ExecuteDraw(string) Method

Executes the draw with optional additional entropy.

```csharp
public string[][] ExecuteDraw(string additionalEntropy="");
```
#### Parameters

<a name='TrustedWinner.Core.DrawExecutor.ExecuteDraw(string).additionalEntropy'></a>

`additionalEntropy` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

Optional additional entropy to use for seed generation.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
A two-dimensional array containing the draw results.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown if draw has already been executed.

<a name='TrustedWinner.Core.DrawExecutor.GetAuditableJson()'></a>

## DrawExecutor.GetAuditableJson() Method

Gets the complete auditable draw result as a JSON string containing all inputs and outputs.  
The JSON includes the version, configuration, entries, seed, and results.

```csharp
public string GetAuditableJson();
```

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
A JSON string containing all draw details in a format suitable for auditing.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown if draw has not been executed yet.

<a name='TrustedWinner.Core.DrawExecutor.GetResults()'></a>

## DrawExecutor.GetResults() Method

Gets the results of the draw as a two-dimensional array.  
The first dimension represents each winner, and the second dimension contains the winner and their substitutes.

```csharp
public string[][] GetResults();
```

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
A two-dimensional array containing the draw results.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown if draw has not been executed yet.

<a name='TrustedWinner.Core.DrawExecutor.SimulateDraw(TrustedWinner.Core.Seed)'></a>

## DrawExecutor.SimulateDraw(Seed) Method

Simulates a draw with a specific seed.

```csharp
public string[][] SimulateDraw(TrustedWinner.Core.Seed seed);
```
#### Parameters

<a name='TrustedWinner.Core.DrawExecutor.SimulateDraw(TrustedWinner.Core.Seed).seed'></a>

`seed` [Seed](index.md#TrustedWinner.Core.Seed 'TrustedWinner.Core.Seed')

The seed to use for the draw simulation.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
A two-dimensional array containing the draw results.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown if draw has already been executed.

<a name='TrustedWinner.Core.DrawResult'></a>

## DrawResult Class

Represents the result of a draw, including all inputs and outputs.

```csharp
public class DrawResult
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; DrawResult
### Fields

<a name='TrustedWinner.Core.DrawResult.SerializerOptions'></a>

## DrawResult.SerializerOptions Field

JSON serialization options for DrawResult serialization and deserialization.

```csharp
public static readonly JsonSerializerOptions SerializerOptions;
```

#### Field Value
[System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')
### Properties

<a name='TrustedWinner.Core.DrawResult.Certificate'></a>

## DrawResult.Certificate Property

The public certificate used to sign the draw result, if any.

```csharp
public string? Certificate { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='TrustedWinner.Core.DrawResult.Configuration'></a>

## DrawResult.Configuration Property

The configuration used for the draw.

```csharp
public TrustedWinner.Core.Configuration Configuration { get; set; }
```

#### Property Value
[Configuration](index.md#TrustedWinner.Core.Configuration 'TrustedWinner.Core.Configuration')

<a name='TrustedWinner.Core.DrawResult.Entries'></a>

## DrawResult.Entries Property

The entries that were used in the draw.

```csharp
public string[] Entries { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

<a name='TrustedWinner.Core.DrawResult.Results'></a>

## DrawResult.Results Property

The results of the draw. Each element is an array containing a winner and their substitutes.

```csharp
public string[][] Results { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

<a name='TrustedWinner.Core.DrawResult.Seed'></a>

## DrawResult.Seed Property

The seed used for random selection.

```csharp
public TrustedWinner.Core.Seed Seed { get; set; }
```

#### Property Value
[Seed](index.md#TrustedWinner.Core.Seed 'TrustedWinner.Core.Seed')

<a name='TrustedWinner.Core.DrawResult.Signature'></a>

## DrawResult.Signature Property

The signature of the draw result, if signed with a certificate.

```csharp
public string? Signature { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='TrustedWinner.Core.DrawResult.Version'></a>

## DrawResult.Version Property

The version of TrustedWinner.Core that was used to generate this draw result.

```csharp
public string Version { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='TrustedWinner.Core.DrawVerifier'></a>

## DrawVerifier Class

Verifies the authenticity of a draw result.

```csharp
public class DrawVerifier
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; DrawVerifier
### Methods

<a name='TrustedWinner.Core.DrawVerifier.GetVersion(string)'></a>

## DrawVerifier.GetVersion(string) Method

Gets the version of the TrustedWinner.Core library declared in a draw result.

```csharp
public static string GetVersion(string json);
```
#### Parameters

<a name='TrustedWinner.Core.DrawVerifier.GetVersion(string).json'></a>

`json` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The JSON string containing the draw result.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
The version of the TrustedWinner.Core library.

<a name='TrustedWinner.Core.DrawVerifier.IsAuthentic(string)'></a>

## DrawVerifier.IsAuthentic(string) Method

Verifies that a draw result is authentic and has not been tampered with.

```csharp
public static bool IsAuthentic(string json);
```
#### Parameters

<a name='TrustedWinner.Core.DrawVerifier.IsAuthentic(string).json'></a>

`json` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The JSON string containing the draw result to verify.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the draw result is authentic, false if it appears to have been tampered with.

#### Exceptions

[System.Text.Json.JsonException](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonException 'System.Text.Json.JsonException')  
Thrown when the JSON is invalid or does not match the expected schema.

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when the entries are invalid (e.g., duplicates).

<a name='TrustedWinner.Core.ProjectInfo'></a>

## ProjectInfo Class

Provides information about the TrustedWinner.Core library.

```csharp
public static class ProjectInfo
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ProjectInfo
### Properties

<a name='TrustedWinner.Core.ProjectInfo.Version'></a>

## ProjectInfo.Version Property

Gets the version of the TrustedWinner.Core library.

```csharp
public static string Version { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when the version cannot be retrieved from the assembly.

<a name='TrustedWinner.Core.ResultSigner'></a>

## ResultSigner Class

Handles signing and verification of draw results using X.509 certificates.

```csharp
public class ResultSigner
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ResultSigner
### Methods

<a name='TrustedWinner.Core.ResultSigner.SignResults(string[][],System.Security.Cryptography.X509Certificates.X509Certificate2)'></a>

## ResultSigner.SignResults(string[][], X509Certificate2) Method

Signs the provided results array using the given certificate.

```csharp
public static string SignResults(string[][] results, System.Security.Cryptography.X509Certificates.X509Certificate2 signingCertificate);
```
#### Parameters

<a name='TrustedWinner.Core.ResultSigner.SignResults(string[][],System.Security.Cryptography.X509Certificates.X509Certificate2).results'></a>

`results` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

The results array to sign.

<a name='TrustedWinner.Core.ResultSigner.SignResults(string[][],System.Security.Cryptography.X509Certificates.X509Certificate2).signingCertificate'></a>

`signingCertificate` [System.Security.Cryptography.X509Certificates.X509Certificate2](https://docs.microsoft.com/en-us/dotnet/api/System.Security.Cryptography.X509Certificates.X509Certificate2 'System.Security.Cryptography.X509Certificates.X509Certificate2')

The certificate to use for signing.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
The Base64-encoded signature.

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
Thrown when results or signingCertificate is null.

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when the certificate doesn't have a private key.

<a name='TrustedWinner.Core.ResultSigner.VerifySignature(string[][],string,string)'></a>

## ResultSigner.VerifySignature(string[][], string, string) Method

Verifies the signature of the provided results using the given certificate.

```csharp
public static bool VerifySignature(string[][] results, string signature, string certificatePem);
```
#### Parameters

<a name='TrustedWinner.Core.ResultSigner.VerifySignature(string[][],string,string).results'></a>

`results` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

The results array that was signed.

<a name='TrustedWinner.Core.ResultSigner.VerifySignature(string[][],string,string).signature'></a>

`signature` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The Base64-encoded signature to verify.

<a name='TrustedWinner.Core.ResultSigner.VerifySignature(string[][],string,string).certificatePem'></a>

`certificatePem` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The PEM-encoded certificate used for signing.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the signature is valid, false otherwise.

<a name='TrustedWinner.Core.SeedGenerator'></a>

## SeedGenerator Class

Generates unique, verifiable seeds for random number generation.

```csharp
public static class SeedGenerator
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; SeedGenerator
### Methods

<a name='TrustedWinner.Core.SeedGenerator.Generate(string)'></a>

## SeedGenerator.Generate(string) Method

Generates a new seed using the current UTC timestamp, cryptographically secure random string,  
and optional additional entropy.

```csharp
public static TrustedWinner.Core.Seed Generate(string additionalEntropy="");
```
#### Parameters

<a name='TrustedWinner.Core.SeedGenerator.Generate(string).additionalEntropy'></a>

`additionalEntropy` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

Optional string to add additional verifiable entropy to the seed.

#### Returns
[Seed](index.md#TrustedWinner.Core.Seed 'TrustedWinner.Core.Seed')  
A new seed instance with the provided additional entropy.

<a name='TrustedWinner.Core.SelectionAlgorithm'></a>

## SelectionAlgorithm Class

Implements a deterministic random selection algorithm for choosing winners and substitutes.  
The algorithm uses a seed to ensure reproducible results and maintains a set of selected entries  
to prevent duplicates.

```csharp
public class SelectionAlgorithm
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; SelectionAlgorithm
### Constructors

<a name='TrustedWinner.Core.SelectionAlgorithm.SelectionAlgorithm(string,string[])'></a>

## SelectionAlgorithm(string, string[]) Constructor

Initializes a new instance of the SelectionAlgorithm with a seed and list of entries.

```csharp
public SelectionAlgorithm(string seed, string[] entries);
```
#### Parameters

<a name='TrustedWinner.Core.SelectionAlgorithm.SelectionAlgorithm(string,string[]).seed'></a>

`seed` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A string seed used to initialize the random number generator.

<a name='TrustedWinner.Core.SelectionAlgorithm.SelectionAlgorithm(string,string[]).entries'></a>

`entries` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

The list of entries to select from.
### Methods

<a name='TrustedWinner.Core.SelectionAlgorithm.SelectWinnerWithSubstitutes(uint)'></a>

## SelectionAlgorithm.SelectWinnerWithSubstitutes(uint) Method

Selects a winner and their substitutes from the available entries.

```csharp
public string[] SelectWinnerWithSubstitutes(uint substitutesCount);
```
#### Parameters

<a name='TrustedWinner.Core.SelectionAlgorithm.SelectWinnerWithSubstitutes(uint).substitutesCount'></a>

`substitutesCount` [System.UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32 'System.UInt32')

The number of substitutes to select for the winner.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
An array containing the winner as the first element, followed by the substitutes.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Thrown when there are no more entries available to select.
### Structs

<a name='TrustedWinner.Core.Seed'></a>

## Seed Struct

Represents a seed used for deterministic random selection.  
A seed consists of a timestamp, a cryptographically secure random part, and an optional entropy string  
to ensure uniqueness and verifiability.

```csharp
public readonly struct Seed :
System.IEquatable<TrustedWinner.Core.Seed>
```

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[Seed](index.md#TrustedWinner.Core.Seed 'TrustedWinner.Core.Seed')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Constructors

<a name='TrustedWinner.Core.Seed.Seed(System.DateTime,string,string)'></a>

## Seed(DateTime, string, string) Constructor

Represents a seed used for deterministic random selection.  
A seed consists of a timestamp, a cryptographically secure random part, and an optional entropy string  
to ensure uniqueness and verifiability.

```csharp
public Seed(System.DateTime Timestamp, string RandomPart, string AdditionalEntropy="");
```
#### Parameters

<a name='TrustedWinner.Core.Seed.Seed(System.DateTime,string,string).Timestamp'></a>

`Timestamp` [System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')

The UTC timestamp when the seed was created.

<a name='TrustedWinner.Core.Seed.Seed(System.DateTime,string,string).RandomPart'></a>

`RandomPart` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A cryptographically secure random string.

<a name='TrustedWinner.Core.Seed.Seed(System.DateTime,string,string).AdditionalEntropy'></a>

`AdditionalEntropy` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

Optional additional entropy that can be provided for extra verification.
### Properties

<a name='TrustedWinner.Core.Seed.AdditionalEntropy'></a>

## Seed.AdditionalEntropy Property

Optional additional entropy that can be provided for extra verification.

```csharp
public string AdditionalEntropy { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='TrustedWinner.Core.Seed.RandomPart'></a>

## Seed.RandomPart Property

A cryptographically secure random string.

```csharp
public string RandomPart { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='TrustedWinner.Core.Seed.Timestamp'></a>

## Seed.Timestamp Property

The UTC timestamp when the seed was created.

```csharp
public System.DateTime Timestamp { get; set; }
```

#### Property Value
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')
### Methods

<a name='TrustedWinner.Core.Seed.ToString()'></a>

## Seed.ToString() Method

Returns a string representation of the seed that can be used for random number generation.  
The format is: timestamp|randomPart|additionalEntropy (empty string if no additional entropy)

```csharp
public override string ToString();
```

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
A string combining all seed components.