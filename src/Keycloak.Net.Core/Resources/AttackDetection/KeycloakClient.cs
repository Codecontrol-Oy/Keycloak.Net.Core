using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.AttackDetection;
using Keycloak.Net.Core.Resources.AttackDetection;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IAttackDetectionClient
{
    public async Task<bool> ClearUserLoginFailuresAsync(string realm, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/attack-detection/brute-force/users")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ClearUserLoginFailuresAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/attack-detection/brute-force/users/{userId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<UserNameStatus> GetUserNameStatusInBruteForceDetectionAsync(string realm, string userId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/attack-detection/brute-force/users/{userId}")
            .GetJsonAsync<UserNameStatus>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }
}