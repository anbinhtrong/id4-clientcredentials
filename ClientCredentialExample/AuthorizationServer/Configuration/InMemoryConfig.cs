using IdentityServer4.Models;

namespace AuthorizationServer.Configuration
{
    public class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("ApiOne.full", "Api One"),
                new ApiScope("ApiOne.read", "Api One"),
                new ApiScope("ApiOne.write", "Api One"),
                new ApiScope("ApiTwo", "Api Two")
            };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("ApiResource1", "My API")
                {
                    Scopes = new[] { "ApiOne.full", "ApiOne.read", "ApiOne.write" }
                },
                new ApiResource("ApiResource2", "My API 2")
                {
                    Scopes = new[] { "ApiTwo" }
                }
            };


        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "GoodReads",
                ClientSecrets = new [] {new Secret("ClientSecrets".Sha512())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "ApiOne.full", "ApiOne.read", "ApiOne.write" }
            },
            new Client
            {
                ClientId = "AMD",
                ClientSecrets = new [] {new Secret("ClientSecrets".Sha512())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "ApiTwo" }
            }
        };
    }
}
