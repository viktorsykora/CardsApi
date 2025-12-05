using System.Text.Json.Serialization;

namespace Infrastructure.HttpClients.Litacka
{
    internal class LitackaCardStateResponse
    {
        [JsonPropertyName("state_id")]
        public CardState State { get; set; }

        [JsonPropertyName("state_description")]
        public string StateDescription { get; set; }
    }

    internal enum CardState
    {
        Active = 100
    }
}
