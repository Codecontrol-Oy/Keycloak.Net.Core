﻿using Newtonsoft.Json;

namespace Keycloak.Net.Core.Models.Root
{
    public class ConnectionsJpaUpdaterProviders
    {
        [JsonProperty("liquibase")]
        public HasOrder Liquibase { get; set; }
    }
}