using System.Text.Json.Serialization;

namespace ZenithSkyGrupo2.Api.Domain
{
    public class Airplane
    {
        private int airplaneId;
        private int capacity;
        private int typeId;
        private int airlineId;
        private string identifier;
        private string? description;
        private string airlineName;

        public Airplane()
        {
            this.identifier = "";
            this.description = null;
            this.airlineName = "";
        }

        public Airplane(int airplaneId, int capacity,
            int typeId, int airlineId, string identifier,
            string? description, string airlineName)
        {
            this.airplaneId = airplaneId;
            this.capacity = capacity;
            this.typeId = typeId;
            this.airlineId = airlineId;
            this.identifier = identifier;
            this.description = description;
            this.airlineName = airlineName;
        }

        [JsonPropertyName("airplane_id")]
        public int AirplaneId
        {
            get => airplaneId;
            set => airplaneId = value;
        }

        public int Capacity
        {
            get => capacity;
            set => capacity = value;
        }

        public int TypeId
        {
            get => typeId;
            set => typeId = value;
        }

        public int AirlineId
        {
            get => airlineId;
            set => airlineId = value;
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

        public string AirlineName
        {
            get => airlineName;
            set => airlineName = value;
        }
    }
}