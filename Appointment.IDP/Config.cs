// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4.Models;

namespace Appointment.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("country", new[] {"country"})
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("appointment.api",
                    "Appointment API to access data",
                    new[] {"country"})// just for testing purposes
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "appointment.web",
                    ClientName = "Web application to manage appointments",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = {new Secret("108B7B4F-BEFC-4DD2-82E1-7F025F0F75D0".Sha256())},
                    RedirectUris = {"https://localhost:5001/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:5001"},
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                    RequirePkce = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                    AllowedScopes = {"openid", "profile", "email", "appointment.api", "country"}
                }
            };
    }
}