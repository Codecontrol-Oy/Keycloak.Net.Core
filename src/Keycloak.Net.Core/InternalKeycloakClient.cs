using System;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Keycloak.Net.Core.Common.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Keycloak.Net.Core
{
    public class InternalKeycloakClient : IInternalKeycloakClient
    {
        private readonly Url _url;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _clientSecret;
        private readonly Func<string> _getToken;
        private readonly KeycloakOptions _options;
        private readonly ILogger<InternalKeycloakClient> _logger;

        private InternalKeycloakClient(string url, KeycloakOptions options, ILogger<InternalKeycloakClient> logger)
        {
            _url = url;
            _logger = logger;
            _options = options ?? new KeycloakOptions();
        }

        public InternalKeycloakClient(string url, string userName, string password, KeycloakOptions options = null, ILogger<InternalKeycloakClient> logger = null) 
            : this(url, options, logger)
        {
            _userName = userName;
            _password = password;
        }

        public InternalKeycloakClient(string url, string clientSecret, KeycloakOptions options = null, ILogger<InternalKeycloakClient> logger = null)
            : this(url, options, logger)
        {
            _clientSecret = clientSecret;
        }

        public InternalKeycloakClient(string url, Func<string> getToken, KeycloakOptions options = null, ILogger<InternalKeycloakClient> logger = null)
            : this(url, options, logger)
        {
            _getToken = getToken;
        }

        public ISerializer Serializer { get; private set; } = new NewtonsoftJsonSerializer(new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        });

        public void SetSerializer(ISerializer serializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public IFlurlRequest GetBaseUrl(string authenticationRealm) => new Url(_url)
            .AppendPathSegment(_options.Prefix)
            .ConfigureRequest(settings => settings.JsonSerializer = Serializer)
            .WithAuthentication(_getToken, _url, authenticationRealm, _userName, _password, _clientSecret, _options);
    }

    public class KeycloakOptions
    {
        public string Prefix { get; }
        public string AdminClientId { get; }

        public KeycloakOptions(string prefix = "", string adminClientId = "admin-cli")
        {
            Prefix = prefix.TrimStart('/').TrimEnd('/');
            AdminClientId = adminClientId;
        }
    }
}