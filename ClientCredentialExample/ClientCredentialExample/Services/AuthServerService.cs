using IdentityModel.Client;

namespace ClientCredentialExample.Services
{
    public class AuthServerService : IAuthServerService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscoveryCache _discoveryCache;
        public AuthServerService(HttpClient httpClient, IDiscoveryCache discoveryCache)
        {
            _httpClient = httpClient;
            _discoveryCache = discoveryCache;
        }
        public async Task<string> RequestClientCredentialsTokenAsync()
        {
            var endPointDiscovery = await _discoveryCache.GetAsync();
            if (endPointDiscovery.IsError)
            {
                //todo sth
            }

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = endPointDiscovery.TokenEndpoint,
                ClientId = "GoodReads",
                ClientSecret = "ClientSecrets",
                Scope = "ApiOne.full ApiOne.read ApiOne.write"
            });

            if (tokenResponse.IsError)
            {
                //todo sth
            }

            return tokenResponse.AccessToken;
        }
    }
}
