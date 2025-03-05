# TrustedWinner CLI

This is the command-line interface for TrustedWinner, providing an easy way to execute draws and verify their results. It serves as both a practical tool and an example of how to use the TrustedWinner library in your own applications.

## Commands

### Draw Command

Executes a new draw with the specified parameters.

```bash
trustedwinner draw <entries-file> [options]
```

#### Arguments
- `entries-file`: JSON file containing the array of entries to draw from

#### Options
- `--winners <number>`: Number of winners to select (default: 1)
- `--substitutes <number>`: Number of substitutes per winner (default: 0)
- `--output <file>`: File where to save the auditable draw result. If not specified, defaults to `[entries-file]-result.json`
- `--entropy <string>`: Optional extra entropy to use in the draw
- `--certificate <file>`: Optional certificate file (PEM format) to sign the draw results

#### Example
```bash
trustedwinner draw entries.json --winners 3 --substitutes 2 --output result.json
```

### Verify Command

Verifies the authenticity of a draw result.

```bash
trustedwinner verify <file>
```

#### Arguments
- `file`: The JSON file containing the draw result to verify

#### Example
```bash
trustedwinner verify result.json
```

### Create Certificate Command

Creates a new certificate for signing draw results.

```bash
trustedwinner create-certificate <output-file> [options]
```

#### Arguments
- `output-file`: File where to save the certificate (PFX format)

#### Options
- `--password <string>`: Password to protect the certificate private key (required)
- `--common-name <string>`: Common Name (CN) for the certificate (default: "TrustedWinner")
- `--organization <string>`: Organization (O) name (e.g., "Your Company Ltd")
- `--country <string>`: Two-letter country code (C) (e.g., "US", "GB", "ES")
- `--validity-years <number>`: Number of years the certificate will be valid (default: 1)

#### Example
```bash
trustedwinner create-certificate my-cert.pfx --password "secure123" --common-name "My Contest" --organization "My Company Ltd" --country "US" --validity-years 2
```

## Input File Format

The entries file should be a JSON array of strings. For example:

```json
[
    "Entry 1",
    "Entry 2",
    "Entry 3"
]
```

## Output

The draw command will:
1. Display the selected winners and substitutes in the console
2. Save an auditable and verifiable JSON result file

The verify command will:
1. Check if the draw result is authentic and hasn't been tampered with
2. Display certificate information if the result was signed
3. Exit with code 0 if authentic, 1 if tampered with


## Building from Source

To build the executable from source:

1. Ensure you have the .NET SDK installed (version 8.0 or later)
   - Download from: https://dotnet.microsoft.com/download

2. Run the following command to create a self-contained executable:

```bash
dotnet publish -c Release --self-contained true -o ./publish
```

The executable will be created as:
- `publish/TrustedWinner.Core.Cli.exe` on Windows
- `publish/TrustedWinner.Core.Cli` on Linux and macOS
