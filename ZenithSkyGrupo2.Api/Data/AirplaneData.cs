using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Data
{
    public class AirplaneData
    {
        private readonly SqlConnectionFactory connectionFactory;

        public AirplaneData(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        // Obtiene todos los aviones, con opción de filtrar por tipo y aerolínea

        public List<Airplane> ObtenerTodos(int? typeId = null,
            int? airlineId = null)
        {
            List<Airplane> aviones = new List<Airplane>();

            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.sp_GetAirplanes";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@type_id", SqlDbType.Int)
                    .Value = (object?)typeId ?? DBNull.Value;

                cmd.Parameters.Add("@airline_id", SqlDbType.Int)
                    .Value = (object?)airlineId ?? DBNull.Value;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        aviones.Add(LeerAvion(reader));
                    }
                }
            }

            return aviones;
        }


        // Obtiene aviones por ID
        public Airplane? ObtenerPorId(int airplaneId)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.sp_GetAirplaneById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@airplane_id", SqlDbType.Int)
                    .Value = airplaneId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return LeerAvion(reader);
                    }
                }
            }

            return null;
        }

        // Insertar avión nuevo
        public bool Insertar(Airplane avion)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.sp_InsertAirplane";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@capacity", SqlDbType.Int)
                    .Value = avion.Capacity;

                cmd.Parameters.Add("@type_id", SqlDbType.Int)
                    .Value = avion.TypeId;

                cmd.Parameters.Add("@airline_id", SqlDbType.Int)
                    .Value = avion.AirlineId;

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Actualizar avión existente
        public bool Actualizar(Airplane avion)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.sp_UpdateAirplane";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@airplane_id", SqlDbType.Int)
                    .Value = avion.AirplaneId;

                cmd.Parameters.Add("@capacity", SqlDbType.Int)
                    .Value = avion.Capacity;

                cmd.Parameters.Add("@type_id", SqlDbType.Int)
                    .Value = avion.TypeId;

                cmd.Parameters.Add("@airline_id", SqlDbType.Int)
                    .Value = avion.AirlineId;

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int airplaneId)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.sp_DeleteAirplane";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@airplane_id", SqlDbType.Int)
                    .Value = airplaneId;

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Convertir SqlDataReader a objeto Airplane
        private Airplane LeerAvion(SqlDataReader reader)
        {
            int airplaneId =
                Convert.ToInt32(reader["airplane_id"]);

            int capacity =
                Convert.ToInt32(reader["capacity"]);

            int typeId =
                Convert.ToInt32(reader["type_id"]);

            int airlineId =
                Convert.ToInt32(reader["airline_id"]);

            string identifier =
                reader["identifier"].ToString() ?? "";

            string? description =
                reader["description"] == DBNull.Value
                ? null
                : reader["description"].ToString();

            string airlineName =
                reader["airlinename"].ToString() ?? "";

            return new Airplane(
                airplaneId,
                capacity,
                typeId,
                airlineId,
                identifier,
                description,
                airlineName
            );
        }

        // Verifica existencia de avión por ID

        public bool ExisteTipoAvion(int typeId)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM airplane_type WHERE type_id = @type_id";

                cmd.Parameters.Add("@type_id", SqlDbType.Int)
                    .Value = typeId;

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }


        // Verifica existencia de aerolínea por ID

        public bool ExisteAerolinea(int airlineId)
        {
            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM airline WHERE airline_id = @airline_id";

                cmd.Parameters.Add("@airline_id", SqlDbType.Int)
                    .Value = airlineId;

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
    }
}