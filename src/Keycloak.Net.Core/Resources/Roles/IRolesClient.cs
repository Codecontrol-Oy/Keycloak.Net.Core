using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Groups;
using Keycloak.Net.Core.Models.Roles;
using Keycloak.Net.Core.Models.Users;

namespace Keycloak.Net.Core.Resources.Roles;

public interface IRolesClient
{
    Task<bool> CreateRoleAsync(string realm, string clientId, Role role, CancellationToken cancellationToken = default);
    Task<bool> CreateRoleAsync(string realm, Role role, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRolesAsync(string realm, string clientId, int? first = null, int? max = null,
        string search = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRolesAsync(string realm, int? first = null, int? max = null, string search = null,
        CancellationToken cancellationToken = default);

    Task<Role> GetRoleByNameAsync(string realm, string clientId, string roleName,
        CancellationToken cancellationToken = default);

    Task<Role> GetRoleByNameAsync(string realm, string roleName, CancellationToken cancellationToken = default);

    Task<bool> UpdateRoleByNameAsync(string realm, string clientId, string roleName, Role role,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateRoleByNameAsync(string realm, string roleName, Role role,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteRoleByNameAsync(string realm, string clientId, string roleName,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteRoleByNameAsync(string realm, string roleName, CancellationToken cancellationToken = default);

    Task<bool> AddCompositesToRoleAsync(string realm, string clientId, string roleName, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<bool> AddCompositesToRoleAsync(string realm, string roleName, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRoleCompositesAsync(string realm, string clientId, string roleName,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRoleCompositesAsync(string realm, string roleName,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveCompositesFromRoleAsync(string realm, string clientId, string roleName, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveCompositesFromRoleAsync(string realm, string roleName, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetApplicationRolesForCompositeAsync(string realm, string clientId, string roleName,
        string forClientId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetApplicationRolesForCompositeAsync(string realm, string roleName, string forClientId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRealmRolesForCompositeAsync(string realm, string clientId, string roleName,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRealmRolesForCompositeAsync(string realm, string roleName,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Group>> GetGroupsWithRoleNameAsync(string realm, string clientId, string roleName,
        int? first = null, bool? full = null, int? max = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<Group>> GetGroupsWithRoleNameAsync(string realm, string roleName, int? first = null,
        bool? full = null, int? max = null, CancellationToken cancellationToken = default);

    Task<ManagementPermission> GetRoleAuthorizationPermissionsInitializedAsync(string realm, string clientId,
        string roleName, CancellationToken cancellationToken = default);

    Task<ManagementPermission> GetRoleAuthorizationPermissionsInitializedAsync(string realm, string roleName,
        CancellationToken cancellationToken = default);

    Task<ManagementPermission> SetRoleAuthorizationPermissionsInitializedAsync(string realm, string clientId,
        string roleName, ManagementPermission managementPermission, CancellationToken cancellationToken = default);

    Task<ManagementPermission> SetRoleAuthorizationPermissionsInitializedAsync(string realm, string roleName,
        ManagementPermission managementPermission, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetUsersWithRoleNameAsync(string realm, string clientId, string roleName, int? first = null,
        int? max = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetUsersWithRoleNameAsync(string realm, string roleName, int? first = null, int? max = null,
        CancellationToken cancellationToken = default);
}