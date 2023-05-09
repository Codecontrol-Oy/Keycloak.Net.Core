using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.AuthenticationManagement;

namespace Keycloak.Net.Core.Resources.AuthenticationManagement;

public interface IAuthenticationManagementClient
{
    Task<IEnumerable<IDictionary<string, object>>> GetAuthenticatorProvidersAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<IDictionary<string, object>>> GetClientAuthenticatorProvidersAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<AuthenticatorConfigInfo> GetAuthenticatorProviderConfigurationDescriptionAsync(string realm, string providerId,
        CancellationToken cancellationToken = default);

    Task<AuthenticatorConfig> GetAuthenticatorConfigurationAsync(string realm, string configurationId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAuthenticatorConfigurationAsync(string realm, string configurationId,
        AuthenticatorConfig authenticatorConfig, CancellationToken cancellationToken = default);

    Task<bool> DeleteAuthenticatorConfigurationAsync(string realm, string configurationId,
        CancellationToken cancellationToken = default);

    Task<bool> AddAuthenticationExecutionAsync(string realm, AuthenticationExecution authenticationExecution,
        CancellationToken cancellationToken = default);

    Task<AuthenticationExecutionById> GetAuthenticationExecutionAsync(string realm, string executionId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAuthenticationExecutionAsync(string realm, string executionId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAuthenticationExecutionConfigurationAsync(string realm, string executionId,
        AuthenticatorConfig authenticatorConfig, CancellationToken cancellationToken = default);

    Task<bool> LowerAuthenticationExecutionPriorityAsync(string realm, string executionId,
        CancellationToken cancellationToken = default);

    Task<bool> RaiseAuthenticationExecutionPriorityAsync(string realm, string executionId,
        CancellationToken cancellationToken = default);

    Task<bool> CreateAuthenticationFlowAsync(string realm, AuthenticationFlow authenticationFlow,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<AuthenticationFlow>> GetAuthenticationFlowsAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<bool> DuplicateAuthenticationFlowAsync(string realm, string flowAlias, string newName,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<AuthenticationFlowExecution>> GetAuthenticationFlowExecutionsAsync(string realm, string flowAlias,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAuthenticationFlowExecutionsAsync(string realm, string flowAlias,
        AuthenticationExecutionInfo authenticationExecutionInfo, CancellationToken cancellationToken = default);

    Task<bool> AddAuthenticationFlowExecutionAsync(string realm, string flowAlias,
        IDictionary<string, object> dataWithProvider, CancellationToken cancellationToken = default);

    Task<bool> AddAuthenticationFlowAndExecutionToAuthenticationFlowAsync(string realm, string flowAlias,
        IDictionary<string, object> dataWithAliasTypeProviderDescription,
        CancellationToken cancellationToken = default);

    Task<AuthenticationFlow> GetAuthenticationFlowByIdAsync(string realm, string flowId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAuthenticationFlowAsync(string realm, string flowId, AuthenticationFlow authenticationFlow,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAuthenticationFlowAsync(string realm, string flowId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<IDictionary<string, object>>> GetFormActionProvidersAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<IDictionary<string, object>>> GetFormProvidersAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<IDictionary<string, object>> GetConfigurationDescriptionsForAllClientsAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<bool> RegisterRequiredActionAsync(string realm, IDictionary<string, object> dataWithProviderIdName,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<RequiredActionProvider>> GetRequiredActionsAsync(string realm,
        CancellationToken cancellationToken = default);

    Task<RequiredActionProvider> GetRequiredActionByAliasAsync(string realm, string requiredActionAlias,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateRequiredActionAsync(string realm, string requiredActionAlias,
        RequiredActionProvider requiredActionProvider, CancellationToken cancellationToken = default);

    Task<bool> DeleteRequiredActionAsync(string realm, string requiredActionAlias,
        CancellationToken cancellationToken = default);

    Task<bool> LowerRequiredActionPriorityAsync(string realm, string requiredActionAlias,
        CancellationToken cancellationToken = default);

    Task<bool> RaiseRequiredActionPriorityAsync(string realm, string requiredActionAlias,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<IDictionary<string, object>>> GetUnregisteredRequiredActionsAsync(string realm,
        CancellationToken cancellationToken = default);
}