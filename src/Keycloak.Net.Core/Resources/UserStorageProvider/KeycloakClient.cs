﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Core.Errors;
using Keycloak.Net.Core.Models.UserStorageProvider;
using Keycloak.Net.Core.Resources.UserStorageProvider;

namespace Keycloak.Net.Core;

public partial class KeycloakClient : IUserStorageProviderClient
{
    [Obsolete("Not working yet")]
    public async Task<bool> RemoveImportedUsersAsync(string realm, string storageProviderId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/user-storage/{storageProviderId}/remove-imported-users")
            .PostAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    [Obsolete("Not working yet")]
    public async Task<SynchronizationResult> TriggerUserSynchronizationAsync(string realm, string storageProviderId,
        UserSyncActions action, CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient
            .GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/user-storage/{storageProviderId}/sync")
            .SetQueryParam(nameof(action),
                action == UserSyncActions.Full ? "triggerFullSync" : "triggerChangedUsersSync")
            .PostAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .ReceiveJson<SynchronizationResult>()
            .ConfigureAwait(false);
    }

    [Obsolete("Not working yet")]
    public async Task<bool> UnlinkImportedUsersAsync(string realm, string storageProviderId,
        CancellationToken cancellationToken = default)
    {
        var response = await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/user-storage/{storageProviderId}/unlink-users")
            .PostAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .HandleErrorsAsync()
            .ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    [Obsolete("Not working yet")]
    public async Task<SynchronizationResult> TriggerLdapMapperSynchronizationAsync(string realm,
        string storageProviderId, string mapperId, LdapMapperSyncActions direction,
        CancellationToken cancellationToken = default)
    {
        return await _internalKeycloakClient.GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/user-storage/{storageProviderId}/mappers/{mapperId}/sync")
            .SetQueryParam(nameof(direction),
                direction == LdapMapperSyncActions.FedToKeycloak ? "fedToKeycloak" : "keycloakToFed")
            .PostAsync(new StringContent(""), cancellationToken)
            .HandleErrorsAsync()
            .HandleErrorsAsync()
            .ReceiveJson<SynchronizationResult>()
            .ConfigureAwait(false);
    }
}