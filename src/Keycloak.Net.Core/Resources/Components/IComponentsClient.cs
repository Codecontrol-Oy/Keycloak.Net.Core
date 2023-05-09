using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Components;

namespace Keycloak.Net.Core.Resources.Components;

public interface IComponentsClient
{
    Task<bool> CreateComponentAsync(string realm, Component componentRepresentation,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Component>> GetComponentsAsync(string realm, string name = null, string parent = null,
        string type = null, CancellationToken cancellationToken = default);

    Task<Component> GetComponentAsync(string realm, string componentId, CancellationToken cancellationToken = default);

    Task<bool> UpdateComponentAsync(string realm, string componentId, Component componentRepresentation,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteComponentAsync(string realm, string componentId, CancellationToken cancellationToken = default);

    Task<IEnumerable<ComponentType>> GetSubcomponentTypesAsync(string realm, string componentId, string type = null,
        CancellationToken cancellationToken = default);
}