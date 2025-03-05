# TrustedWinner API

The TrustedWinner API provides a RESTful interface to execute secure, verifiable draws. It encapsulates all the cryptographic security and verification features of TrustedWinner in an easy-to-use HTTP API.

## Building and Running

### Prerequisites
- .NET 8.0 SDK or later
- (Optional) PFX password-protected certificate for result signing

### Configuration

The API can be configured through `appsettings.json` or environment variables:

```json
{
  "CertificateSettings": {
    "CertificatePath": "path/to/your/certificate.pfx",
    "CertificatePassword": "your-certificate-password"
  }
}
```

Environment variables:
- `CertificateSettings__CertificatePath`
- `CertificateSettings__CertificatePassword`

Any password protected PFX file containing both public and private parts can be used.
The certificate can be self-signed or issued by a third-party.

The TrustedWinner.Cli package includes a command to generate a self-signed certificate.


### Building
```bash
dotnet build
```

### Running
```bash
dotnet run
```

The API will start on `http://localhost:5064` by default.

## API Usage

### Execute an Instant Draw

**Endpoint**: POST `/api/Draw/instantDraw`

**Request Body**:
```json
{
  "entries": [
    "participant1@example.com",
    "participant2@example.com",
    "participant3@example.com"
  ],
  "configuration": {
    "winners": 1,
    "substitutes": 2
  },
  "additionalEntropy": "optional-extra-entropy-text"
}
```

**Response**: Returns a JSON object containing:
- TrustedWinner version
- Draw configuration
- Original entries list
- Cryptographic seed information
- Results (winners and their substitutes)
- Certificate and signature (if configured)

Example response:
```json
{
  "version": "1.0.0",
  "configuration": {
    "winners": 1,
    "substitutes": 1
  },
  "entries": [
    "participant1@example.com",
    "participant2@example.com",
    "participant3@example.com"
  ],
  "seed": {
    "timestamp": "2024-03-20T15:30:45.1234567Z",
    "randomPart": "ab12cd34...",
    "additionalEntropy": "optional-extra-entropy-text"
  },
  "results": [
    ["participant2@example.com", "participant1@example.com"]
  ],
  "certificate": "base64-encoded-public-certificate",
  "signature": "base64-encoded-signature"
}
```

### Execute a Persistent Draw

**Endpoint**: POST `/api/Draw/persistentDraw`

**Request Body**:
```json
{
  "contestId": "unique-contest-identifier",
  "title": "Contest Title",
  "entries": [
    "participant1@example.com",
    "participant2@example.com",
    "participant3@example.com"
  ],
  "configuration": {
    "winners": 1,
    "substitutes": 2
  },
  "additionalEntropy": "optional-extra-entropy-text"
}
```

**Response**: Returns a GUID representing the unique identifier of the created draw.

### Retrieve Draw Information

**Endpoint**: GET `/api/Draw/{id}`

Retrieves the main properties of a persisted draw by its ID.

**Response**: Returns a JSON object containing the draw information without the full auditable JSON.

### Retrieve Draw Auditable Result

**Endpoint**: GET `/api/Draw/{id}/auditableResult`

Retrieves the full auditable JSON of a persisted draw by its ID.

**Response**: Returns the complete auditable JSON result of the draw.

