using System.Text.Json.Serialization;

namespace Application.Contracts
{
    public class CardValidityResponse
    {
        [JsonConverter(typeof(CustomDateConverter))]
        public DateTime ValidityEnd { get; set; }

        public string StateDescription { get; set; }
    }
}