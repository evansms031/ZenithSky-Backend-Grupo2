using System.Text.Json.Serialization;

namespace ZenithSkyGrupo2.Api.Domain
{
    public class Airline
    {
        private int airlineId;
        private string iata;
        private string airlinename;
        private int baseAirport;

        // Datos del aeropuerto base
        private string? baseAirportIata;
        private string? baseAirportName;

        public Airline()
        {
            this.iata = "";
            this.airlinename = "";
            this.baseAirportIata = null;
            this.baseAirportName = null;
        }

        public Airline(
            int airlineId,
            string iata,
            string airlinename,
            int baseAirport,
            string? baseAirportIata = null,
            string? baseAirportName = null)
        {
            this.airlineId = airlineId;
            this.iata = iata;
            this.airlinename = airlinename;
            this.baseAirport = baseAirport;
            this.baseAirportIata = baseAirportIata;
            this.baseAirportName = baseAirportName;
        }

        [JsonPropertyName("airline_id")]
        public int AirlineId
        {
            get => airlineId;
            set => airlineId = value;
        }

        public string Iata
        {
            get => iata;
            set => iata = value;
        }

        public string Airlinename
        {
            get => airlinename;
            set => airlinename = value;
        }

        [JsonPropertyName("base_airport")]
        public int BaseAirport
        {
            get => baseAirport;
            set => baseAirport = value;
        }

        [JsonPropertyName("base_airport_iata")]
        public string? BaseAirportIata
        {
            get => baseAirportIata;
            set => baseAirportIata = value;
        }

        [JsonPropertyName("base_airport_name")]
        public string? BaseAirportName
        {
            get => baseAirportName;
            set => baseAirportName = value;
        }
    }
}