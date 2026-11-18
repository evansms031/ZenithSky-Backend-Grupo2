using System.Text.Json.Serialization;

namespace ZenithSkyGrupo2.Api.Domain
{
    public class AirplaneType
    {
        private int typeId;
        private string identifier;
        private string? description;

        public AirplaneType()
        {
            this.identifier = "";
            this.description = null;
        }

        public AirplaneType(int typeId, string identifier,
            string? description)
        {
            this.typeId = typeId;
            this.identifier = identifier;
            this.description = description;
        }

        [JsonPropertyName("type_id")]
        public int TypeId
        {
            get => typeId;
            set => typeId = value;
        }

        public string Identifier
        {
            get => identifier;
            set => identifier = value;
        }

        public string? Description
        {
            get => description;
            set => description = value;
        }
    }
}
