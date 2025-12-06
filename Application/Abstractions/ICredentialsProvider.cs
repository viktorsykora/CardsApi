namespace Application.Abstractions
{
    public interface ICredentialsProvider
    {
        bool TryGetCredentials(string apiKey, out string username);
    }
}
