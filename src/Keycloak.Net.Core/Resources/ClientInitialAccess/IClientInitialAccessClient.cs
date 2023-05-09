using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.ClientInitialAccess;

namespace Keycloak.Net.Core.Resources.ClientInitialAccess;

public interface IClientInitialAccessClient
{
    Task<ClientInitialAccessPresentation> CreateInitialAccessTokenAsync(string realm,
        ClientInitialAccessCreatePresentation create, CancellationToken cancellationToken = default);

    Task<IEnumerable<ClientInitialAccessPresentation>> GetClientInitialAccessAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteInitialAccessTokenAsync(string realm, string clientInitialAccessTokenId,
        CancellationToken cancellationToken = default);
}