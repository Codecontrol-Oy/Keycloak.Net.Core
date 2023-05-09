using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.AuthorizationResources;
using Keycloak.Net.Core.Resources.AuthorizationResource;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IAuthorizationResourceClient
{
    public async Task<bool> CreateResourceAsync(string realm, string resourceServerId,
        AuthorizationResource resource, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource")
            .PostJsonAsync(resource, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<AuthorizationResource>> GetResourcesAsync(string realm,
        string resourceServerId = null,
        bool deep = false, int? first = null, int? max = null, string name = null, string owner = null,
        string type = null, string uri = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(deep)] = deep,
            [nameof(first)] = first,
            [nameof(max)] = max,
            [nameof(name)] = name,
            [nameof(owner)] = owner,
            [nameof(type)] = type,
            [nameof(uri)] = uri
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<AuthorizationResource>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<AuthorizationResource> GetResourceAsync(string realm,
        string resourceServerId, string resourceId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment(
                $"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource/{resourceId}")
            .GetJsonAsync<AuthorizationResource>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateResourceAsync(string realm, string resourceServerId, string resourceId,
        AuthorizationResource resource, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment(
                $"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource/{resourceId}")
            .PutJsonAsync(resource, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteResourceAsync(string realm, string resourceServerId, string resourceId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment(
                $"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource/{resourceId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }
}