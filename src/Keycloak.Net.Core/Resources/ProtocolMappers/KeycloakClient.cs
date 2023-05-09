using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.ProtocolMappers;
using Keycloak.Net.Core.Resources.ProtocolMappers;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IProtocolMappersClient
{
    public async Task<bool> CreateMultipleProtocolMappersAsync(string realm, string clientScopeId,
        IEnumerable<ProtocolMapper> protocolMapperRepresentations, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/add-models")
            .PostJsonAsync(protocolMapperRepresentations, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateProtocolMapperAsync(string realm, string clientScopeId,
        ProtocolMapper protocolMapperRepresentation, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models")
            .PostJsonAsync(protocolMapperRepresentation, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<ProtocolMapper>> GetProtocolMappersAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models")
            .GetJsonAsync<IEnumerable<ProtocolMapper>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ProtocolMapper> GetProtocolMapperAsync(string realm, string clientScopeId,
        string protocolMapperId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment(
                $"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models/{protocolMapperId}")
            .GetJsonAsync<ProtocolMapper>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateProtocolMapperAsync(string realm, string clientScopeId, string protocolMapperId,
        ProtocolMapper protocolMapperRepresentation, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment(
                $"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models/{protocolMapperId}")
            .PutJsonAsync(protocolMapperRepresentation, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProtocolMapperAsync(string realm, string clientScopeId, string protocolMapperId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment(
                $"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models/{protocolMapperId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<ProtocolMapper>> GetProtocolMappersByNameAsync(string realm, string clientScopeId,
        string protocol, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment(
                $"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/protocol/{protocol}")
            .GetJsonAsync<IEnumerable<ProtocolMapper>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }
}