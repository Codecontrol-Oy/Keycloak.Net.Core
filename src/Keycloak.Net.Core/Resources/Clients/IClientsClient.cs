using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Clients;
using Keycloak.Net.Core.Models.ClientScopes;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Roles;
using Keycloak.Net.Core.Models.Users;

namespace Keycloak.Net.Core.Resources.Clients;

public interface IClientsClient
{
    Task<bool> CreateClientAsync(string realm, Client client, CancellationToken cancellationToken = default);

    Task<string> CreateClientAndRetrieveClientIdAsync(string realm, Client client,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Client>> GetClientsAsync(string realm, string clientId = null, bool? viewableOnly = null,
        CancellationToken cancellationToken = default);

    Task<Client> GetClientAsync(string realm, string clientId, CancellationToken cancellationToken = default);

    Task<bool> UpdateClientAsync(string realm, string clientId, Client client,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteClientAsync(string realm, string clientId, CancellationToken cancellationToken = default);

    Task<Credentials> GenerateClientSecretAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<Credentials> GetClientSecretAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ClientScope>> GetDefaultClientScopesAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateDefaultClientScopeAsync(string realm, string clientId, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteDefaultClientScopeAsync(string realm, string clientId, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<AccessToken> GenerateClientExampleAccessTokenAsync(string realm, string clientId, string scope = null,
        string userId = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<ClientScopeEvaluateResourceProtocolMapperEvaluation>> GetProtocolMappersInTokenGenerationAsync(
        string realm, string clientId, string scope = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetClientGrantedScopeMappingsAsync(string realm, string clientId, string roleContainerId,
        string scope = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetClientNotGrantedScopeMappingsAsync(string realm, string clientId, string roleContainerId,
        string scope = null, CancellationToken cancellationToken = default);

    Task<string> GetClientProviderAsync(string realm, string clientId, string providerId,
        CancellationToken cancellationToken = default);

    Task<ManagementPermission> GetClientAuthorizationPermissionsInitializedAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<ManagementPermission> SetClientAuthorizationPermissionsInitializedAsync(string realm, string clientId,
        ManagementPermission managementPermission, CancellationToken cancellationToken = default);

    Task<bool> RegisterClientClusterNodeAsync(string realm, string clientId, IDictionary<string, object> formParams,
        CancellationToken cancellationToken = default);

    Task<bool> UnregisterClientClusterNodeAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<int> GetClientOfflineSessionCountAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<UserSession>> GetClientOfflineSessionsAsync(string realm, string clientId, int? first = null,
        int? max = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<ClientScope>> GetOptionalClientScopesAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateOptionalClientScopeAsync(string realm, string clientId, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteOptionalClientScopeAsync(string realm, string clientId, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<GlobalRequestResult> PushClientRevocationPolicyAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<Client> GenerateClientRegistrationAccessTokenAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<User> GetUserForServiceAccountAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<int> GetClientSessionCountAsync(string realm, string clientId, CancellationToken cancellationToken = default);

    Task<GlobalRequestResult> TestClientClusterNodesAvailableAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<UserSession>> GetClientUserSessionsAsync(string realm, string clientId, int? first = null,
        int? max = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<Resource>> GetResourcesOwnedByClientAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);
}