using System.Threading;
using System.Threading.Tasks;

namespace Keycloak.Net.Core.Resources.OpenIDConfiguration;

public interface IOpenIDConfigurationClient
{
    Task<Models.OpenIDConfiguration.OpenIDConfiguration> GetOpenIDConfigurationAsync(string realm,
        CancellationToken cancellationToken = default);
}