using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Data
{
    public class AirplaneTypeData
    {
        private readonly SqlConnectionFactory connectionFactory;

        public AirplaneTypeData(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public List<AirplaneType> ObtenerTodos()
        {
            List<AirplaneType> tipos = new List<AirplaneType>();

            using (SqlConnection connection =
                connectionFactory.CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.AirplaneType_GetAll";
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int typeId = Convert.ToInt32(reader["type_id"]);

                        string identifier =
                            reader["identifier"].ToString() ?? "";

                        string? description =
                            reader["description"] == DBNull.Value
                            ? null
                            : reader["description"].ToString();

                        AirplaneType tipo = new AirplaneType(
                            typeId,
                            identifier,
                            description
                        );

                        tipos.Add(tipo);
                    }
                }
            }

            return tipos;
        }
    }
}
