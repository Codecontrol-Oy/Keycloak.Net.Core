﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Common.Extensions;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Groups;
using Keycloak.Net.Core.Models.Users;
using Keycloak.Net.Core.Resources.Groups;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IGroupsClient
{
    public async Task<bool> CreateGroupAsync(string realm, Group group, CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups")
            .PostJsonAsync(group, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Group>> GetGroupHierarchyAsync(string realm, int? first = null, int? max = null,
        string search = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(max)] = max,
            [nameof(search)] = search,
            ["briefRepresentation"] = false
        };

        return await _internalKeycloakClient.GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/groups")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<Group>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<int> GetGroupsCountAsync(string realm, string search = null, bool? top = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(search)] = search,
            [nameof(top)] = top
        };

        var result = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/count")
            .SetQueryParams(queryParams)
            .GetJsonAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);

        return Convert.ToInt32(DynamicExtensions.GetFirstPropertyValue(result));
    }

    public async Task<Group> GetGroupAsync(string realm, string groupId, CancellationToken cancellationToken = default)
    {
        var result = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}")
            .GetJsonAsync<Group>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);

        return result;
    }

    public async Task<bool> UpdateGroupAsync(string realm, string groupId, Group group,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}")
            .PutJsonAsync(group, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteGroupAsync(string realm, string groupId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}")
            .DeleteAsync(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetOrCreateGroupChildAsync(string realm, string groupId, Group group,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/children")
            .PostJsonAsync(group, cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    public async Task<ManagementPermission> GetGroupClientAuthorizationPermissionsInitializedAsync(string realm,
        string groupId, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/management/permissions")
            .GetJsonAsync<ManagementPermission>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }

    public async Task<ManagementPermission> SetGroupClientAuthorizationPermissionsInitializedAsync(string realm,
        string groupId, ManagementPermission managementPermission, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/management/permissions")
            .PutJsonAsync(managementPermission, cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<ManagementPermission>()
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<User>> GetGroupUsersAsync(string realm, string groupId, int? first = null,
        int? max = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(first)] = first,
            [nameof(max)] = max
        };

        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/members")
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<User>>(cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
    }
}