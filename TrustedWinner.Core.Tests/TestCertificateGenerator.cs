using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace TrustedWinner.Core.Tests;

public static class TestCertificateGenerator
{
    public static X509Certificate2 GenerateCertificate()
    {
        // Generate a new RSA key pair
        using var rsa = RSA.Create(2048);

        // Create a certificate request
        var request = new CertificateRequest(
            "CN=Test Certificate",
            rsa,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);

        // Add basic constraints
        request.CertificateExtensions.Add(
            new X509BasicConstraintsExtension(true, false, 0, true));

        // Add key usage
        request.CertificateExtensions.Add(
            new X509KeyUsageExtension(
                X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.KeyEncipherment,
                true));

        // Create the certificate
        var certificate = request.CreateSelfSigned(
            DateTimeOffset.UtcNow.AddDays(-1),
            DateTimeOffset.UtcNow.AddDays(1));

        // Return a new certificate with the private key
        return new X509Certificate2(certificate.Export(X509ContentType.Pfx));
    }

    public static X509Certificate2 GenerateCertificateWithoutPrivateKey()
    {
        var cert = GenerateCertificate();
        return new X509Certificate2(cert.Export(X509ContentType.Cert));
    }
} 