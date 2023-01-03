using Conjoint.Client.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Text.Json;

namespace Conjoint.Client.Services
{
    public class ApiService : IApiService
    {
        private readonly ApiTokenCacheService _apiTokenCacheService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;
        private readonly JsonSerializerOptions _options;

        public ApiService(ApiTokenCacheService apiTokenCacheService,
            HttpClient httpClient, IAccessTokenProvider accessToken,
            ApplicationSettingsModel settings, ILogger<ApiService> logger)
        {
            _apiTokenCacheService = apiTokenCacheService;

            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            _httpClient.DefaultRequestHeaders.Add("Cello-Apim-Subscription-Key", settings.SubscriptionKey);

            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            _logger = logger;
        }

        /// <summary>
        /// Gets the sensitivity asynchronous.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns></returns>
        public async Task<SensitivityResponse> GetSensitivityAsync(string country = "100")
        {
            try
            {
                _logger.LogInformation("[GetSensitivityAsync] Starting the call");

                var token = await _apiTokenCacheService.GetAccessToken();

                if (token == null)
                    _logger.LogError("[GetSensitivityAsync] Token is null");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"sensitivity?filterCountry={country}");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                _logger.LogInformation("[GetSensitivityAsync] Request completed. Sending request...");

                HttpResponseMessage responseMessage;
                responseMessage = await _httpClient.SendAsync(request);
                responseMessage.EnsureSuccessStatusCode();

                _logger.LogInformation($"[GetSensitivityAsync] Request completed: {responseMessage.StatusCode}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<SensitivityResponse>(responseContent, _options);
                }
                else
                    return new SensitivityResponse() { Success = false };
            }
            catch (AccessTokenNotAvailableException aex)
            {
                _logger.LogError($"[GetSensitivityAsync] AccessTokenNotAvailableException: {aex.Message}");

                return new SensitivityResponse() { Success = false };
            }
            catch (Exception ex)
            {
                _logger.LogError($"[GetSensitivityAsync] Error: {ex.Message}");
                return new SensitivityResponse() { Success = false };
            }
        }
    }
}