using System;

namespace Keycloak.Net.Core.Errors.ClientSideErrors;

/// <summary>
/// 404 | Indicates that the requested resource does not exist
/// </summary>
public class KeycloakNotFound : BaseError
{
    public KeycloakNotFound(string message, string description) : base(message, description)
    {
    }

    public KeycloakNotFound(string message, Exception inner, string description) : base(message, inner, description)
    {
    }
}