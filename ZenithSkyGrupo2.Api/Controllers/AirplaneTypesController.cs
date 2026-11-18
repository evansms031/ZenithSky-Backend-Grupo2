using Microsoft.AspNetCore.Mvc;
using ZenithSkyGrupo2.Api.Business;
using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Controllers
{
    [ApiController]
    [Route("api/airplane-types")]
    public class AirplaneTypesController : ControllerBase
    {
        private readonly AirplaneTypeService airplaneTypeService;

        public AirplaneTypesController(
            AirplaneTypeService airplaneTypeService)
        {
            this.airplaneTypeService = airplaneTypeService;
        }

        [HttpGet]
        public ActionResult<List<AirplaneType>> ObtenerTodos()
        {
            List<AirplaneType> tipos =
                airplaneTypeService.ObtenerTodos();

            return Ok(tipos);
        }
    }
}
