namespace Keycloak.Net.Core;

public partial class KeycloakClient : IKeycloakClient
{
    private readonly IInternalKeycloakClient _internalKeycloakClient;

    public KeycloakClient(IInternalKeycloakClient internalKeycloakClient)
    {
        _internalKeycloakClient = internalKeycloakClient;
    }
}