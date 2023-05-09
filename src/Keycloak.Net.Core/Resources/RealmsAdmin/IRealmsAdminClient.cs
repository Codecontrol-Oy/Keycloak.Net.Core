using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Clients;
using Keycloak.Net.Core.Models.ClientScopes;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.Groups;
using Keycloak.Net.Core.Models.RealmsAdmin;

namespace Keycloak.Net.Core.Resources.RealmsAdmin;

public interface IRealmsAdminClient
{
    Task<bool> ImportRealmAsync(string realm, Realm rep, CancellationToken cancellationToken = default);
    Task<IEnumerable<Realm>> GetRealmsAsync(string realm, CancellationToken cancellationToken = default);
    Task<Realm> GetRealmAsync(string realm, CancellationToken cancellationToken = default);
    Task<bool> UpdateRealmAsync(string realm, Realm rep, CancellationToken cancellationToken = default);
    Task<bool> DeleteRealmAsync(string realm, CancellationToken cancellationToken = default);

    Task<IEnumerable<AdminEvent>> GetAdminEventsAsync(string realm, string authClient = null,
        string authIpAddress = null, string authRealm = null, string authUser = null,
        string dateFrom = null, string dateTo = null, int? first = null, int? max = null,
        IEnumerable<string> operationTypes = null, string resourcePath = null, IEnumerable<string> resourceTypes = null,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAdminEventsAsync(string realm, CancellationToken cancellationToken = default);
    Task<bool> ClearKeysCacheAsync(string realm, CancellationToken cancellationToken = default);
    Task<bool> ClearRealmCacheAsync(string realm, CancellationToken cancellationToken = default);
    Task<bool> ClearUserCacheAsync(string realm, CancellationToken cancellationToken = default);

    Task<Client> BasePathForImportingClientsAsync(string realm, string description,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<IDictionary<string, object>>> GetClientSessionStatsAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ClientScope>> GetRealmDefaultClientScopesAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateRealmDefaultClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteRealmDefaultClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Group>> GetRealmGroupHierarchyAsync(string realm, CancellationToken cancellationToken = default);
    Task<bool> UpdateRealmGroupAsync(string realm, string groupId, CancellationToken cancellationToken = default);
    Task<bool> DeleteRealmGroupAsync(string realm, string groupId, CancellationToken cancellationToken = default);

    Task<IEnumerable<ClientScope>> GetRealmOptionalClientScopesAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateRealmOptionalClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteRealmOptionalClientScopeAsync(string realm, string clientScopeId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Event>> GetEventsAsync(string realm, string client = null, string dateFrom = null,
        string dateTo = null, int? first = null,
        string ipAddress = null, int? max = null, string type = null, string user = null,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteEventsAsync(string realm, CancellationToken cancellationToken = default);

    Task<RealmEventsConfig> GetRealmEventsProviderConfigurationAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateRealmEventsProviderConfigurationAsync(string realm, RealmEventsConfig rep,
        CancellationToken cancellationToken = default);

    Task<Group> GetRealmGroupByPathAsync(string realm, string path, CancellationToken cancellationToken = default);
    Task<GlobalRequestResult> RemoveUserSessionsAsync(string realm, CancellationToken cancellationToken = default);

    Task<Realm> RealmPartialExportAsync(string realm, bool? exportClients = null, bool? exportGroupsAndRoles = null,
        CancellationToken cancellationToken = default);

    Task<bool> RealmPartialImportAsync(string realm, PartialImport rep, CancellationToken cancellationToken = default);

    Task<GlobalRequestResult> PushRealmRevocationPolicyAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteUserSessionAsync(string realm, string session, CancellationToken cancellationToken = default);

    Task<bool> TestLdapConnectionAsync(string realm, string action = null, string bindCredential = null,
        string bindDn = null,
        string componentId = null, string connectionTimeout = null, string connectionUrl = null,
        string useTruststoreSpi = null, CancellationToken cancellationToken = default);

    Task<bool> TestSmtpConnectionAsync(string realm, string config, CancellationToken cancellationToken = default);

    Task<ManagementPermission> GetRealmUsersManagementPermissionsAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<ManagementPermission> UpdateRealmUsersManagementPermissionsAsync(string realm,
        ManagementPermission managementPermission, CancellationToken cancellationToken = default);
}