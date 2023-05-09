using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.Common;
using Keycloak.Net.Core.Models.IdentityProviders;

namespace Keycloak.Net.Core.Resources.IdentityProviders;

public interface IIdentityProvidersClient
{
    Task<IDictionary<string, object>> ImportIdentityProviderAsync(string realm, string input,
        CancellationToken cancellationToken = default);

    Task<bool> CreateIdentityProviderAsync(string realm, IdentityProvider identityProvider,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<IdentityProvider>> GetIdentityProviderInstancesAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<IdentityProvider> GetIdentityProviderAsync(string realm, string identityProviderAlias,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     <see
    ///         cref="https://github.com/keycloak/keycloak-documentation/blob/master/server_development/topics/identity-brokering/tokens.adoc" />
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="identityProviderAlias"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IdentityProviderToken> GetIdentityProviderTokenAsync(string realm, string identityProviderAlias,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateIdentityProviderAsync(string realm, string identityProviderAlias,
        IdentityProvider identityProvider, CancellationToken cancellationToken = default);

    Task<bool> DeleteIdentityProviderAsync(string realm, string identityProviderAlias,
        CancellationToken cancellationToken = default);

    Task<bool> ExportIdentityProviderPublicBrokerConfigurationAsync(string realm, string identityProviderAlias,
        CancellationToken cancellationToken = default);

    Task<ManagementPermission> GetIdentityProviderAuthorizationPermissionsInitializedAsync(string realm,
        string identityProviderAlias, CancellationToken cancellationToken = default);

    Task<ManagementPermission> SetIdentityProviderAuthorizationPermissionsInitializedAsync(string realm,
        string identityProviderAlias, ManagementPermission managementPermission,
        CancellationToken cancellationToken = default);

    Task<IDictionary<string, object>> GetIdentityProviderMapperTypesAsync(string realm, string identityProviderAlias,
        CancellationToken cancellationToken = default);

    Task<bool> AddIdentityProviderMapperAsync(string realm, string identityProviderAlias,
        IdentityProviderMapper identityProviderMapper, CancellationToken cancellationToken = default);

    Task<IEnumerable<IdentityProviderMapper>> GetIdentityProviderMappersAsync(string realm,
        string identityProviderAlias, CancellationToken cancellationToken = default);

    Task<IdentityProviderMapper> GetIdentityProviderMapperByIdAsync(string realm, string identityProviderAlias,
        string mapperId, CancellationToken cancellationToken = default);

    Task<bool> UpdateIdentityProviderMapperAsync(string realm, string identityProviderAlias, string mapperId,
        IdentityProviderMapper identityProviderMapper, CancellationToken cancellationToken = default);

    Task<bool> DeleteIdentityProviderMapperAsync(string realm, string identityProviderAlias, string mapperId,
        CancellationToken cancellationToken = default);

    Task<IdentityProviderInfo> GetIdentityProviderByProviderIdAsync(string realm, string providerId,
        CancellationToken cancellationToken = default);
}