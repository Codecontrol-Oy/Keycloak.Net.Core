using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Roles;

namespace Keycloak.Net.Core.Resources.RoleMapper;

public interface IRoleMapperClient
{
    Task<Mapping> GetRoleMappingsForGroupAsync(string realm, string groupId,
        CancellationToken cancellationToken = default);

    Task<bool> AddRealmRoleMappingsToGroupAsync(string realm, string groupId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRealmRoleMappingsForGroupAsync(string realm, string groupId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteRealmRoleMappingsFromGroupAsync(string realm, string groupId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAvailableRealmRoleMappingsForGroupAsync(string realm, string groupId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetEffectiveRealmRoleMappingsForGroupAsync(string realm, string groupId,
        CancellationToken cancellationToken = default);

    Task<Mapping> GetRoleMappingsForUserAsync(string realm, string userId,
        CancellationToken cancellationToken = default);

    Task<bool> AddRealmRoleMappingsToUserAsync(string realm, string userId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetRealmRoleMappingsForUserAsync(string realm, string userId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteRealmRoleMappingsFromUserAsync(string realm, string userId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAvailableRealmRoleMappingsForUserAsync(string realm, string userId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetEffectiveRealmRoleMappingsForUserAsync(string realm, string userId,
        CancellationToken cancellationToken = default);
}