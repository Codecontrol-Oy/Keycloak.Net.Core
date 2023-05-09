using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.OpenIDConfiguration;
using Keycloak.Net.Core.Resources.OpenIDConfiguration;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IOpenIDConfigurationClient
{
    public async Task<OpenIDConfiguration> GetOpenIDConfigurationAsync(string realm,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/realms/{realm}/.well-known/openid-configuration")
            .GetJsonAsync<OpenIDConfiguration>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }
}