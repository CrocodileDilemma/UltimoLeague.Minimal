using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using UltimoLeague.Minimal.Blazor.Authentication;
using UltimoLeague.Minimal.Contracts.Dtos;
using UltimoLeague.Minimal.Contracts.Requests;
using UltimoLeague.Minimal.Blazor.Interfaces;
using ErrorOr;
using System;

namespace UltimoLeague.Minimal.Blazor.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<ErrorOr<MessageDto>> Register(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("users/register", request);
            try
            {
                string content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<MessageDto>(content);
                }

                var error = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(error))
                {
                    return Error.Validation();
                }

                return Error.Validation(description: error);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(ex.InnerException is null ? ex.Message : ex.InnerException.Message);
            }
        }

        public async Task<ErrorOr<SessionDto>> Login(SessionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("users/login", request);
            
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<SessionDto>();
                    await _localStorage.SetItemAsync("authToken", result.Token);
                    ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.EmailAddress);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                    return result;
                }

                var error = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(error))
                {
                    return Error.Validation();
                }

                return Error.Validation(description:error);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(ex.InnerException is null ? ex.Message : ex.InnerException.Message);
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
