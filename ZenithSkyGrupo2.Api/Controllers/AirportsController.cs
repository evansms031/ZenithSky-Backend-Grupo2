using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Domain;
using ZenithSkyGrupo2.Api.Dto;

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
        // GET: api/airports?busqueda=SJO
        [HttpGet]
        public ActionResult<List<AirportDTO>> ObtenerTodos(
            [FromQuery] string? busqueda)
        {
            List<Airport> aeropuertos =
                airportService.ObtenerTodos(busqueda);

            List<AirportDTO> respuesta = aeropuertos
                .Select(a => new AirportDTO
                {
                    AirportId = a.AirportId,
                    Iata = a.Iata,
                    Icao = a.Icao,
                    Name = a.Name
                })
                .ToList();

            return Ok(respuesta);
        }

        // GET: api/airports/1
        [HttpGet("{id}")]
        public ActionResult<AirportDTO> ObtenerPorId(int id)
        {
            Airport? aeropuerto =
                airportService.ObtenerPorId(id);

            if (aeropuerto == null)
            {
                return NotFound(
                    "El aeropuerto no existe.");
            }

            AirportDTO respuesta = new AirportDTO
            {
                AirportId = aeropuerto.AirportId,
                Iata = aeropuerto.Iata,
                Icao = aeropuerto.Icao,
                Name = aeropuerto.Name
            };

            return Ok(respuesta);
        }

        // POST: api/airports
        [HttpPost]
        public IActionResult Insertar(
            [FromBody] AirportDTO aeropuertoDTO)
        {
            try
            {
                Airport aeropuerto = new Airport(
                    0,
                    aeropuertoDTO.Iata,
                    aeropuertoDTO.Icao,
                    aeropuertoDTO.Name
                );

                int airportId =
                    airportService.Insertar(aeropuerto);

                Airport? aeropuertoCreado =
                    airportService.ObtenerPorId(airportId);

                if (aeropuertoCreado == null)
                {
                    return StatusCode(
                        500,
                        "El aeropuerto fue creado, pero no se pudo recuperar."
                    );
                }

                AirportDTO respuesta = new AirportDTO
                {
                    AirportId = aeropuertoCreado.AirportId,
                    Iata = aeropuertoCreado.Iata,
                    Icao = aeropuertoCreado.Icao,
                    Name = aeropuertoCreado.Name
                };

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = airportId },
                    respuesta
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex) when (
                ex.Number == 2601 ||
                ex.Number == 2627)
            {
                return Conflict(
                    "El código IATA o ICAO ya está registrado."
                );
            }
        }

        // PUT: api/airports/1
        [HttpPut("{id}")]
        public IActionResult Actualizar(
            int id,
            [FromBody] AirportDTO aeropuertoDTO)
        {
            try
            {
                Airport aeropuerto = new Airport(
                    id,
                    aeropuertoDTO.Iata,
                    aeropuertoDTO.Icao,
                    aeropuertoDTO.Name
                );

                bool actualizado =
                    airportService.Actualizar(aeropuerto);

                if (!actualizado)
                {
                    return NotFound(
                        "El aeropuerto no existe."
                    );
                }

                return Ok(
                    "Aeropuerto actualizado correctamente."
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex) when (
                ex.Number == 2601 ||
                ex.Number == 2627)
            {
                return Conflict(
                    "El código IATA o ICAO ya está registrado."
                );
            }
        }

        // DELETE: api/airports/1
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            int resultado =
                airportService.Eliminar(id);

            if (resultado == 404)
            {
                return NotFound(
                    "El aeropuerto no existe."
                );
            }

            if (resultado == 409)
            {
                return Conflict(
                    "No se puede eliminar el aeropuerto porque está asignado como aeropuerto base de una aerolínea."
                );
            }

            return Ok(
                "Aeropuerto eliminado correctamente."
            );
        }
    }
}