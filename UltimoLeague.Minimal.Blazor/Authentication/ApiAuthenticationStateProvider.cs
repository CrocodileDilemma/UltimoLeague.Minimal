using Blazored.LocalStorage;
using DnsClient;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;

namespace UltimoLeague.Minimal.Blazor.Authentication
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);
            return new AuthenticationState(this.CreatePrincipal(savedToken));
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var authenticatedUser = CreatePrincipal(token);
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }

        private ClaimsPrincipal CreatePrincipal(string token)
        {
            return new ClaimsPrincipal(new
                ClaimsIdentity(ParseClaimsFromJwt(token), "apiauth"));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            try
            {
                var payload = jwt.Split('.')[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                keyValuePairs.TryGetValue("role", out object roles);
                keyValuePairs.TryGetValue("email", out object email);
                keyValuePairs.TryGetValue("nameid", out object nameid);
                keyValuePairs.TryGetValue("unique_name", out object name);

                return new List<Claim>
                {
                    new Claim(ClaimTypes.Role, roles.ToString()),
                    new Claim(ClaimTypes.Email, email.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, nameid.ToString()),
                    new Claim(ClaimTypes.Name, name.ToString())
                };
            }
            catch (Exception)
            {
                return new List<Claim>();
            }
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
