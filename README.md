# TrustedWinner

TrustedWinner is a powerful and secure digital draw system that ensures fair and transparent winner selection for contests, giveaways, and promotions. Built with cryptographic security at its core, it provides an independently verifiable way to select winners while preventing any possibility of manipulation or fraud.

## Why TrustedWinner?

- **100% Transparent**: Every draw can be independently verified by anyone
- **Cryptographically Secure**: Uses advanced cryptographic methods to ensure true randomness
- **Anti-Fraud Protection**: Multiple layers of security prevent any form of manipulation
- **Easy to Use**: Simple configuration and clear, auditable results
- **Flexible**: Works with most types of contest entries

## Security Features

TrustedWinner implements several security measures to prevent fraud:

1. **Cryptographic Randomness**: Uses cryptographically secure random number generation
2. **Verifiable Seed**: Each draw uses a unique seed composed of:
   - Precise timestamp (down to the highest possible precision)
   - A randomized 256 bit seed
   - An optional extra entropy text to allow other external and easy-to-verify contributors to randomness
3. **Independent Verification**: The program provides an auditable results file:
   - Can be verified manually inspected
   - Can be verified automatically, independently, with the included verifier
4. **Unique Selection**: The system prevents duplicate winners or substitutes
5. **Immutable Results**: Once a draw is completed, the results cannot be altered
6. **Signed Results**: Proofs that a specific draw executor was used
7. **Certificate Management**: Built-in tools for creating and managing certificates to sign draw results

## Usage Options

TrustedWinner is designed to be flexible and can be integrated into any application:

### As a Library
The core functionality can be embedded in any .NET application, regardless of its interface or platform. As long as your application provides the generated JSON results file, the results can still be independently verified using this library or its command-line-interface.

For detailed usage instructions of the core library, see the [Library README](TrustedWinner.Core/README.md).

### RESTful API
A ready-to-use HTTP API is available, making it easy to integrate TrustedWinner into any application regardless of the programming language. The API provides secure draw execution with result signing and verifiable results file.

For detailed API documentation and setup instructions, see the [API README](TrustedWinner.Api/README.md).

### Command Line Interface
This repository includes a command-line interface.
It provides:
- Easy execution of draws
- Verification of draw results
- Certificate creation tool
- Example implementation of the library's usage

For detailed usage instructions, see the [CLI Readme](TrustedWinner.Core.Cli/README.md).
