using System;

namespace Keycloak.Net.Core.Errors.ClientSideErrors;

/// <summary>
/// 405 | Indicates that the method chosen by the client to access a resource is not supported
/// </summary>
public class KeycloakMethodNotAllowed : BaseError
{
    public KeycloakMethodNotAllowed(string message, string description) : base(message, description)
    {
    }

    public KeycloakMethodNotAllowed(string message, Exception inner, string description) : base(message, inner, description)
    {
    }
}