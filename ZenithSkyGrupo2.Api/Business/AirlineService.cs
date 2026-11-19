using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Data;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Business
{
    public class AirlineService
    {
        private readonly AirlineData airlineData;

        public AirlineService(AirlineData airlineData)
        {
            this.airlineData = airlineData;
        }

        public List<Airline> ObtenerTodos()
        {
            return airlineData.ObtenerTodos();
        }

        public Airline? ObtenerPorId(int airlineId)
        {
            return airlineData.ObtenerPorId(airlineId);
        }

        public bool ExisteAeropuerto(int airportId)
        {
            return airlineData.ExisteAeropuerto(airportId);
        }

        public int Crear(Airline airline)
        {
            return airlineData.Crear(airline);
        }

        public bool Actualizar(Airline airline)
        {
            return airlineData.Actualizar(airline);
        }

        public int Eliminar(int airlineId)
        {
            return airlineData.Eliminar(airlineId);
        }
    }
}