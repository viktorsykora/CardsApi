using Application.Abstractions;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication
{
    internal class ConfigStoreCredentialsProvider : ICredentialsProvider
    {
        private readonly IOptions<ApiKeySettings> _options;

        public ConfigStoreCredentialsProvider(IOptions<ApiKeySettings> options)
        {
            _options = options;
        }        

        public bool TryGetCredentials(string apiKey, out string username)
        {
            return _options.Value.ApiKeys.TryGetValue(apiKey, out username);
        }
    }
}