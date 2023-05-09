using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.ClientScopes;
using Keycloak.Net.Core.Resources.ClientScopes;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IClientScopesClient
{
    public async Task<bool> CreateClientScopeAsync(string realm, ClientScope clientScope,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/client-scopes")
            .PostJsonAsync(clientScope, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<ClientScope>>
        GetClientScopesAsync(string realm, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/client-scopes")
            .GetJsonAsync<IEnumerable<ClientScope>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ClientScope> GetClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}")
            .GetJsonAsync<ClientScope>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateClientScopeAsync(string realm, string clientScopeId, ClientScope clientScope,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}")
            .PutJsonAsync(clientScope, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }
}