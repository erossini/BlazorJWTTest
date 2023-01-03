using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Conjoint.Client.Services
{
    public class ApiTokenCacheService
    {
        private readonly HttpClient _httpClient;

        private LocalStorageService _cache;

        private string _clientName = "2200107UI";
        private IAccessTokenProvider _accessToken;

        public ApiTokenCacheService(
            IHttpClientFactory httpClientFactory,
            LocalStorageService cache,
            IAccessTokenProvider accessToken)
        {
            _accessToken = accessToken;
            _cache = cache;

            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<string> GetAccessToken()
        {
            AccessToken accessToken = await GetFromCacheAsync(_clientName);

            if (accessToken != null && accessToken.Expires > DateTime.UtcNow)
            {
                return accessToken.Value;
            }

            AccessToken localAccessToken = await RequestUserToken();
            await AddToCacheAsync(_clientName, localAccessToken);

            return localAccessToken.Value;
        }

        private async Task<AccessToken> GetFromCacheAsync(string key)
        {
            return await _cache.GetAsync<AccessToken>(key);
        }

        private async Task AddToCacheAsync(string key, AccessToken accessTokenItem)
        {
            await _cache.SetAsync(key, accessTokenItem);
        }

        private async Task<AccessToken> RequestUserToken()
        {
            try
            {
                var tokenResult = await _accessToken.RequestAccessToken();
                tokenResult.TryGetToken(out var token);

                Console.WriteLine(token);

                return token;
            }
            catch (AccessTokenNotAvailableException aex)
            {
                Console.WriteLine($"[RequestUserToken] Exception {aex}");
                throw new ApplicationException($"Exception {aex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RequestUserToken] AccessTokenNotAvailable Exception Exception {ex}");
                throw new ApplicationException($"AccessTokenNotAvailable Exception Exception {ex}");
            }
        }
    }
}