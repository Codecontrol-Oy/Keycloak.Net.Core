using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.ClientAttributeCertificate;

namespace Keycloak.Net.Core.Resources.ClientAttributeCertificate;

public interface IClientAttributeCertificateClient
{
    Task<Certificate> GetKeyInfoAsync(string realm, string clientId, string attribute,
        CancellationToken cancellationToken = default);

    Task<byte[]> GetKeyStoreForClientAsync(string realm, string clientId, string attribute,
        KeyStoreConfig keyStoreConfig, CancellationToken cancellationToken = default);

    Task<Certificate> GenerateCertificateWithNewKeyPairAsync(string realm, string clientId, string attribute,
        CancellationToken cancellationToken = default);

    Task<byte[]> GenerateCertificateWithNewKeyPairAndGetKeyStoreAsync(string realm, string clientId, string attribute,
        KeyStoreConfig keyStoreConfig, CancellationToken cancellationToken = default);

    Task<Certificate> UploadCertificateWithPrivateKeyAsync(string realm, string clientId, string attribute,
        string fileName, CancellationToken cancellationToken = default);

    Task<Certificate> UploadCertificateWithoutPrivateKeyAsync(string realm, string clientId, string attribute,
        string fileName, CancellationToken cancellationToken = default);
}