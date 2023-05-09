using System;

namespace Keycloak.Net.Core.Errors.ClientSideErrors;

/// <summary>
/// 415 | Indicates that the requested media type is not supported
/// </summary>
public class KeycloakUnsupportedMediaType : BaseError
{
    public KeycloakUnsupportedMediaType(string message, string description) : base(message, description)
    {
    }

    public KeycloakUnsupportedMediaType(string message, Exception inner, string description) : base(message, inner, description)
    {
    }
}