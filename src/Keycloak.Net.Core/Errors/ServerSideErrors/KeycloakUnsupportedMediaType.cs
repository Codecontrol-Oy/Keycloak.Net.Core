using System;

namespace Keycloak.Net.Core.Errors.ServerSideErrors;

/// <summary>
/// 500 | Indicates that the server could not fulfill the request due to some unexpected error
/// </summary>
public class KeycloakInternalServerError : BaseError
{
    public KeycloakInternalServerError(string message, string description) : base(message, description)
    {
    }

    public KeycloakInternalServerError(string message, Exception inner, string description) : base(message, inner, description)
    {
    }
}