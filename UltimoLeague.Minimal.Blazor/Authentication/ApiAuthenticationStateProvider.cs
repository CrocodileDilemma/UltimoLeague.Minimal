using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace UltimoLeague.Minimal.Blazor.Authentication;

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

        var authenticatedUser = CreatePrincipal(savedToken);
        if (TokenExpired(authenticatedUser))
        {
            await MarkUserAsLoggedOut();
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);
        return new AuthenticationState(this.CreatePrincipal(savedToken));
    }

    private bool TokenExpired(ClaimsPrincipal authenticatedUser)
    {
        var expiry = authenticatedUser.FindFirst(ClaimTypes.Expiration);
        var ticks = long.Parse(expiry.Value);
        var tokenDate = DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;
        return tokenDate < System.DateTime.Now.ToUniversalTime();
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var authenticatedUser = CreatePrincipal(token);
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task MarkUserAsLoggedOut()
    {
        var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        _httpClient.DefaultRequestHeaders.Authorization = null;
        await _localStorage.RemoveItemAsync("authToken");
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
            keyValuePairs.TryGetValue("exp", out object expiry);

            return new List<Claim>
            {
                new Claim(ClaimTypes.Role, roles.ToString()),
                new Claim(ClaimTypes.Email, email.ToString()),
                new Claim(ClaimTypes.NameIdentifier, nameid.ToString()),
                new Claim(ClaimTypes.Name, name.ToString()),
                new Claim(ClaimTypes.Expiration, expiry.ToString())
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
