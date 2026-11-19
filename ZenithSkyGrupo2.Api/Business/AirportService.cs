
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using ZenithSkyGrupo2.Api.Data;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Business
{
    public class AirportService
    {
        private readonly AirportData airportData;

        public AirportService(AirportData airportData)
        {
            this.airportData = airportData;
        }

        public List<Airport> ObtenerTodos(string? busqueda)
        {
            return airportData.ObtenerTodos(busqueda);
        }

        public Airport? ObtenerPorId(int airportId)
        {
            return airportData.ObtenerPorId(airportId);
        }

        public int Insertar(Airport aeropuerto)
        {
            ValidarAeropuerto(aeropuerto);

            return airportData.Insertar(aeropuerto);
        }

        public bool Actualizar(Airport aeropuerto)
        {
            ValidarAeropuerto(aeropuerto);

            return airportData.Actualizar(aeropuerto) > 0;
        }

        public int Eliminar(int airportId)
        {
            return airportData.Eliminar(airportId);
        }

        private void ValidarAeropuerto(Airport aeropuerto)
        {
            if (aeropuerto == null)
            {
                throw new ArgumentException(
                    "Los datos del aeropuerto son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(aeropuerto.Iata) ||
                !Regex.IsMatch(aeropuerto.Iata, "^[A-Za-z]{3}$"))
            {
                throw new ArgumentException(
                    "El código IATA debe contener exactamente 3 letras.");
            }

            if (string.IsNullOrWhiteSpace(aeropuerto.Icao) ||
                !Regex.IsMatch(aeropuerto.Icao, "^[A-Za-z]{4}$"))
            {
                throw new ArgumentException(
                    "El código ICAO debe contener exactamente 4 letras.");
            }

            if (string.IsNullOrWhiteSpace(aeropuerto.Name))
            {
                throw new ArgumentException(
                    "El nombre del aeropuerto es obligatorio.");
            }
        }
    }
}
