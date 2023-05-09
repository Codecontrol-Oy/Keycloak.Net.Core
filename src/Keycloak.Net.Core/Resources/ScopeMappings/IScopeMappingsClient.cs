using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Roles;

namespace Keycloak.Net.Core;

public interface IScopeMappingsClient
{
    Task<Mapping> GetScopeMappingsAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<bool> AddClientRolesToClientScopeAsync(string realm, string clientScopeId, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetClientRolesForClientScopeAsync(string realm, string clientScopeId,
        string clientId, CancellationToken cancellationToken = default);

    Task<bool> RemoveClientRolesFromClientScopeAsync(string realm, string clientScopeId, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAvailableClientRolesForClientScopeAsync(string realm, string clientScopeId,
        string clientId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetEffectiveClientRolesForClientScopeAsync(string realm, string clientScopeId,
        string clientId, CancellationToken cancellationToken = default);

    Task<bool> AddRealmRolesToClientScopeAsync(string realm, string clientScopeId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRealmRolesForClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveRealmRolesFromClientScopeAsync(string realm, string clientScopeId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAvailableRealmRolesForClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetEffectiveRealmRolesForClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<Mapping> GetScopeMappingsForClientAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<bool> AddClientRolesScopeMappingToClientAsync(string realm, string clientId, string scopeClientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetClientRolesScopeMappingsForClientAsync(string realm, string clientId,
        string scopeClientId, CancellationToken cancellationToken = default);

    Task<bool> RemoveClientRolesFromClientScopeForClientAsync(string realm, string clientId,
        string scopeClientId, IEnumerable<Role> roles, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAvailableClientRolesForClientScopeForClientAsync(string realm,
        string clientId, string scopeClientId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetEffectiveClientRolesForClientScopeForClientAsync(string realm,
        string clientId, string scopeClientId, CancellationToken cancellationToken = default);

    Task<bool> AddRealmRolesScopeMappingToClientAsync(string realm, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRealmRolesScopeMappingsForClientAsync(string realm, string clientId,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveRealmRolesFromClientScopeForClientAsync(string realm, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAvailableRealmRolesForClientScopeForClientAsync(string realm,
        string clientId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetEffectiveRealmRolesForClientScopeForClientAsync(string realm,
        string clientId, CancellationToken cancellationToken = default);
}