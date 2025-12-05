using System.Text.Json.Serialization;

namespace Infrastructure.HttpClients.Litacka
{
    internal class LitackaCardValidityResponse
    {
        [JsonPropertyName("validity_start")]
        public DateTime ValidityStart { get; set; }

        [JsonPropertyName("validity_end")]
        public DateTime ValidityEnd { get; set; }
    }
}