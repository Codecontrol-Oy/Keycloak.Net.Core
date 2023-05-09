using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.ClientAttributeCertificate;
using Keycloak.Net.Core.Resources.ClientAttributeCertificate;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IClientAttributeCertificateClient
{
    public async Task<Certificate> GetKeyInfoAsync(string realm, string clientId, string attribute,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/certificates/{attribute}")
            .GetJsonAsync<Certificate>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<byte[]> GetKeyStoreForClientAsync(string realm, string clientId, string attribute,
        KeyStoreConfig keyStoreConfig, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/certificates/{attribute}/download")
            .PostJsonAsync(keyStoreConfig, cancellationToken)
            .HandleErrorsAsync()
            .ReceiveBytes()
            .ConfigureAwait(false);
    }

    public async Task<Certificate> GenerateCertificateWithNewKeyPairAsync(string realm, string clientId,
        string attribute, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/certificates/{attribute}/generate")
            .PostAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<Certificate>()
            .ConfigureAwait(false);
    }

    public async Task<byte[]> GenerateCertificateWithNewKeyPairAndGetKeyStoreAsync(string realm, string clientId,
        string attribute, KeyStoreConfig keyStoreConfig, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment(
                $"/admin/realms/{realm}/clients/{clientId}/certificates/{attribute}/generate-and-download")
            .PostJsonAsync(keyStoreConfig, cancellationToken)
            .HandleErrorsAsync()
            .ReceiveBytes()
            .ConfigureAwait(false);
    }

    public async Task<Certificate> UploadCertificateWithPrivateKeyAsync(string realm, string clientId, string attribute,
        string fileName, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/certificates/{attribute}/upload")
            .PostMultipartAsync(content => content.AddFile(Path.GetFileName(fileName), Path.GetDirectoryName(fileName)),
                cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<Certificate>()
            .ConfigureAwait(false);
    }

    public async Task<Certificate> UploadCertificateWithoutPrivateKeyAsync(string realm, string clientId,
        string attribute, string fileName, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/certificates/{attribute}/upload-certificate")
            .PostMultipartAsync(content => content.AddFile(Path.GetFileName(fileName), Path.GetDirectoryName(fileName)),
                cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<Certificate>()
            .ConfigureAwait(false);
    }
}