
using Microsoft.Extensions.Configuration;
using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Data;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Test;

public class AirlineServiceTest
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

        AirlineService service = new AirlineService(data);

        // Act
        var aerolineas = service.ObtenerTodos();

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

        AirlineService service = new AirlineService(data);

        int airlineIdEsperado = 1;

        // Act
        var aerolinea = service.ObtenerPorId(airlineIdEsperado);

        // Assert
        Assert.That(aerolinea, Is.Not.Null);
        Assert.That(aerolinea!.AirlineId,
            Is.EqualTo(airlineIdEsperado));
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

        AirlineService service = new AirlineService(data);

        int airportIdEsperado = 1;

        // Act
        bool resultado = service.ExisteAeropuerto(airportIdEsperado);

        // Assert
        Assert.That(resultado, Is.True);
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

        AirlineService service = new AirlineService(data);

        Airline airline = new Airline(
            0,
            "TST",
            "Test Airline",
            1
        );

        // Act
        int airlineId = service.Crear(airline);

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

        AirlineService service = new AirlineService(data);

        Airline airline = new Airline(
            1,
            "TST",
            "Test Airline Updated",
            1
        );

        // Act
        bool resultado = service.Actualizar(airline);

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

        AirlineService service = new AirlineService(data);

        int airlineId = 1;

        // Act
        int resultado = service.Eliminar(airlineId);

        // Assert
        Assert.That(resultado, Is.GreaterThan(0));
    }
}
