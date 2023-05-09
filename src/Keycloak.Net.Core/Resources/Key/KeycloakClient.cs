using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Key;
using Keycloak.Net.Core.Resources.Key;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IKeyClient
{
    public async Task<KeysMetadata> GetKeysAsync(string realm, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/keys")
            .GetJsonAsync<KeysMetadata>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }
}