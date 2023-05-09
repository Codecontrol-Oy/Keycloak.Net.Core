using System;

namespace Keycloak.Net.Core.Errors.ClientSideErrors;

/// <summary>
/// 400 | Indicates that the request is invalid, usually related with the validation of the payload
/// </summary>
public class KeycloakBadRequest : BaseError
{
    public KeycloakBadRequest(string message, string description) : base(message, description)
    {
    }

    public KeycloakBadRequest(string message, Exception inner, string description) : base(message, inner, description)
    {
    }
}