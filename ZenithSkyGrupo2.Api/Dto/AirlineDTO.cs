using System.Text.Json.Serialization;

namespace ZenithSkyGrupo2.Api.Dto
{
    public class AirlineDTO
    {
        [JsonPropertyName("airline_id")]
        public int AirlineId { get; set; }

        [JsonPropertyName("iata")]
        public string Iata { get; set; }

        [JsonPropertyName("airlinename")]
        public string Airlinename { get; set; }

        [JsonPropertyName("base_airport")]
        public int BaseAirport { get; set; }

        [JsonPropertyName("base_airport_iata")]
        public string? BaseAirportIata { get; set; }

        [JsonPropertyName("base_airport_name")]
        public string? BaseAirportName { get; set; }

        public AirlineDTO()
        {
            Iata = "";
            Airlinename = "";
            BaseAirportIata = null;
            BaseAirportName = null;
        }
    }
}