using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Roles;

namespace Keycloak.Net.Core.Resources.ClientRoleMappings;

public interface IClientRoleMappingsClient
{
    Task<bool> AddClientRoleMappingsToGroupAsync(string realm, string groupId, string clientId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetClientRoleMappingsForGroupAsync(string realm, string groupId, string clientId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteClientRoleMappingsFromGroupAsync(string realm, string groupId, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAvailableClientRoleMappingsForGroupAsync(string realm, string groupId, string clientId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetEffectiveClientRoleMappingsForGroupAsync(string realm, string groupId, string clientId,
        CancellationToken cancellationToken = default);

    Task<bool> AddClientRoleMappingsToUserAsync(string realm, string userId, string clientId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetClientRoleMappingsForUserAsync(string realm, string userId, string clientId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteClientRoleMappingsFromUserAsync(string realm, string userId, string clientId,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAvailableClientRoleMappingsForUserAsync(string realm, string userId, string clientId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetEffectiveClientRoleMappingsForUserAsync(string realm, string userId, string clientId,
        CancellationToken cancellationToken = default);
}