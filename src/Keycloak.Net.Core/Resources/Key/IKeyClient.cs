using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Key;

namespace Keycloak.Net.Core.Resources.Key;

public interface IKeyClient
{
    Task<KeysMetadata> GetKeysAsync(string realm, CancellationToken cancellationToken = default);
}