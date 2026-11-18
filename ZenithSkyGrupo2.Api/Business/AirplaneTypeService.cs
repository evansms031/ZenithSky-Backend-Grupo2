using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Data;
using ZenithSkyGrupo2.Api.Domain;


namespace ZenithSkyGrupo2.Api.Business
{
    public class AirplaneTypeService
    {
        private readonly AirplaneTypeData airplaneTypeData;

        public AirplaneTypeService(AirplaneTypeData airplaneTypeData)
        {
            this.airplaneTypeData = airplaneTypeData;
        }

        public List<AirplaneType> ObtenerTodos()
        {
            return airplaneTypeData.ObtenerTodos();
        }
    }
}
