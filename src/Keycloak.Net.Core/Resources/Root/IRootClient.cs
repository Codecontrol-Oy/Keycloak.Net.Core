using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Root;

namespace Keycloak.Net.Core.Resources.Root;

public interface IRootClient
{
    Task<ServerInfo> GetServerInfoAsync(string realm, CancellationToken cancellationToken = default);
    Task<bool> CorsPreflightAsync(string realm, CancellationToken cancellationToken = default);
}