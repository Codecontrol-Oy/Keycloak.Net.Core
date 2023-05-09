using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Groups;
using Keycloak.Net.Core.Models.Users;

namespace Keycloak.Net.Core.Resources.Groups;

public interface IGroupsClient
{
    Task<bool> CreateGroupAsync(string realm, Group group, CancellationToken cancellationToken = default);

    Task<IEnumerable<Group>> GetGroupHierarchyAsync(string realm, int? first = null, int? max = null,
        string search = null, CancellationToken cancellationToken = default);

    Task<int> GetGroupsCountAsync(string realm, string search = null, bool? top = null,
        CancellationToken cancellationToken = default);

    Task<Group> GetGroupAsync(string realm, string groupId, CancellationToken cancellationToken = default);

    Task<bool> UpdateGroupAsync(string realm, string groupId, Group group,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteGroupAsync(string realm, string groupId, CancellationToken cancellationToken = default);

    Task<bool> SetOrCreateGroupChildAsync(string realm, string groupId, Group group,
        CancellationToken cancellationToken = default);

    Task<ManagementPermission> GetGroupClientAuthorizationPermissionsInitializedAsync(string realm, string groupId,
        CancellationToken cancellationToken = default);

    Task<ManagementPermission> SetGroupClientAuthorizationPermissionsInitializedAsync(string realm, string groupId,
        ManagementPermission managementPermission, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetGroupUsersAsync(string realm, string groupId, int? first = null, int? max = null,
        CancellationToken cancellationToken = default);
}