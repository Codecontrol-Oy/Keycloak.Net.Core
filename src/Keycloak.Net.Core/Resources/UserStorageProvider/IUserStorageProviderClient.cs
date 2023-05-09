using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.UserStorageProvider;

namespace Keycloak.Net.Core.Resources.UserStorageProvider;

public interface IUserStorageProviderClient
{
    Task<bool> RemoveImportedUsersAsync(string realm, string storageProviderId,
        CancellationToken cancellationToken = default);

    Task<SynchronizationResult> TriggerUserSynchronizationAsync(string realm, string storageProviderId,
        UserSyncActions action, CancellationToken cancellationToken = default);

    Task<bool> UnlinkImportedUsersAsync(string realm, string storageProviderId,
        CancellationToken cancellationToken = default);

    Task<SynchronizationResult> TriggerLdapMapperSynchronizationAsync(string realm, string storageProviderId,
        string mapperId, LdapMapperSyncActions direction, CancellationToken cancellationToken = default);
}