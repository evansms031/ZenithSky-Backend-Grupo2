using System.Text.Json.Serialization;

namespace ZenithSkyGrupo2.Api.Dto
{
    public class AirportDTO
    {
        [JsonPropertyName("airport_id")]
        public int AirportId { get; set; }

        [JsonPropertyName("iata")]
        public string Iata { get; set; }

        [JsonPropertyName("icao")]
        public string Icao { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public AirportDTO()
        {
            Iata = "";
            Icao = "";
            Name = "";
        }
    }
}