using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GreekShoping.IdentityServer.Configuration;

public static class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    // Corrigindo o retorno dos IdentityResources
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

    // Corrigindo o retorno dos ApiScopes
    public static IEnumerable<ApiScope> ApiScopes =>
         new List<ApiScope>
         {
            new ApiScope("greek_Shoping", "GreekShoping Server"),
            new ApiScope("read", "Read data"),
            new ApiScope("write", "Write data"),
            new ApiScope("delete", "Delete data"),
         };

    // Corrigindo o Cliente 
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
             // Use uma strin complexa no lugar de my_super_secret para não comprometer a segurança do token 
            new Client
            {
                ClientId ="Client",
                ClientSecrets = {new Secret ("my_super_secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile"}
            },
            new Client
            {
                ClientId ="greek_Shoping",
                ClientSecrets = {new Secret ("my_super_secret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:4430/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:4430/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,
                    "greek_Shoping"
                }
            }
        };

}
