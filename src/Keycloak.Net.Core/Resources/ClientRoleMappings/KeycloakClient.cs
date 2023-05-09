using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Roles;
using Keycloak.Net.Core.Resources.ClientRoleMappings;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IClientRoleMappingsClient
{
    public async Task<bool> AddClientRoleMappingsToGroupAsync(string realm, string groupId, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/role-mappings/clients/{clientId}")
            .PostJsonAsync(roles, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetClientRoleMappingsForGroupAsync(string realm, string groupId,
        string clientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/role-mappings/clients/{clientId}")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> DeleteClientRoleMappingsFromGroupAsync(string realm, string groupId, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/role-mappings/clients/{clientId}")
            .SendJsonAsync(HttpMethod.Delete, roles, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetAvailableClientRoleMappingsForGroupAsync(string realm, string groupId,
        string clientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/role-mappings/clients/{clientId}/available")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Role>> GetEffectiveClientRoleMappingsForGroupAsync(string realm, string groupId,
        string clientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/role-mappings/clients/{clientId}/composite")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> AddClientRoleMappingsToUserAsync(string realm, string userId, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/role-mappings/clients/{clientId}")
            .PostJsonAsync(roles, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetClientRoleMappingsForUserAsync(string realm, string userId, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/role-mappings/clients/{clientId}")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> DeleteClientRoleMappingsFromUserAsync(string realm, string userId, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/role-mappings/clients/{clientId}")
            .SendJsonAsync(HttpMethod.Delete, roles, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetAvailableClientRoleMappingsForUserAsync(string realm, string userId,
        string clientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/role-mappings/clients/{clientId}/available")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Role>> GetEffectiveClientRoleMappingsForUserAsync(string realm, string userId,
        string clientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/role-mappings/clients/{clientId}/composite")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }
}