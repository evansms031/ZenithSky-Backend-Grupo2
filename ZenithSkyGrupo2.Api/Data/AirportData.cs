
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Data
{
    public class AirportData
    {
        private readonly SqlConnectionFactory connectionFactory;

        public AirportData(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public List<Airport> ObtenerTodos(string? busqueda)
        {
            List<Airport> aeropuertos = new List<Airport>();

            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airport_GetAll";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@busqueda",
                    string.IsNullOrWhiteSpace(busqueda)
                    ? DBNull.Value
                    : busqueda
                );

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Airport aeropuerto = new Airport(
                            Convert.ToInt32(reader["airport_id"]),
                            reader["iata"].ToString() ?? "",
                            reader["icao"].ToString() ?? "",
                            reader["name"].ToString() ?? ""
                        );

                        aeropuertos.Add(aeropuerto);
                    }
                }
            }

            return aeropuertos;
        }

        public Airport? ObtenerPorId(int airportId)
        {
            Airport? aeropuerto = null;

            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airport_GetById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@airport_id", airportId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        aeropuerto = new Airport(
                            Convert.ToInt32(reader["airport_id"]),
                            reader["iata"].ToString() ?? "",
                            reader["icao"].ToString() ?? "",
                            reader["name"].ToString() ?? ""
                        );
                    }
                }
            }

            return aeropuerto;
        }

        public int Insertar(Airport aeropuerto)
        {
            int airportId = 0;

            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airport_Insert";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iata", aeropuerto.Iata);
                cmd.Parameters.AddWithValue("@icao", aeropuerto.Icao);
                cmd.Parameters.AddWithValue("@name", aeropuerto.Name);

                airportId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return airportId;
        }

        public int Actualizar(Airport aeropuerto)
        {
            int filasAfectadas = 0;

            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airport_Update";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@airport_id", aeropuerto.AirportId);

                cmd.Parameters.AddWithValue("@iata", aeropuerto.Iata);
                cmd.Parameters.AddWithValue("@icao", aeropuerto.Icao);
                cmd.Parameters.AddWithValue("@name", aeropuerto.Name);

                filasAfectadas = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return filasAfectadas;
        }

        public int Eliminar(int airportId)
        {
            int resultado = 0;

            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.Airport_Delete";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@airport_id", airportId);

                resultado = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return resultado;
        }
    }
}
