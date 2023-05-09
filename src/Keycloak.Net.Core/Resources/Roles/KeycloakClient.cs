﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Groups;
using Keycloak.Net.Core.Models.Roles;
using Keycloak.Net.Core.Models.Users;
using Keycloak.Net.Core.Resources.Roles;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IRolesClient
{
    public async Task<bool> CreateRoleAsync(string realm, string clientId, Role role,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles")
            .PostJsonAsync(role, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetRolesAsync(string realm, string clientId, int? first = null,
        int? max = null, string search = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(max)] = max,
            [nameof(search)] = search
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<Role> GetRoleByNameAsync(string realm, string clientId, string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}")
            .GetJsonAsync<Role>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateRoleByNameAsync(string realm, string clientId, string roleName, Role role,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}")
            .PutJsonAsync(role, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteRoleByNameAsync(string realm, string clientId, string roleName,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AddCompositesToRoleAsync(string realm, string clientId, string roleName,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}/composites")
            .PostJsonAsync(roles, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetRoleCompositesAsync(string realm, string clientId, string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}/composites")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> RemoveCompositesFromRoleAsync(string realm, string clientId, string roleName,
        IEnumerable<Role> roles, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}/composites")
            .SendJsonAsync(HttpMethod.Delete, roles, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetApplicationRolesForCompositeAsync(string realm, string clientId,
        string roleName, string forClientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment(
                $"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}/composites/clients/{forClientId}")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Role>> GetRealmRolesForCompositeAsync(string realm, string clientId, string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}/composites/realm")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    [Obsolete("Not working yet")]
    public async Task<IEnumerable<Group>> GetGroupsWithRoleNameAsync(string realm, string clientId, string roleName,
        int? first = null, bool? full = null, int? max = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(full)] = full,
            [nameof(max)] = max
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}/groups")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<Group>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ManagementPermission> GetRoleAuthorizationPermissionsInitializedAsync(string realm,
        string clientId, string roleName, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}/management/permissions")
            .GetJsonAsync<ManagementPermission>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ManagementPermission> SetRoleAuthorizationPermissionsInitializedAsync(string realm,
        string clientId, string roleName, ManagementPermission managementPermission,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}/management/permissions")
            .PutJsonAsync(managementPermission, cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<ManagementPermission>()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<User>> GetUsersWithRoleNameAsync(string realm, string clientId, string roleName,
        int? first = null, int? max = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(max)] = max
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/roles/{roleName}/users")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<User>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> CreateRoleAsync(string realm, Role role, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles")
            .PostJsonAsync(role, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetRolesAsync(string realm, int? first = null, int? max = null,
        string search = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(max)] = max,
            [nameof(search)] = search
        };

        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<Role> GetRoleByNameAsync(string realm, string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}")
            .GetJsonAsync<Role>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateRoleByNameAsync(string realm, string roleName, Role role,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}")
            .PutJsonAsync(role, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteRoleByNameAsync(string realm, string roleName,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AddCompositesToRoleAsync(string realm, string roleName, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}/composites")
            .PostJsonAsync(roles, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetRoleCompositesAsync(string realm, string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}/composites")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> RemoveCompositesFromRoleAsync(string realm, string roleName, IEnumerable<Role> roles,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}/composites")
            .SendJsonAsync(HttpMethod.Delete, roles, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Role>> GetApplicationRolesForCompositeAsync(string realm, string roleName,
        string forClientId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}/composites/clients/{forClientId}")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Role>> GetRealmRolesForCompositeAsync(string realm, string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}/composites/realm")
            .GetJsonAsync<IEnumerable<Role>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    [Obsolete("Not working yet")]
    public async Task<IEnumerable<Group>> GetGroupsWithRoleNameAsync(string realm, string roleName, int? first = null,
        bool? full = null, int? max = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(full)] = full,
            [nameof(max)] = max
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}/groups")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<Group>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ManagementPermission> GetRoleAuthorizationPermissionsInitializedAsync(string realm,
        string roleName, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}/management/permissions")
            .GetJsonAsync<ManagementPermission>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ManagementPermission> SetRoleAuthorizationPermissionsInitializedAsync(string realm,
        string roleName, ManagementPermission managementPermission, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}/management/permissions")
            .PutJsonAsync(managementPermission, cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<ManagementPermission>()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<User>> GetUsersWithRoleNameAsync(string realm, string roleName, int? first = null,
        int? max = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(max)] = max
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/roles/{roleName}/users")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<User>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }
}