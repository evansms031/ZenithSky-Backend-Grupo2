using Microsoft.AspNetCore.Mvc;
using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Domain;
using ZenithSkyGrupo2.Api.Dto;

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

        // GET /api/airplane-types
        [HttpGet]
        public ActionResult<List<AirplaneTypeDTO>> ObtenerTodos()
        {
            List<AirplaneType> tipos =
                airplaneTypeService.ObtenerTodos();

            List<AirplaneTypeDTO> respuesta = tipos
                .Select(t => new AirplaneTypeDTO
                {
                    TypeId = t.TypeId,
                    Identifier = t.Identifier,
                    Description = t.Description
                })
                .ToList();

            return Ok(respuesta);
        }
    }
}