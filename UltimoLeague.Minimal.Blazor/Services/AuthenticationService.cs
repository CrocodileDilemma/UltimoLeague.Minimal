﻿using Blazored.LocalStorage;
using ErrorOr;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using UltimoLeague.Minimal.Blazor.Authentication;
using UltimoLeague.Minimal.Blazor.Interfaces;
using UltimoLeague.Minimal.Contracts.Dtos;
using UltimoLeague.Minimal.Contracts.Requests;

namespace UltimoLeague.Minimal.Blazor.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage) : base(httpClient, authenticationStateProvider)
        {
            _localStorage = localStorage;
        }

        public async Task<ErrorOr<SessionDto>> Login(SessionRequest request)
        {
            ErrorOr<SessionDto> result = await base.Post<SessionDto>("users/login", request);

            if (!result.IsError)
            {
                await _localStorage.SetItemAsync("authToken", result.Value.Token);
                ((ApiAuthenticationStateProvider)AuthStateProvider).MarkUserAsAuthenticated(result.Value.Token);
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Value.Token);
            }

            return result;
        }

        public async Task Logout()
        {
            ((ApiAuthenticationStateProvider)AuthStateProvider).MarkUserAsLoggedOut();          
        }
    }
}
