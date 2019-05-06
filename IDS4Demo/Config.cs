// Copyright (c) Jeffcky <see cref="https://jeffcky.ke.qq.com/"/> All rights reserved.
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IDS4Demo
{
    public class Config
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
              
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ac35545d-e187-4485-b1f6-1784b6f78093",
                    ClientName="身份认证与授权服务",
                    ClientSecrets = {new Secret("YoUJc9doyutt3lIMLDSwjdMii1WnffNgOUCv".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowAccessTokensViaBrowser = false,
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = {"http://localhost:5000"}
                },
                new Client
                {
                    ClientId = "f40a7fab-8a6f-4952-b1c4-66c9d3cdd012",
                    ClientName = "WebAPI接口",
                    ClientUri = "http://localhost:5001/",
                    RequireConsent = false,
                    RequireClientSecret = true,
                    ClientSecrets = { new Secret("dN1equTBzBhpg0sSrFvJ5U5yV9GQJbTUAe9t".Sha256()) },

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowedCorsOrigins = {"http://localhost:5001"},

                    RedirectUris = {"http://localhost:5001/signin-oidc" },
                    PostLogoutRedirectUris = {"http://localhost:5001/" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "dc6ce328-3693-46ba-b3c1-55b258e097a5",
                    ClientName = "客户端",
                    ClientUri = "http://localhost:5002/",

                    RequireConsent = false,
                    RequireClientSecret = true,
                    ClientSecrets = { new Secret("dZf1nxJQvb7M7IoNKDTkhTxFJTZF4J5mHE9z".Sha256()) },

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = false,
                    AllowedCorsOrigins = {"http://localhost:5002"},

                    RedirectUris = {"http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc" },


                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}
