
using Microsoft.Extensions.Configuration;
using ZenithSkyGrupo2.Api.Data;

namespace ZenithSkyGrupo2.Test;

public class AirlineDataTest
{
    [Test]
    public void ObtenerTodos_Test()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<SqlConnectionFactory>()
            .Build();

        SqlConnectionFactory factory =
            new SqlConnectionFactory(configuration);

        AirlineData data = new AirlineData(factory);

        // Act
        var aerolineas = data.ObtenerTodos();

        // Assert
        Assert.That(aerolineas, Is.Not.Null);
        Assert.That(aerolineas.Count, Is.GreaterThan(0));
    }

    [Test]
    public void ObtenerPorId_Test()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<SqlConnectionFactory>()
            .Build();

        SqlConnectionFactory factory =
            new SqlConnectionFactory(configuration);

        AirlineData data = new AirlineData(factory);

        int airlineIdEsperado = 1;

        // Act
        var aerolinea = data.ObtenerPorId(airlineIdEsperado);

        // Assert
        Assert.That(aerolinea, Is.Not.Null);
        Assert.That(aerolinea!.AirlineId,
            Is.EqualTo(airlineIdEsperado));
    }

    [Test]
    public void Crear_Test()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<SqlConnectionFactory>()
            .Build();

        SqlConnectionFactory factory =
            new SqlConnectionFactory(configuration);

        AirlineData data = new AirlineData(factory);

        Airline airline = new Airline(
            0,
            "TST",
            "Test Airline",
            1
        );

        // Act
        int airlineId = data.Crear(airline);

        // Assert
        Assert.That(airlineId, Is.GreaterThan(0));
    }

    [Test]
    public void Actualizar_Test()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<SqlConnectionFactory>()
            .Build();

        SqlConnectionFactory factory =
            new SqlConnectionFactory(configuration);

        AirlineData data = new AirlineData(factory);

        Airline airline = new Airline(
            1,
            "TST",
            "Test Airline Updated",
            1
        );

        // Act
        bool resultado = data.Actualizar(airline);

        // Assert
        Assert.That(resultado, Is.True);
    }

    [Test]
    public void Eliminar_Test()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<SqlConnectionFactory>()
            .Build();

        SqlConnectionFactory factory =
            new SqlConnectionFactory(configuration);

        AirlineData data = new AirlineData(factory);

        // Act
        int resultado = data.Eliminar(1);

        // Assert
        Assert.That(resultado, Is.GreaterThan(0));
    }

    [Test]
    public void ExisteAeropuerto_Test()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<SqlConnectionFactory>()
            .Build();

        SqlConnectionFactory factory =
            new SqlConnectionFactory(configuration);

        AirlineData data = new AirlineData(factory);

        int airportIdEsperado = 1;

        // Act
        bool resultado = data.ExisteAeropuerto(airportIdEsperado);

        // Assert
        Assert.That(resultado, Is.True);
    }
}
