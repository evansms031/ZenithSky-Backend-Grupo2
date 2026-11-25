using System.Text.Json.Serialization;

namespace ZenithSkyGrupo2.Api.Dto
{
    public class AirplaneDTO
    {
        [JsonPropertyName("airplane_id")]
        public int AirplaneId { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }

        [JsonPropertyName("airline_id")]
        public int AirlineId { get; set; }

        [JsonPropertyName("identifier")]
        public string? Identifier { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("airline_name")]
        public string? AirlineName { get; set; }

        public AirplaneDTO()
        {
            Identifier = null;
            Description = null;
            AirlineName = null;
        }
    }
}