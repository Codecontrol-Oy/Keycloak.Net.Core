using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Common.Extensions;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Clients;
using Keycloak.Net.Core.Models.ClientScopes;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Roles;
using Keycloak.Net.Core.Models.Users;
using Keycloak.Net.Core.Resources.Clients;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IClientsClient
{
    public async Task<bool> CreateClientAsync(string realm, Client client,
        CancellationToken cancellationToken = default)
    {
        var response =
            await InternalCreateClientAsync(realm, client, cancellationToken)
                .HandleErrorsAsync().ConfigureAwait(false);

        return response.IsSuccessStatusCode;
    }

    public async Task<string> CreateClientAndRetrieveClientIdAsync(string realm, Client client,
        CancellationToken cancellationToken = default)
    {
        var response =
            await InternalCreateClientAsync(realm, client, cancellationToken)
                .HandleErrorsAsync().ConfigureAwait(false);

        var locationPathAndQuery = response.Headers.Location.PathAndQuery;
        var clientId = response.IsSuccessStatusCode
            ? locationPathAndQuery.Substring(locationPathAndQuery.LastIndexOf("/", StringComparison.Ordinal) + 1)
            : null;
        return clientId;
    }

    public async Task<IEnumerable<Client>> GetClientsAsync(string realm, string clientId = null,
        bool? viewableOnly = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(clientId)] = clientId,
            [nameof(viewableOnly)] = viewableOnly
        };

        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/clients")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<Client>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<Client>
        GetClientAsync(string realm, string clientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}")
            .GetJsonAsync<Client>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateClientAsync(string realm, string clientId, Client client,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}")
            .PutJsonAsync(client, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteClientAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<Credentials> GenerateClientSecretAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/client-secret")
            .PostJsonAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<Credentials>()
            .ConfigureAwait(false);
    }

    public async Task<Credentials> GetClientSecretAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/client-secret")
            .GetJsonAsync<Credentials>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<ClientScope>> GetDefaultClientScopesAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/default-client-scopes")
            .GetJsonAsync<IEnumerable<ClientScope>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateDefaultClientScopeAsync(string realm, string clientId, string clientScopeId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/default-client-scopes/{clientScopeId}")
            .PutAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteDefaultClientScopeAsync(string realm, string clientId, string clientScopeId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/default-client-scopes/{clientScopeId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    [Obsolete("Not working yet")]
    public async Task<AccessToken> GenerateClientExampleAccessTokenAsync(string realm, string clientId,
        string scope = null, string userId = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(scope)] = scope,
            [nameof(userId)] = userId
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment(
                $"/admin/realms/{realm}/clients/{clientId}/evaluate-scopes/generate-example-access-token")
            .SetQueryParams(queryParams)
            .GetJsonAsync<AccessToken>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<ClientScopeEvaluateResourceProtocolMapperEvaluation>>
        GetProtocolMappersInTokenGenerationAsync(string realm, string clientId, string scope = null,
            CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(scope)] = scope
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/evaluate-scopes/protocol-mappers")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<ClientScopeEvaluateResourceProtocolMapperEvaluation>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Role>> GetClientGrantedScopeMappingsAsync(string realm, string clientId,
        string roleContainerId, string scope = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(scope)] = scope
        };

        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment(
                $"/admin/realms/{realm}/clients/{clientId}/evaluate-scopes/scope-mappings/{roleContainerId}/granted")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Role>> GetClientNotGrantedScopeMappingsAsync(string realm, string clientId,
        string roleContainerId, string scope = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(scope)] = scope
        };

        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment(
                $"/admin/realms/{realm}/clients/{clientId}/evaluate-scopes/scope-mappings/{roleContainerId}/not-granted")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    [Obsolete("Not working yet")]
    public async Task<string> GetClientProviderAsync(string realm, string clientId, string providerId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/installation/providers/{providerId}")
            .GetStringAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ManagementPermission> GetClientAuthorizationPermissionsInitializedAsync(string realm,
        string clientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/management/permissions")
            .GetJsonAsync<ManagementPermission>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ManagementPermission> SetClientAuthorizationPermissionsInitializedAsync(string realm,
        string clientId, ManagementPermission managementPermission, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/management/permissions")
            .PutJsonAsync(managementPermission, cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<ManagementPermission>()
            .ConfigureAwait(false);
    }

    public async Task<bool> RegisterClientClusterNodeAsync(string realm, string clientId,
        IDictionary<string, object> formParams, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/nodes")
            .PostJsonAsync(formParams, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UnregisterClientClusterNodeAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/nodes")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<int> GetClientOfflineSessionCountAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        var result = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/offline-session-count")
            .GetJsonAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);

        return Convert.ToInt32(DynamicExtensions.GetFirstPropertyValue(result));
    }

    public async Task<IEnumerable<UserSession>> GetClientOfflineSessionsAsync(string realm, string clientId,
        int? first = null, int? max = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(max)] = max
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/offline-sessions")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<UserSession>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<ClientScope>> GetOptionalClientScopesAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/optional-client-scopes")
            .GetJsonAsync<IEnumerable<ClientScope>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateOptionalClientScopeAsync(string realm, string clientId, string clientScopeId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/optional-client-scopes/{clientScopeId}")
            .PutAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteOptionalClientScopeAsync(string realm, string clientId, string clientScopeId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/optional-client-scopes/{clientScopeId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<GlobalRequestResult> PushClientRevocationPolicyAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/push-revocation")
            .PostAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<GlobalRequestResult>()
            .ConfigureAwait(false);
    }

    public async Task<Client> GenerateClientRegistrationAccessTokenAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/registration-access-token")
            .PostJsonAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<Client>()
            .ConfigureAwait(false);
    }

    [Obsolete("Not working yet")]
    public async Task<User> GetUserForServiceAccountAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/service-account-user")
            .GetJsonAsync<User>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<int> GetClientSessionCountAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        var result = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/session-count")
            .GetJsonAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);

        return Convert.ToInt32(DynamicExtensions.GetFirstPropertyValue(result));
    }

    public async Task<GlobalRequestResult> TestClientClusterNodesAvailableAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/test-nodes-available")
            .GetJsonAsync<GlobalRequestResult>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<UserSession>> GetClientUserSessionsAsync(string realm, string clientId,
        int? first = null, int? max = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(max)] = max
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/user-sessions")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<UserSession>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Resource>> GetResourcesOwnedByClientAsync(string realm, string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/realms/{realm}/protocol/openid-connect/token")
            .PostUrlEncodedAsync(new List<KeyValuePair<string, string>>
            {
                new("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket"),
                new("response_mode", "permissions"),
                new("audience", clientId)
            }, cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<IEnumerable<Resource>>()
            .ConfigureAwait(false);
    }

    private async Task<HttpResponseMessage> InternalCreateClientAsync(string realm, Client client,
        CancellationToken cancellationToken)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/clients")
            .PostJsonAsync(client, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }
}