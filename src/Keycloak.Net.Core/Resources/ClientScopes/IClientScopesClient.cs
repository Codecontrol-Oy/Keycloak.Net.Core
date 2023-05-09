using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.ClientScopes;

namespace Keycloak.Net.Core.Resources.ClientScopes;

public interface IClientScopesClient
{
    Task<bool> CreateClientScopeAsync(string realm, ClientScope clientScope,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ClientScope>> GetClientScopesAsync(string realm, CancellationToken cancellationToken = default);

    Task<ClientScope> GetClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateClientScopeAsync(string realm, string clientScopeId, ClientScope clientScope,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);
}