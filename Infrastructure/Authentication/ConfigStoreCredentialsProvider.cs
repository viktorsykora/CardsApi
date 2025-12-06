using Application.Abstractions;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication
{
    internal class ConfigStoreCredentialsProvider : ICredentialsProvider
    {
        private readonly IOptionsMonitor<ApiKeySettings> _options;

        public ConfigStoreCredentialsProvider(IOptionsMonitor<ApiKeySettings> options)
        {
            _options = options;
        }        

        public bool TryGetCredentials(string apiKey, out string username)
        {
            return _options.CurrentValue.ApiKeys.TryGetValue(apiKey, out username);
        }
    }
}