using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Data
{
    public class AirlineData
    {
        private readonly SqlConnectionFactory connectionFactory;

        public AirlineData(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public List<Airline> ObtenerTodos()
        {
            List<Airline> aerolineas = new List<Airline>();

            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airline_GetAll";
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Airline airline = CrearAirlineDesdeReader(reader);
                        aerolineas.Add(airline);
                    }
                }
            }

            return aerolineas;
        }

        public Airline? ObtenerPorId(int airlineId)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airline_GetById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@airline_id", airlineId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return CrearAirlineDesdeReader(reader);
                    }
                }
            }

            return null;
        }

        public int Crear(Airline airline)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airline_Create";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iata", airline.Iata);
                cmd.Parameters.AddWithValue("@airlinename", airline.Airlinename);
                cmd.Parameters.AddWithValue("@base_airport", airline.BaseAirport);

                object? result = cmd.ExecuteScalar();

                return Convert.ToInt32(result);
            }
        }

        public bool Actualizar(Airline airline)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airline_Update";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@airline_id", airline.AirlineId);
                cmd.Parameters.AddWithValue("@iata", airline.Iata);
                cmd.Parameters.AddWithValue("@airlinename", airline.Airlinename);
                cmd.Parameters.AddWithValue("@base_airport", airline.BaseAirport);

                int rowsAffected = Convert.ToInt32(
                    cmd.ExecuteScalar());

                return rowsAffected > 0;
            }
        }

        public int Eliminar(int airlineId)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airline_Delete";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@airline_id", airlineId);

                object? result = cmd.ExecuteScalar();

                return Convert.ToInt32(result);
            }
        }

        private Airline CrearAirlineDesdeReader(SqlDataReader reader)
        {
            int airlineId =
                Convert.ToInt32(reader["airline_id"]);

            string iata =
                reader["iata"].ToString()?.Trim() ?? "";

            string airlinename =
                reader["airlinename"].ToString() ?? "";

            int baseAirport =
                Convert.ToInt32(reader["base_airport"]);

            string? baseAirportIata =
                reader["base_airport_iata"] == DBNull.Value
                ? null
                : reader["base_airport_iata"].ToString()?.Trim();

            string? baseAirportName =
                reader["base_airport_name"] == DBNull.Value
                ? null
                : reader["base_airport_name"].ToString();

            return new Airline(
                airlineId,
                iata,
                airlinename,
                baseAirport,
                baseAirportIata,
                baseAirportName
            );
        }

        public bool ExisteAeropuerto(int airportId)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = @"
            SELECT COUNT(1)
            FROM dbo.airport
            WHERE airport_id = @airport_id";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@airport_id", airportId);

                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                return cantidad > 0;
            }
        }
    }
}