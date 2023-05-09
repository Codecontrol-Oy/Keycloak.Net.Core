using Keycloak.Net.Core.Resources.AttackDetection;
using Keycloak.Net.Core.Resources.AuthenticationManagement;
using Keycloak.Net.Core.Resources.AuthorizationResource;
using Keycloak.Net.Core.Resources.AuthorizationScope;
using Keycloak.Net.Core.Resources.ClientAttributeCertificate;
using Keycloak.Net.Core.Resources.ClientAuthorization;
using Keycloak.Net.Core.Resources.ClientInitialAccess;
using Keycloak.Net.Core.Resources.ClientRegistrationPolicy;
using Keycloak.Net.Core.Resources.ClientRoleMappings;
using Keycloak.Net.Core.Resources.Clients;
using Keycloak.Net.Core.Resources.ClientScopes;
using Keycloak.Net.Core.Resources.Components;
using Keycloak.Net.Core.Resources.Groups;
using Keycloak.Net.Core.Resources.IdentityProviders;
using Keycloak.Net.Core.Resources.Key;
using Keycloak.Net.Core.Resources.OpenIDConfiguration;
using Keycloak.Net.Core.Resources.ProtocolMappers;
using Keycloak.Net.Core.Resources.RealmsAdmin;
using Keycloak.Net.Core.Resources.RoleMapper;
using Keycloak.Net.Core.Resources.Roles;
using Keycloak.Net.Core.Resources.RolesById;
using Keycloak.Net.Core.Resources.Root;
using Keycloak.Net.Core.Resources.Users;
using Keycloak.Net.Core.Resources.UserStorageProvider;

namespace Keycloak.Net.Core;

public interface IKeycloakClient : 
    IAttackDetectionClient, 
    IAuthenticationManagementClient, 
    IAuthorizationResourceClient,
    IAuthorizationScopeClient, 
    IClientAttributeCertificateClient, 
    IClientAuthorizationClient, 
    IClientInitialAccessClient, 
    IClientRegistrationPolicyClient, 
    IClientRoleMappingsClient,
    IClientsClient, 
    IClientScopesClient,
    IComponentsClient,
    IGroupsClient, 
    IIdentityProvidersClient, 
    IKeyClient, 
    IOpenIDConfigurationClient, 
    IProtocolMappersClient,
    IRealmsAdminClient,
    IRoleMapperClient, 
    IRolesClient, 
    IRolesByIdClient, 
    IRootClient, 
    IScopeMappingsClient,
    IUsersClient, 
    IUserStorageProviderClient
{
}