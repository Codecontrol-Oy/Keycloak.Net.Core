using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.ProtocolMappers;

namespace Keycloak.Net.Core.Resources.ProtocolMappers;

public interface IProtocolMappersClient
{
    Task<bool> CreateMultipleProtocolMappersAsync(string realm, string clientScopeId,
        IEnumerable<ProtocolMapper> protocolMapperRepresentations, CancellationToken cancellationToken = default);

    Task<bool> CreateProtocolMapperAsync(string realm, string clientScopeId,
        ProtocolMapper protocolMapperRepresentation, CancellationToken cancellationToken = default);

    Task<IEnumerable<ProtocolMapper>> GetProtocolMappersAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<ProtocolMapper> GetProtocolMapperAsync(string realm, string clientScopeId, string protocolMapperId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateProtocolMapperAsync(string realm, string clientScopeId, string protocolMapperId,
        ProtocolMapper protocolMapperRepresentation, CancellationToken cancellationToken = default);

    Task<bool> DeleteProtocolMapperAsync(string realm, string clientScopeId, string protocolMapperId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ProtocolMapper>> GetProtocolMappersByNameAsync(string realm, string clientScopeId, string protocol,
        CancellationToken cancellationToken = default);
}