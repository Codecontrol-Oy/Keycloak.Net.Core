using System;

namespace Keycloak.Net.Core.Errors.ClientSideErrors;

/// <summary>
/// 409 | Indicates that the resource the client is trying to create already exists or some conflict when processing the request
/// </summary>
public class KeycloakConflict : BaseError
{
    public KeycloakConflict(string message, string description) : base(message, description)
    {
    }

    public KeycloakConflict(string message, Exception inner, string description) : base(message, inner, description)
    {
    }
}