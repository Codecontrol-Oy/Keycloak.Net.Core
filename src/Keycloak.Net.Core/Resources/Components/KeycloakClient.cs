using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Components;
using Keycloak.Net.Core.Resources.Components;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IComponentsClient
{
    public async Task<bool> CreateComponentAsync(string realm, Component componentRepresentation,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/components")
            .PostJsonAsync(componentRepresentation, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Component>> GetComponentsAsync(string realm, string name = null, string parent = null,
        string type = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(name)] = name,
            [nameof(parent)] = parent,
            [nameof(type)] = type
        };

        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/components")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<Component>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<Component> GetComponentAsync(string realm, string componentId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/components/{componentId}")
            .GetJsonAsync<Component>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateComponentAsync(string realm, string componentId, Component componentRepresentation,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/components/{componentId}")
            .PutJsonAsync(componentRepresentation, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteComponentAsync(string realm, string componentId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/components/{componentId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<ComponentType>> GetSubcomponentTypesAsync(string realm, string componentId,
        string type = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(type)] = type
        };

        var result = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/components/{componentId}/sub-component-types")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<ComponentType>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return result;
    }
}