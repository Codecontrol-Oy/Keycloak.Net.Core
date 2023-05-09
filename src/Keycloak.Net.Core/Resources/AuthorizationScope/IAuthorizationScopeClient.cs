using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Keycloak.Net.Core.Resources.AuthorizationScope;

public interface IAuthorizationScopeClient
{
    Task<bool> CreateAuthorizationScopeAsync(string realm, string resourceServerId,
        Models.AuthorizationScopes.AuthorizationScope scope, CancellationToken cancellationToken = default);

    Task<IEnumerable<Models.AuthorizationScopes.AuthorizationScope>> GetAuthorizationScopesAsync(string realm,
        string resourceServerId = null,
        bool deep = false, int? first = null, int? max = null, string name = null,
        CancellationToken cancellationToken = default);

    Task<Models.AuthorizationScopes.AuthorizationScope> GetAuthorizationScopeAsync(string realm,
        string resourceServerId, string scopeId, CancellationToken cancellationToken = default);

    Task<bool> UpdateAuthorizationScopeAsync(string realm, string resourceServerId, string scopeId,
        Models.AuthorizationScopes.AuthorizationScope scope, CancellationToken cancellationToken = default);

    Task<bool> DeleteAuthorizationScopeAsync(string realm, string resourceServerId, string scopeId,
        CancellationToken cancellationToken = default);
}