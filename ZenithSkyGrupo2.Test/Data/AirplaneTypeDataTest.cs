using Microsoft.Extensions.Configuration;
using ZenithSkyGrupo2.Api.Data;

namespace ZenithSkyGrupo2.Test;

public class AirplaneTypeDataTest
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

        AirplaneTypeData data = new AirplaneTypeData(factory);

        // Act
        var tipos = data.ObtenerTodos();

        // Assert
        Assert.That(tipos, Is.Not.Null);
        Assert.That(tipos.Count, Is.GreaterThan(0));
    }
}
