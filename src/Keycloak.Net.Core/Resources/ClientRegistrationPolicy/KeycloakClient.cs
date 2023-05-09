using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Components;
using Keycloak.Net.Core.Resources.ClientRegistrationPolicy;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IClientRegistrationPolicyClient
{
    public async Task<IEnumerable<ComponentType>>
        GetRetrieveProvidersBasePathAsync(string realm, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/client-registration-policy/providers")
            .GetJsonAsync<IEnumerable<ComponentType>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }
}