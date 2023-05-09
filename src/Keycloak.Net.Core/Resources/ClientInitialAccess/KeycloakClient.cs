using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.ClientInitialAccess;
using Keycloak.Net.Core.Resources.ClientInitialAccess;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IClientInitialAccessClient
{
    public async Task<ClientInitialAccessPresentation> CreateInitialAccessTokenAsync(string realm,
        ClientInitialAccessCreatePresentation create, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients-initial-access")
            .PostJsonAsync(create, cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<ClientInitialAccessPresentation>()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<ClientInitialAccessPresentation>> GetClientInitialAccessAsync(string realm,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients-initial-access")
            .GetJsonAsync<IEnumerable<ClientInitialAccessPresentation>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> DeleteInitialAccessTokenAsync(string realm, string clientInitialAccessTokenId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients-initial-access/{clientInitialAccessTokenId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }
}