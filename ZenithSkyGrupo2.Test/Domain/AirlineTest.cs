
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Test;

public class AirlineTest
{
    [Test]
    public void Airline_Constructor_Con_PropiedadesInicializadas()
    {
        // Arrange
        int airlineIdEsperado = 1;
        string iataEsperado = "AV";
        string airlinenameEsperado = "Avianca";
        int baseAirportEsperado = 10;
        string baseAirportIataEsperado = "SJO";
        string baseAirportNameEsperado = "Juan Santamaria";

        // Act
        Airline airline = new Airline(
            airlineIdEsperado,
            iataEsperado,
            airlinenameEsperado,
            baseAirportEsperado,
            baseAirportIataEsperado,
            baseAirportNameEsperado
        );

        // Assert
        Assert.That(airline.AirlineId,
            Is.EqualTo(airlineIdEsperado));

        Assert.That(airline.Iata,
            Is.EqualTo(iataEsperado));

        Assert.That(airline.Airlinename,
            Is.EqualTo(airlinenameEsperado));

        Assert.That(airline.BaseAirport,
            Is.EqualTo(baseAirportEsperado));

        Assert.That(airline.BaseAirportIata,
            Is.EqualTo(baseAirportIataEsperado));

        Assert.That(airline.BaseAirportName,
            Is.EqualTo(baseAirportNameEsperado));
    }

    [Test]
    public void Airline_Constructor_Vacio()
    {
        // Arrange
        int airlineIdEsperado = 0;
        string iataEsperado = "";
        string airlinenameEsperado = "";
        int baseAirportEsperado = 0;

        // Act
        Airline airline = new Airline();

        // Assert
        Assert.That(airline.AirlineId,
            Is.EqualTo(airlineIdEsperado));

        Assert.That(airline.Iata,
            Is.EqualTo(iataEsperado));

        Assert.That(airline.Airlinename,
            Is.EqualTo(airlinenameEsperado));

        Assert.That(airline.BaseAirport,
            Is.EqualTo(baseAirportEsperado));

        Assert.That(airline.BaseAirportIata,
            Is.Null);

        Assert.That(airline.BaseAirportName,
            Is.Null);
    }

    [Test]
    public void Airline_Modificar_Propiedades()
    {
        // Arrange
        Airline airline = new Airline();

        int airlineIdEsperado = 2;
        string iataEsperado = "CM";
        string airlinenameEsperado = "Copa Airlines";
        int baseAirportEsperado = 20;
        string baseAirportIataEsperado = "PTY";
        string baseAirportNameEsperado = "Tocumen";

        // Act
        airline.AirlineId = airlineIdEsperado;
        airline.Iata = iataEsperado;
        airline.Airlinename = airlinenameEsperado;
        airline.BaseAirport = baseAirportEsperado;
        airline.BaseAirportIata = baseAirportIataEsperado;
        airline.BaseAirportName = baseAirportNameEsperado;

        // Assert
        Assert.That(airline.AirlineId,
            Is.EqualTo(airlineIdEsperado));

        Assert.That(airline.Iata,
            Is.EqualTo(iataEsperado));

        Assert.That(airline.Airlinename,
            Is.EqualTo(airlinenameEsperado));

        Assert.That(airline.BaseAirport,
            Is.EqualTo(baseAirportEsperado));

        Assert.That(airline.BaseAirportIata,
            Is.EqualTo(baseAirportIataEsperado));

        Assert.That(airline.BaseAirportName,
            Is.EqualTo(baseAirportNameEsperado));
    }
}
