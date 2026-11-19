using System;
using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Data;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Business
{
    public class AirplaneService
    {
        private readonly AirplaneData airplaneData;

        public AirplaneService(AirplaneData airplaneData)
        {
            this.airplaneData = airplaneData;
        }

        // Obtiene todos los aviones, con opción de filtrar por tipo y aerolínea

        public List<Airplane> ObtenerTodos(int? typeId = null,
            int? airlineId = null)
        {
            return airplaneData.ObtenerTodos(typeId, airlineId);
        }


        // Obtiene avión por ID

        public Airplane? ObtenerPorId(int airplaneId)
        {
            return airplaneData.ObtenerPorId(airplaneId);
        }

        // Inserta un nuevo avión
        public bool Insertar(Airplane avion)
        {
            ValidarAvion(avion);

            return airplaneData.Insertar(avion);
        }

        // Actualiza un avión existente

        public bool Actualizar(Airplane avion)
        {
            ValidarAvion(avion);

            return airplaneData.Actualizar(avion);
        }

        // Elimina un avión por ID
        public bool Eliminar(int airplaneId)
        {
            return airplaneData.Eliminar(airplaneId);
        }


        // Reglas de negocio
        // Valida los datos de un avión antes de insertarlo o actualizarlo
        private void ValidarAvion(Airplane avion)
        {
            if (avion.Capacity <= 0)
            {
                throw new ArgumentException(
                    "La capacidad debe ser mayor que cero.");
            }

            if (!airplaneData.ExisteTipoAvion(avion.TypeId))
            {
                throw new ArgumentException(
                    "El tipo de avión no existe.");
            }

            if (!airplaneData.ExisteAerolinea(avion.AirlineId))
            {
                throw new ArgumentException(
                    "La aerolínea no existe.");
            }
        }
    }
}