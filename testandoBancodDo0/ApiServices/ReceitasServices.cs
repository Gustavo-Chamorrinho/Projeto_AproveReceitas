using Newtonsoft.Json;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.ApiServices
{
    public class ReceitasServices
    {
        private readonly HttpClient _httpClient;

        public ReceitasServices(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiReceitasClient");
        }
        public async Task<List<ReceitasModel>?> GetReceitasAsync()
        {
            var response = await _httpClient.GetAsync("/api/Receitas");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ReceitasModel>>(content);
        }
        public async Task<ReceitasModel?> GetReceitasByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Receitas/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReceitasModel>(content);
        }
    }
}
