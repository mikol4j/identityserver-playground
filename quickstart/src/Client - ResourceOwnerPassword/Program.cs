﻿using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client___ResourceOwnerPassword
{
    class Program
    {
        private static async Task Main()
        {
            {
                // discover endpoints from metadata
                var client = new HttpClient();

                var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    return;
                }
                // request token
                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "ro.client",
                    ClientSecret = "secret",

                    UserName = "alice",
                    Password = "password",
                    Scope = "api1"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);

                Console.ReadKey();
            }
        }
    }
}
