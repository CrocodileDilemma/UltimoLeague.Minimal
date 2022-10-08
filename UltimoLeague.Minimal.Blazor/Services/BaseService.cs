using ErrorOr;
using Newtonsoft.Json;
using System.Net.Http.Json;
using UltimoLeague.Minimal.Blazor.Interfaces;
using UltimoLeague.Minimal.Contracts.Requests;

namespace UltimoLeague.Minimal.Blazor.Services
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient _httpClient;
        public BaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public HttpClient HttpClient
        {
            get { return _httpClient; }
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

                return await this.HandleError<T>(response);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(ex.InnerException is null ? ex.Message : ex.InnerException.Message);
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
