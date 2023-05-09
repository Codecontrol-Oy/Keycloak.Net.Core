using System;
using Keycloak.Net.Core.Resources.ClientRoleMappings;
using Keycloak.Net.Core.Resources.Clients;
using Keycloak.Net.Core.Resources.RoleMapper;
using Keycloak.Net.Core.Resources.Roles;
using Keycloak.Net.Core.Resources.Users;
using Keycloak.Net.Core.Resources.UserStorageProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Keycloak.Net.Core;

/// <summary>
/// Adds Keycloak Admin Client
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Keycloak Admin Client
    /// </summary>
    /// <param name="services"></param>
    /// <param name="url"></param>
    /// <param name="clientSecret"></param>
    /// <param name="keycloakOptions"></param>
    public static void AddKeycloakAdminClient(
        this IServiceCollection services,
        string url,
        string clientSecret,
        KeycloakOptions keycloakOptions)
    {
        // Base service
        services.TryAddSingleton<IInternalKeycloakClient>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<InternalKeycloakClient>>();
            return new InternalKeycloakClient(url, clientSecret, keycloakOptions, logger);
        });
        
        // Resource services
        services.AddTransient<IKeycloakClient, KeycloakClient>();
    }
    
    /// <summary>
    /// Adds Keycloak Admin Client
    /// </summary>
    /// <param name="services"></param>
    /// <param name="url"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="keycloakOptions"></param>
    public static void AddKeycloakAdminClient(
        this IServiceCollection services,
        string url,
        string userName,
        string password,
        KeycloakOptions keycloakOptions)
    {
        // Base service
        services.TryAddSingleton<IInternalKeycloakClient>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<InternalKeycloakClient>>();
            return new InternalKeycloakClient(url, userName, password, keycloakOptions, logger);
        });
        
        // Resource services
        services.AddTransient<IKeycloakClient, KeycloakClient>();
    }
    
    /// <summary>
    /// Adds Keycloak Admin Client
    /// </summary>
    /// <param name="services"></param>
    /// <param name="url"></param>
    /// <param name="getToken"></param>
    /// <param name="keycloakOptions"></param>
    public static void AddKeycloakAdminClient(
        this IServiceCollection services,
        string url,
        Func<string> getToken,
        KeycloakOptions keycloakOptions)
    {
        // Base service
        services.TryAddSingleton<IInternalKeycloakClient>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<InternalKeycloakClient>>();
            return new InternalKeycloakClient(url, getToken, keycloakOptions, logger);
        });
        
        // Resource services
        services.AddTransient<IKeycloakClient, KeycloakClient>();
    }
}