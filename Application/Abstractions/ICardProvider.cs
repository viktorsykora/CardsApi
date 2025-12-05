namespace Application.Abstractions
{
    public interface ICardProvider
    {
        Task<string> GetCardStatusDescriptionAsync(string cardName);
        Task<DateTime> GetCardValidityAsync(string cardName);
    }   
}