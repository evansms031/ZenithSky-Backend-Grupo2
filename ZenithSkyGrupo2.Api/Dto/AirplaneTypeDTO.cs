using System.Text.Json.Serialization;

namespace ZenithSkyGrupo2.Api.Dto
{
    public class AirplaneTypeDTO
    {
        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        public AirplaneTypeDTO()
        {
            Identifier = "";
            Description = null;
        }
    }
}