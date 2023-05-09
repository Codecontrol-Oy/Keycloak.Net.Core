using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Root;
using Keycloak.Net.Core.Resources.Root;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IRootClient
{
    public async Task<ServerInfo> GetServerInfoAsync(string realm, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment("/admin/serverinfo/")
            .GetJsonAsync<ServerInfo>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> CorsPreflightAsync(string realm, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment("/admin/serverinfo/")
            .OptionsAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }
}