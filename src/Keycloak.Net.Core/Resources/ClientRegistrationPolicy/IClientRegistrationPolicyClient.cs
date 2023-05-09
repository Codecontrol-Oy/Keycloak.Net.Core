using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Components;

namespace Keycloak.Net.Core.Resources.ClientRegistrationPolicy;

public interface IClientRegistrationPolicyClient
{
    Task<IEnumerable<ComponentType>> GetRetrieveProvidersBasePathAsync(string realm,
        CancellationToken cancellationToken = default);
}