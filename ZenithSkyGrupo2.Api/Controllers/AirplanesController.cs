using Microsoft.AspNetCore.Mvc;
using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Domain;
using ZenithSkyGrupo2.Api.Dto;

namespace ZenithSkyGrupo2.Api.Controllers
{
    [ApiController]
    [Route("api/airplanes")]
    public class AirplanesController : ControllerBase
    {
        private readonly AirplaneService airplaneService;

        public AirplanesController(
            AirplaneService airplaneService)
        {
            this.airplaneService = airplaneService;
        }

        // GET /api/airplanes
        // GET /api/airplanes?typeId=1
        // GET /api/airplanes?airlineId=1
        [HttpGet]
        public ActionResult<List<AirplaneDTO>> ObtenerTodos(
            [FromQuery] int? typeId = null,
            [FromQuery] int? airlineId = null)
        {
            List<Airplane> aviones =
                airplaneService.ObtenerTodos(
                    typeId,
                    airlineId
                );

            List<AirplaneDTO> respuesta = aviones
                .Select(a => new AirplaneDTO
                {
                    AirplaneId = a.AirplaneId,
                    Capacity = a.Capacity,
                    TypeId = a.TypeId,
                    AirlineId = a.AirlineId,
                    Identifier = a.Identifier,
                    Description = a.Description,
                    AirlineName = a.AirlineName
                })
                .ToList();

            return Ok(respuesta);
        }

        // GET /api/airplanes/{id}
        [HttpGet("{id}")]
        public ActionResult<AirplaneDTO> ObtenerPorId(int id)
        {
            Airplane? avion =
                airplaneService.ObtenerPorId(id);

            if (avion == null)
            {
                return NotFound(
                    "El avión no existe."
                );
            }

            AirplaneDTO respuesta = new AirplaneDTO
            {
                AirplaneId = avion.AirplaneId,
                Capacity = avion.Capacity,
                TypeId = avion.TypeId,
                AirlineId = avion.AirlineId,
                Identifier = avion.Identifier,
                Description = avion.Description,
                AirlineName = avion.AirlineName
            };

            return Ok(respuesta);
        }

        // POST /api/airplanes
        [HttpPost]
        public IActionResult Insertar(
            [FromBody] AirplaneDTO avionDTO)
        {
            try
            {
                Airplane avion = new Airplane(
                    0,
                    avionDTO.Capacity,
                    avionDTO.TypeId,
                    avionDTO.AirlineId,
                    "",
                    null,
                    ""
                );

                if (airplaneService.Insertar(avion))
                {
                    return StatusCode(
                        201,
                        "Avión creado correctamente."
                    );
                }

                return BadRequest(
                    "No se pudo insertar el avión."
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT /api/airplanes/{id}
        [HttpPut("{id}")]
        public IActionResult Actualizar(
            int id,
            [FromBody] AirplaneDTO avionDTO)
        {
            try
            {
                Airplane avion = new Airplane(
                    id,
                    avionDTO.Capacity,
                    avionDTO.TypeId,
                    avionDTO.AirlineId,
                    "",
                    null,
                    ""
                );

                if (airplaneService.Actualizar(avion))
                {
                    return NoContent();
                }

                return NotFound(
                    "El avión no existe."
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE /api/airplanes/{id}
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            if (airplaneService.Eliminar(id))
            {
                return NoContent();
            }

            return NotFound(
                "El avión no existe."
            );
        }
    }
}