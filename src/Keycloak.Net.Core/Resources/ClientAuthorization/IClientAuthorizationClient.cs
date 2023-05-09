using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.AuthorizationPermissions;
using Keycloak.Net.Core.Models.Clients;

namespace Keycloak.Net.Core.Resources.ClientAuthorization;

public interface IClientAuthorizationClient
{
    Task<AuthorizationPermission> CreateAuthorizationPermissionAsync(string realm, string clientId,
        AuthorizationPermission permission, CancellationToken cancellationToken = default);

    Task<AuthorizationPermission> GetAuthorizationPermissionByIdAsync(string realm, string clientId,
        AuthorizationPermissionType permissionType, string permissionId, CancellationToken cancellationToken = default);

    Task<IEnumerable<AuthorizationPermission>> GetAuthorizationPermissionsAsync(string realm, string clientId,
        AuthorizationPermissionType? ofPermissionType = null,
        int? first = null, int? max = null, string name = null, string resource = null, string scope = null,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAuthorizationPermissionAsync(string realm, string clientId, AuthorizationPermission permission,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAuthorizationPermissionAsync(string realm, string clientId,
        AuthorizationPermissionType permissionType,
        string permissionId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Policy>> GetAuthorizationPermissionAssociatedPoliciesAsync(string realm, string clientId,
        string permissionId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Models.AuthorizationScopes.AuthorizationScope>> GetAuthorizationPermissionAssociatedScopesAsync(
        string realm, string clientId, string permissionId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Models.AuthorizationResources.AuthorizationResource>>
        GetAuthorizationPermissionAssociatedResourcesAsync(string realm, string clientId, string permissionId,
            CancellationToken cancellationToken = default);

    Task<RolePolicy> CreateRolePolicyAsync(string realm, string clientId, RolePolicy policy,
        CancellationToken cancellationToken = default);

    Task<RolePolicy> GetRolePolicyByIdAsync(string realm, string clientId, PolicyType policyType, string rolePolicyId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Policy>> GetAuthorizationPoliciesAsync(string realm, string clientId,
        int? first = null, int? max = null,
        string name = null, string resource = null,
        string scope = null, bool? permission = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<RolePolicy>> GetRolePoliciesAsync(string realm, string clientId,
        int? first = null, int? max = null,
        string name = null, string resource = null,
        string scope = null, bool? permission = null, CancellationToken cancellationToken = default);

    Task<bool> UpdateRolePolicyAsync(string realm, string clientId, RolePolicy policy,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteRolePolicyAsync(string realm, string clientId, PolicyType policyType, string rolePolicyId,
        CancellationToken cancellationToken = default);
}