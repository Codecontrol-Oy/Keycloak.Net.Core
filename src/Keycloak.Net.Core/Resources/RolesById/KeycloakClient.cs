using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Content;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Roles;
using Keycloak.Net.Core.Resources.RolesById;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IRolesByIdClient
{
    public async Task<Role>
        GetRoleByIdAsync(string realm, string roleId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}")
            .GetJsonAsync<Role>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateRoleByIdAsync(string realm, string roleId, Role role,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}")
            .PutJsonAsync(role, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteRoleByIdAsync(string realm, string roleId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> MakeRoleCompositeAsync(string realm, string roleId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites")
            .PostJsonAsync(roles, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetRoleChildrenAsync(string realm, string roleId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> RemoveRolesFromCompositeAsync(string realm, string roleId, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites")
            .SendJsonAsync(HttpMethod.Delete,
                new CapturedJsonContent(_internalKeycloakClient.Serializer.Serialize(roles)), cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetClientRolesForCompositeByIdAsync(string realm, string roleId,
        string clientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites/clients/{clientId}")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Role>> GetRealmRolesForCompositeByIdAsync(string realm, string roleId,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites/realm")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ManagementPermission> GetRoleByIdAuthorizationPermissionsInitializedAsync(string realm,
        string roleId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/management/permissions")
            .GetJsonAsync<ManagementPermission>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ManagementPermission> SetRoleByIdAuthorizationPermissionsInitializedAsync(string realm,
        string roleId, ManagementPermission managementPermission, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/management/permissions")
            .PutJsonAsync(managementPermission, cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<ManagementPermission>()
            .ConfigureAwait(false);
    }
}