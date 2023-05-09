using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Keycloak.Net.Core.Resources.AuthorizationResource;

public interface IAuthorizationResourceClient
{
    Task<bool> CreateResourceAsync(string realm, string resourceServerId,
        Models.AuthorizationResources.AuthorizationResource resource, CancellationToken cancellationToken = default);

    Task<IEnumerable<Models.AuthorizationResources.AuthorizationResource>> GetResourcesAsync(string realm,
        string resourceServerId = null,
        bool deep = false, int? first = null, int? max = null, string name = null, string owner = null,
        string type = null, string uri = null, CancellationToken cancellationToken = default);

    Task<Models.AuthorizationResources.AuthorizationResource> GetResourceAsync(string realm, string resourceServerId,
        string resourceId, CancellationToken cancellationToken = default);

    Task<bool> UpdateResourceAsync(string realm, string resourceServerId, string resourceId,
        Models.AuthorizationResources.AuthorizationResource resource, CancellationToken cancellationToken = default);

    Task<bool> DeleteResourceAsync(string realm, string resourceServerId, string resourceId,
        CancellationToken cancellationToken = default);
}