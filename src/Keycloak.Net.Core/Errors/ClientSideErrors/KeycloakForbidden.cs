using System;

namespace Keycloak.Net.Core.Errors.ClientSideErrors;

/// <summary>
/// 403 | Indicates that the authorization provided by the client is not enough to access the resource
/// </summary>
public class KeycloakForbidden : BaseError
{
    public KeycloakForbidden(string message, string description) : base(message, description)
    {
    }

    public KeycloakForbidden(string message, Exception inner, string description) : base(message, inner, description)
    {
    }
}