using Microsoft.JSInterop;
using System.Text.Json;

namespace Conjoint.Client.Services
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime js)
        {
            _jsRuntime = js;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            string jsVal = null;
            if (value != null)
                jsVal = JsonSerializer.Serialize(value);

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem",
                new object[] { key, jsVal });
        }

        public async Task<T> GetAsync<T>(string key)
        {
            string val = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (val == null)
                return default;

            T result = JsonSerializer.Deserialize<T>(val);
            return result;
        }

        public async Task RemoveAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}
