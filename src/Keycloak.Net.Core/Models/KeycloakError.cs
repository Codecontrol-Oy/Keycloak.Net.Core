using Newtonsoft.Json;

namespace Keycloak.Net.Core.Models;

public class KeycloakError
{
    [JsonProperty("error")]
    public string Error { get; set; }
    [JsonProperty("error_description")]
    public string ErrorDescription { get; set; }
    [JsonProperty("errorMessage")]
    public string ErrorMessage { get; set; }
}