using System;

namespace Keycloak.Net.Core.Errors;

public class BaseError : Exception
{
    public string Description { get; set; }

    public BaseError(string message, string description)
        : base(message)
    {
        Description = description;
    }

    public BaseError(string message, Exception inner, string description)
        : base(message, inner)
    {
        Description = description;
    }
}