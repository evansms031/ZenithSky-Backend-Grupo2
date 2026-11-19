using Microsoft.AspNetCore.Mvc;
using ZenithSkyGrupo2.Api.Business;
using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Domain;
using System;

namespace ZenithSkyGrupo2.Api.Controllers
{
    [ApiController]
    [Route("api/airplanes")]
    public class AirplanesController : ControllerBase
    {
        private readonly AirplaneService airplaneService;

        public AirplanesController(AirplaneService airplaneService)
        {
            this.airplaneService = airplaneService;
        }

        [HttpGet] // GET /api/airplanes
        public ActionResult<List<Airplane>> ObtenerTodos(
            [FromQuery] int? typeId = null,
            [FromQuery] int? airlineId = null)
        {
            List<Airplane> aviones =
                airplaneService.ObtenerTodos(typeId, airlineId);

            return Ok(aviones);
        }

        [HttpGet("{id}")] // GET /api/airplanes/{id}
        public ActionResult<Airplane> ObtenerPorId(int id)
        {
            Airplane? avion = airplaneService.ObtenerPorId(id);

            if (avion == null)
            {
                return NotFound("El avión no existe.");
            }

            return Ok(avion);
        }

        [HttpPost] // POST /api/airplanes
        public IActionResult Insertar([FromBody] Airplane avion)
        {
            try
            {
                if (airplaneService.Insertar(avion))
                {
                    return StatusCode(201, "Avión creado correctamente.");
                }

                return BadRequest("No se pudo insertar el avión.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")] // PUT /api/airplanes/{id}
        public IActionResult Actualizar(int id,
            [FromBody] Airplane avion)
        {
            try
            {
                avion.AirplaneId = id;

                if (airplaneService.Actualizar(avion))
                {
                    return NoContent();
                }

                return NotFound("El avión no existe.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE /api/airplanes/{id}
        public IActionResult Eliminar(int id)
        {
            if (airplaneService.Eliminar(id))
            {
                return NoContent();
            }

            return NotFound("El avión no existe.");
        }
    }
}