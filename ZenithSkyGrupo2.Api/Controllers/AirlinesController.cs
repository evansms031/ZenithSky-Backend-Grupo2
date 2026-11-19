using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Controllers
{
    [ApiController]
    [Route("api/airlines")]
    public class AirlinesController : ControllerBase
    {
        private readonly AirlineService airlineService;

        public AirlinesController(
            AirlineService airlineService)
        {
            this.airlineService = airlineService;
        }

        // GET /api/airlines
        [HttpGet]
        public ActionResult<List<Airline>> ObtenerTodos()
        {
            List<Airline> aerolineas =
                airlineService.ObtenerTodos();

            return Ok(aerolineas);
        }

        // GET /api/airlines/{id}
        [HttpGet("{id}")]
        public ActionResult<Airline> ObtenerPorId(int id)
        {
            Airline? airline =
                airlineService.ObtenerPorId(id);

            if (airline == null)
            {
                return NotFound();
            }

            return Ok(airline);
        }

        // POST /api/airlines
        [HttpPost]
        public ActionResult Crear([FromBody] Airline airline)
        {
            if (airline == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(airline.Iata) ||
                airline.Iata.Trim().Length != 2)
            {
                return BadRequest(
                    "El IATA de la aerolínea debe tener exactamente 2 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(airline.Airlinename))
            {
                return BadRequest(
                    "El nombre de la aerolínea es obligatorio.");
            }

            if (airline.BaseAirport <= 0)
            {
                return BadRequest(
                    "Debe indicar un aeropuerto base válido.");
            }

            if (!airlineService.ExisteAeropuerto(airline.BaseAirport))
            {
                return BadRequest(
                    "El aeropuerto base indicado no existe.");
            }

            try
            {
                int id = airlineService.Crear(airline);

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = id },
                    new { airline_id = id }
                );
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                return BadRequest(
                    "Los datos de la aerolínea no son válidos.");
            }
        }

        // PUT /api/airlines/{id}
        [HttpPut("{id}")]
        public ActionResult Actualizar(
            int id,
            [FromBody] Airline airline)
        {
            if (airline == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(airline.Iata) ||
                airline.Iata.Trim().Length != 2)
            {
                return BadRequest(
                    "El IATA de la aerolínea debe tener exactamente 2 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(airline.Airlinename))
            {
                return BadRequest(
                    "El nombre de la aerolínea es obligatorio.");
            }

            if (airline.BaseAirport <= 0)
            {
                return BadRequest(
                    "Debe indicar un aeropuerto base válido.");
            }

            if (!airlineService.ExisteAeropuerto(airline.BaseAirport))
            {
                return BadRequest(
                    "El aeropuerto base indicado no existe.");
            }

            airline.AirlineId = id;

            try
            {
                bool actualizado =
                    airlineService.Actualizar(airline);

                if (!actualizado)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                return BadRequest(
                    "Los datos de la aerolínea no son válidos.");
            }
        }

        // DELETE /api/airlines/{id}
        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            int resultado =
                airlineService.Eliminar(id);

            if (resultado == -1)
            {
                return NotFound();
            }

            if (resultado == 0)
            {
                return Conflict(
                    "No se puede eliminar la aerolínea porque tiene aviones asociados.");
            }

            return NoContent();
        }
    }
}