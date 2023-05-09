using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Common.Extensions;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Groups;
using Keycloak.Net.Core.Models.Users;
using Keycloak.Net.Core.Resources.Users;
using Newtonsoft.Json;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IUsersClient
{
    public async Task<bool> CreateUserAsync(string realm, User user, CancellationToken cancellationToken = default)
    {
        var response = await InternalCreateUserAsync(realm, user, cancellationToken)
            .HandleErrorsAsync().ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<string> CreateAndRetrieveUserIdAsync(string realm, User user,
        CancellationToken cancellationToken = default)
    {
        var response = await InternalCreateUserAsync(realm, user, cancellationToken)
            .HandleErrorsAsync().ConfigureAwait(false);
        var locationPathAndQuery = response.Headers.Location.PathAndQuery;
        var userId = response.IsSuccessStatusCode
            ? locationPathAndQuery.Substring(locationPathAndQuery.LastIndexOf("/", StringComparison.Ordinal) + 1)
            : null;
        return userId;
    }

    public async Task<IEnumerable<User>> GetUsersAsync(string realm, bool? briefRepresentation = null,
        string email = null, bool? exact = null, int? first = null,
        string firstName = null, string lastName = null, int? max = null, string search = null, string username = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(briefRepresentation)] = briefRepresentation,
            [nameof(email)] = email,
            [nameof(exact)] = exact,
            [nameof(first)] = first,
            [nameof(firstName)] = firstName,
            [nameof(lastName)] = lastName,
            [nameof(max)] = max,
            [nameof(search)] = search,
            [nameof(username)] = username
        };

        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/users")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<User>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<int> GetUsersCountAsync(string realm, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/users/count")
            .GetJsonAsync<int>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<User> GetUserAsync(string realm, string userId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}")
            .GetJsonAsync<User>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateUserAsync(string realm, string userId, User user,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}")
            .PutJsonAsync(user, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUserAsync(string realm, string userId, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    [Obsolete("Not working yet")]
    public async Task<string> GetUserConsentsAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/consents")
            .GetStringAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> RevokeUserConsentAndOfflineTokensAsync(string realm, string userId, string clientId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/consents/{clientId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Credentials>> GetUserCredentialsAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/credentials")
            .GetJsonAsync<IEnumerable<Credentials>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> DisableUserCredentialsAsync(string realm, string userId,
        IEnumerable<string> credentialTypes, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/disable-credential-types")
            .PutJsonAsync(credentialTypes, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendUserUpdateAccountEmailAsync(string realm, string userId,
        IEnumerable<string> requiredActions, string clientId = null, int? lifespan = null, string redirectUri = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            ["client_id"] = clientId,
            [nameof(lifespan)] = lifespan,
            ["redirect_uri"] = redirectUri
        };

        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/execute-actions-email")
            .SetQueryParams(queryParams)
            .PutJsonAsync(requiredActions, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<FederatedIdentity>> GetUserSocialLoginsAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/federated-identity")
            .GetJsonAsync<IEnumerable<FederatedIdentity>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> AddUserSocialLoginProviderAsync(string realm, string userId, string provider,
        FederatedIdentity federatedIdentity, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/federated-identity/{provider}")
            .PostJsonAsync(federatedIdentity, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveUserSocialLoginProviderAsync(string realm, string userId, string provider,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/federated-identity/{provider}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Group>> GetUserGroupsAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/groups")
            .GetJsonAsync<IEnumerable<Group>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<int> GetUserGroupsCountAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/groups/count")
            .GetJsonAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return Convert.ToInt32(DynamicExtensions.GetFirstPropertyValue(result));
    }

    public async Task<bool> UpdateUserGroupAsync(string realm, string userId, string groupId, Group group,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/groups/{groupId}")
            .PutJsonAsync(group, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUserGroupAsync(string realm, string userId, string groupId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/groups/{groupId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IDictionary<string, object>> ImpersonateUserAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/impersonation")
            .PostAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<IDictionary<string, object>>()
            .ConfigureAwait(false);
    }

    public async Task<bool> RemoveUserSessionsAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/logout")
            .PostAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    [Obsolete("Not working yet")]
    public async Task<IEnumerable<UserSession>> GetUserOfflineSessionsAsync(string realm, string userId,
        string clientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/users/{userId}/offline-sessions/{clientId}")
            .GetJsonAsync<IEnumerable<UserSession>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> RemoveUserTotpAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/remove-totp")
            .PutAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ResetUserPasswordAsync(string realm, string userId, Credentials credentials,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/reset-password")
            .PutJsonAsync(credentials, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ResetUserPasswordAsync(string realm, string userId, string password, bool temporary = true,
        CancellationToken cancellationToken = default)
    {
        var response =
            await InternalResetUserPasswordAsync(realm, userId, password, temporary, cancellationToken)
                .HandleErrorsAsync()
                .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<SetPasswordResponse> SetUserPasswordAsync(string realm, string userId, string password,
        CancellationToken cancellationToken = default)
    {
        var response = await InternalResetUserPasswordAsync(realm, userId, password, false, cancellationToken)
            .HandleErrorsAsync();
        if (response.IsSuccessStatusCode)
            return new SetPasswordResponse { Success = response.IsSuccessStatusCode };

        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<SetPasswordResponse>(jsonString);
    }

    public async Task<bool> VerifyUserEmailAddressAsync(string realm, string userId, string clientId = null,
        string redirectUri = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>();
        if (!string.IsNullOrEmpty(clientId)) queryParams.Add("client_id", clientId);

        if (!string.IsNullOrEmpty(redirectUri)) queryParams.Add("redirect_uri", redirectUri);

        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/send-verify-email")
            .SetQueryParams(queryParams)
            .PutJsonAsync(null, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<UserSession>> GetUserSessionsAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/sessions")
            .GetJsonAsync<IEnumerable<UserSession>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    private async Task<HttpResponseMessage> InternalCreateUserAsync(string realm, User user,
        CancellationToken cancellationToken)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users")
            .PostJsonAsync(user, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    private async Task<HttpResponseMessage> InternalResetUserPasswordAsync(string realm, string userId, string password,
        bool temporary, CancellationToken cancellationToken)
    {
        var credentials = new Credentials
        {
            Value = password,
            Temporary = temporary
        };
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/users/{userId}/reset-password")
            .PutJsonAsync(credentials, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response;
    }
}