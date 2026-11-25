using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Domain;
using ZenithSkyGrupo2.Api.Dto;

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
        public ActionResult<List<AirlineDTO>> ObtenerTodos()
        {
            List<Airline> aerolineas =
                airlineService.ObtenerTodos();

            List<AirlineDTO> respuesta = aerolineas
                .Select(a => new AirlineDTO
                {
                    AirlineId = a.AirlineId,
                    Iata = a.Iata,
                    Airlinename = a.Airlinename,
                    BaseAirport = a.BaseAirport,
                    BaseAirportIata = a.BaseAirportIata,
                    BaseAirportName = a.BaseAirportName
                })
                .ToList();

            return Ok(respuesta);
        }

        // GET /api/airlines/{id}
        [HttpGet("{id}")]
        public ActionResult<AirlineDTO> ObtenerPorId(int id)
        {
            Airline? airline =
                airlineService.ObtenerPorId(id);

            if (airline == null)
            {
                return NotFound(
                    "La aerolínea no existe."
                );
            }

            AirlineDTO respuesta = new AirlineDTO
            {
                AirlineId = airline.AirlineId,
                Iata = airline.Iata,
                Airlinename = airline.Airlinename,
                BaseAirport = airline.BaseAirport,
                BaseAirportIata = airline.BaseAirportIata,
                BaseAirportName = airline.BaseAirportName
            };

            return Ok(respuesta);
        }

        // POST /api/airlines
        [HttpPost]
        public ActionResult Crear(
            [FromBody] AirlineDTO airlineDTO)
        {
            if (airlineDTO == null)
            {
                return BadRequest(
                    "Los datos de la aerolínea son obligatorios."
                );
            }

            if (string.IsNullOrWhiteSpace(airlineDTO.Iata) ||
                airlineDTO.Iata.Trim().Length != 2)
            {
                return BadRequest(
                    "El IATA de la aerolínea debe tener exactamente 2 caracteres."
                );
            }

            if (string.IsNullOrWhiteSpace(
                    airlineDTO.Airlinename))
            {
                return BadRequest(
                    "El nombre de la aerolínea es obligatorio."
                );
            }

            if (airlineDTO.BaseAirport <= 0)
            {
                return BadRequest(
                    "Debe indicar un aeropuerto base válido."
                );
            }

            if (!airlineService.ExisteAeropuerto(
                    airlineDTO.BaseAirport))
            {
                return BadRequest(
                    "El aeropuerto base indicado no existe."
                );
            }

            Airline airline = new Airline(
                0,
                airlineDTO.Iata,
                airlineDTO.Airlinename,
                airlineDTO.BaseAirport
            );

            try
            {
                int id =
                    airlineService.Crear(airline);

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = id },
                    new { airline_id = id }
                );
            }
            catch (SqlException)
            {
                return BadRequest(
                    "Los datos de la aerolínea no son válidos."
                );
            }
        }

        // PUT /api/airlines/{id}
        [HttpPut("{id}")]
        public ActionResult Actualizar(
            int id,
            [FromBody] AirlineDTO airlineDTO)
        {
            if (airlineDTO == null)
            {
                return BadRequest(
                    "Los datos de la aerolínea son obligatorios."
                );
            }

            if (string.IsNullOrWhiteSpace(airlineDTO.Iata) ||
                airlineDTO.Iata.Trim().Length != 2)
            {
                return BadRequest(
                    "El IATA de la aerolínea debe tener exactamente 2 caracteres."
                );
            }

            if (string.IsNullOrWhiteSpace(
                    airlineDTO.Airlinename))
            {
                return BadRequest(
                    "El nombre de la aerolínea es obligatorio."
                );
            }

            if (airlineDTO.BaseAirport <= 0)
            {
                return BadRequest(
                    "Debe indicar un aeropuerto base válido."
                );
            }

            if (!airlineService.ExisteAeropuerto(
                    airlineDTO.BaseAirport))
            {
                return BadRequest(
                    "El aeropuerto base indicado no existe."
                );
            }

            Airline airline = new Airline(
                id,
                airlineDTO.Iata,
                airlineDTO.Airlinename,
                airlineDTO.BaseAirport
            );

            try
            {
                bool actualizado =
                    airlineService.Actualizar(airline);

                if (!actualizado)
                {
                    return NotFound(
                        "La aerolínea no existe."
                    );
                }

                return NoContent();
            }
            catch (SqlException)
            {
                return BadRequest(
                    "Los datos de la aerolínea no son válidos."
                );
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
                return NotFound(
                    "La aerolínea no existe."
                );
            }

            if (resultado == 0)
            {
                return Conflict(
                    "No se puede eliminar la aerolínea porque tiene aviones asociados."
                );
            }

            return NoContent();
        }
    }
}