using Flurl.Http;
using Flurl.Http.Configuration;

namespace Keycloak.Net.Core;

public interface IInternalKeycloakClient
{
    ISerializer Serializer { get; }
    
    void SetSerializer(ISerializer serializer);

    IFlurlRequest GetBaseUrl(string authenticationRealm);
}