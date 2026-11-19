
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Api.Controllers
{
    [ApiController]
    [Route("api/airports")]
    public class AirportsController : ControllerBase
    {
        private readonly AirportService airportService;

        public AirportsController(AirportService airportService)
        {
            this.airportService = airportService;
        }

        // GET: api/airports
        [HttpGet]
        public IActionResult ObtenerTodos([FromQuery] string? busqueda)
        {
            List<Airport> aeropuertos =
                airportService.ObtenerTodos(busqueda);

            return Ok(aeropuertos);
        }

        // GET: api/airports/1
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            Airport? aeropuerto = airportService.ObtenerPorId(id);

            if (aeropuerto == null)
            {
                return NotFound("El aeropuerto no existe.");
            }

            return Ok(aeropuerto);
        }

        // POST: api/airports
        [HttpPost]
        public IActionResult Insertar([FromBody] Airport aeropuerto)
        {
            try
            {
                int airportId = airportService.Insertar(aeropuerto);

                Airport? aeropuertoCreado =
                    airportService.ObtenerPorId(airportId);

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = airportId },
                    aeropuertoCreado
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex) when (
                ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict(
                    "El código IATA o ICAO ya está registrado.");
            }
        }

        // PUT: api/airports/1
        [HttpPut("{id}")]
        public IActionResult Actualizar(
            int id,
            [FromBody] Airport aeropuerto)
        {
            try
            {
                aeropuerto.AirportId = id;

                bool actualizado =
                    airportService.Actualizar(aeropuerto);

                if (!actualizado)
                {
                    return NotFound("El aeropuerto no existe.");
                }

                return Ok("Aeropuerto actualizado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex) when (
                ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict(
                    "El código IATA o ICAO ya está registrado.");
            }
        }

        // DELETE: api/airports/1
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            int resultado = airportService.Eliminar(id);

            if (resultado == 404)
            {
                return NotFound("El aeropuerto no existe.");
            }

            if (resultado == 409)
            {
                return Conflict(
                    "No se puede eliminar el aeropuerto porque está asignado como aeropuerto base de una aerolínea.");
            }

            return Ok("Aeropuerto eliminado correctamente.");
        }
    }
}
