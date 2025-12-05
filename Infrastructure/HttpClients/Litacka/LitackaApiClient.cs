using System.Net.Http.Json;
using Application.Abstractions;

namespace Infrastructure.HttpClients.Litacka
{
    internal class LitackaApiClient : ICardProvider
    {
        private readonly HttpClient _httpClient;

        public LitackaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }

        public async Task<string> GetCardStatusDescriptionAsync(string cardName)
        {
            var response = await GetWithErrorHandlingAsync<LitackaCardStateResponse>($"cards/{cardName}/state");
            return response.StateDescription;
        }

        public async Task<DateTime> GetCardValidityAsync(string cardName)
        {
            var response = await GetWithErrorHandlingAsync<LitackaCardValidityResponse>($"cards/{cardName}/validity");
            return response.ValidityEnd;
        }

        private async Task<T> GetWithErrorHandlingAsync<T>(string uri)
        {
            using var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed - status: {response.StatusCode}. {body}", null, response.StatusCode);
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}