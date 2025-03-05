# TrustedWinner.Core Library

This is the core library of TrustedWinner, providing the main functionality for executing draws and verifying their results.

Please refer to https://github.com/winningconsulting/TrustedWinner for more documentation and related tools.

## Main Components

### DrawExecutor

The `DrawExecutor` class is responsible for executing draws with the following features:

- Configurable number of winners and substitutes
- Cryptographically secure random selection
- Optional certificate-based signing of results
- Full audit trail of the draw process

Basic usage:

```csharp
// Create a configuration with 3 winners and 2 substitutes per winner
var config = new Configuration(Winners: 3, SubstitutesPerWinner: 2);

// Prepare your entries
var entries = new[] { "Entry 1", "Entry 2", "Entry 3", /* ... */ };

// Execute the draw
var executor = new DrawExecutor(config, entries);

// Get the results (array of draws with array of selected entries, first being the winner)
var results = executor.GetResults();

// Get an auditable json of the results
var json = executor.GetAuditableJson();
```

### DrawVerifier

The `DrawVerifier` class allows you to verify the authenticity of draw results:

- Validates that results haven't been tampered with
- Verifies cryptographic signatures if present

Basic usage:

```csharp
// Read the draw result JSON
var json = File.ReadAllText("draw-result.json");

// Verify the result
if (DrawVerifier.Verify(json))
{
    Console.WriteLine("Draw result is authentic!");
}
else
{
    Console.WriteLine("Draw result has been tampered with!");
}
```

## Documentation

For detailed API documentation, including all available methods, properties, and configuration options, see the [automatically generated documentation](docs/index.md).

## Adding to your application

### Via NuGet Package Manager
```shell
Install-Package Winning.TrustedWinner.Core
```

### Via .NET CLI
```shell
dotnet add package Winning.TrustedWinner.Core
```

### Via PackageReference in .csproj file
```xml
<PackageReference Include="Winning.TrustedWinner.Core" Version="2.0.2" />
```

## Building from Source

Since trust is an important factor, you can build it by yourself.

To build the library from source:

1. Clone the repository
2. Navigate to the TrustedWinner.Core directory
3. Run the following commands:
```shell
dotnet restore
dotnet build
```

For Release build:
```shell
dotnet build -c Release
```

The compiled library will be available in the `bin/Debug` or `bin/Release` directory depending on your build configuration.
