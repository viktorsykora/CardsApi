using System.Text.Json.Serialization;

namespace Application.Contracts.CardValidity
{
    public class CardValidityResponse
    {
        [JsonConverter(typeof(CustomDateConverter))]
        public DateTime ValidityEnd { get; set; }

        public string StateDescription { get; set; }
    }
}