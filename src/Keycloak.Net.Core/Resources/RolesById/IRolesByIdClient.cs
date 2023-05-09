using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Roles;

namespace Keycloak.Net.Core.Resources.RolesById;

public interface IRolesByIdClient
{
    Task<Role> GetRoleByIdAsync(string realm, string roleId, CancellationToken cancellationToken = default);

    Task<bool> UpdateRoleByIdAsync(string realm, string roleId, Role role,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteRoleByIdAsync(string realm, string roleId, CancellationToken cancellationToken = default);

    Task<bool> MakeRoleCompositeAsync(string realm, string roleId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRoleChildrenAsync(string realm, string roleId,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveRolesFromCompositeAsync(string realm, string roleId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetClientRolesForCompositeByIdAsync(string realm, string roleId, string clientId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRealmRolesForCompositeByIdAsync(string realm, string roleId,
        CancellationToken cancellationToken = default);

    Task<ManagementPermission> GetRoleByIdAuthorizationPermissionsInitializedAsync(string realm, string roleId,
        CancellationToken cancellationToken = default);

    Task<ManagementPermission> SetRoleByIdAuthorizationPermissionsInitializedAsync(string realm, string roleId,
        ManagementPermission managementPermission, CancellationToken cancellationToken = default);
}