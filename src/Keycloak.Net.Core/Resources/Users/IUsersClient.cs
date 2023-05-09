using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Groups;
using Keycloak.Net.Core.Models.Users;

namespace Keycloak.Net.Core.Resources.Users;

public interface IUsersClient
{
    Task<bool> CreateUserAsync(string realm, User user, CancellationToken cancellationToken = default);
    Task<string> CreateAndRetrieveUserIdAsync(string realm, User user, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetUsersAsync(string realm, bool? briefRepresentation = null, string email = null,
        bool? exact = null, int? first = null,
        string firstName = null, string lastName = null, int? max = null, string search = null, string username = null,
        CancellationToken cancellationToken = default);

    Task<int> GetUsersCountAsync(string realm, CancellationToken cancellationToken = default);
    Task<User> GetUserAsync(string realm, string userId, CancellationToken cancellationToken = default);
    Task<bool> UpdateUserAsync(string realm, string userId, User user, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserAsync(string realm, string userId, CancellationToken cancellationToken = default);
    Task<string> GetUserConsentsAsync(string realm, string userId, CancellationToken cancellationToken = default);

    Task<bool> RevokeUserConsentAndOfflineTokensAsync(string realm, string userId, string clientId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Credentials>> GetUserCredentialsAsync(string realm, string userId,
        CancellationToken cancellationToken = default);

    Task<bool> DisableUserCredentialsAsync(string realm, string userId, IEnumerable<string> credentialTypes,
        CancellationToken cancellationToken = default);

    Task<bool> SendUserUpdateAccountEmailAsync(string realm, string userId, IEnumerable<string> requiredActions,
        string clientId = null, int? lifespan = null, string redirectUri = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<FederatedIdentity>> GetUserSocialLoginsAsync(string realm, string userId,
        CancellationToken cancellationToken = default);

    Task<bool> AddUserSocialLoginProviderAsync(string realm, string userId, string provider,
        FederatedIdentity federatedIdentity, CancellationToken cancellationToken = default);

    Task<bool> RemoveUserSocialLoginProviderAsync(string realm, string userId, string provider,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Group>> GetUserGroupsAsync(string realm, string userId,
        CancellationToken cancellationToken = default);

    Task<int> GetUserGroupsCountAsync(string realm, string userId, CancellationToken cancellationToken = default);

    Task<bool> UpdateUserGroupAsync(string realm, string userId, string groupId, Group group,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteUserGroupAsync(string realm, string userId, string groupId,
        CancellationToken cancellationToken = default);

    Task<IDictionary<string, object>> ImpersonateUserAsync(string realm, string userId,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveUserSessionsAsync(string realm, string userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<UserSession>> GetUserOfflineSessionsAsync(string realm, string userId, string clientId,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveUserTotpAsync(string realm, string userId, CancellationToken cancellationToken = default);

    Task<bool> ResetUserPasswordAsync(string realm, string userId, Credentials credentials,
        CancellationToken cancellationToken = default);

    Task<bool> ResetUserPasswordAsync(string realm, string userId, string password, bool temporary = true,
        CancellationToken cancellationToken = default);

    Task<SetPasswordResponse> SetUserPasswordAsync(string realm, string userId, string password,
        CancellationToken cancellationToken = default);

    Task<bool> VerifyUserEmailAddressAsync(string realm, string userId, string clientId = null,
        string redirectUri = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<UserSession>> GetUserSessionsAsync(string realm, string userId,
        CancellationToken cancellationToken = default);
}