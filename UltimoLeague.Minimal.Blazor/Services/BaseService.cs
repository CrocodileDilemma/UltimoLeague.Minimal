using ErrorOr;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using UltimoLeague.Minimal.Blazor.Authentication;
using UltimoLeague.Minimal.Blazor.Interfaces;

namespace UltimoLeague.Minimal.Blazor.Services
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public BaseService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;

        }
        public HttpClient HttpClient
        {
            get { return _httpClient; }
        }

        public AuthenticationStateProvider AuthStateProvider
        {
            get { return _authenticationStateProvider; }
        }
        public async Task<ErrorOr<T>> Post<T>(string uri, object request)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, request);
            return await this.HandleResponse<T>(response);
        }

        public async Task<ErrorOr<T>> Get<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            return await this.HandleResponse<T>(response);
        }

        private async Task<ErrorOr<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            try
            {
                string content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
                    return Error.Validation(description:"Please log in to perform this operation");
                }

                return await this.HandleError<T>(response);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(description: ex.InnerException is null ? ex.Message : ex.InnerException.Message);
            }
        }

        private async Task<ErrorOr<T>> HandleError<T>(HttpResponseMessage response)
        {
            var error = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(error))
            {
                return Error.Validation();
            }

            return Error.Validation(description:error);
        }
    }
}
