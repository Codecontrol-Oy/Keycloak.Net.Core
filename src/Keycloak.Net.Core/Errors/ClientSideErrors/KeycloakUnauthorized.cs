using System;

namespace Keycloak.Net.Core.Errors.ClientSideErrors;

/// <summary>
/// 401 | Indicates that clients should provide authorization or the provided authorization is invalid
/// </summary>
public class KeycloakUnauthorized : BaseError
{
    public KeycloakUnauthorized(string message, string description) : base(message, description)
    {
    }

    public KeycloakUnauthorized(string message, Exception inner, string description) : base(message, inner, description)
    {
    }
}