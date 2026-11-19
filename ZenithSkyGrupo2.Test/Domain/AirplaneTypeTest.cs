using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Test;

public class AirplaneTypeTest
{

    [Test]
    public void AirplaneType_Constructor_Con_PropiedadesInicializadas()
    {
        // Arrange
        int typeIdEsperado = 1;
        string identifierEsperado = "A320";
        string descriptionEsperada = "Airbus A320";

        // Act
        AirplaneType airplaneType = new AirplaneType(
            typeIdEsperado,
            identifierEsperado,
            descriptionEsperada
        );

        // Assert
        Assert.That(airplaneType.TypeId,
            Is.EqualTo(typeIdEsperado));

        Assert.That(airplaneType.Identifier,
            Is.EqualTo(identifierEsperado));

        Assert.That(airplaneType.Description,
            Is.EqualTo(descriptionEsperada));
    }

    [Test]
    public void AirplaneType_Constructor_Vacio()
    {
        // Arrange
        int typeIdEsperado = 0;
        string identifierEsperado = "";

        // Act
        AirplaneType airplaneType = new AirplaneType();

        // Assert
        Assert.That(airplaneType.TypeId,
            Is.EqualTo(typeIdEsperado));

        Assert.That(airplaneType.Identifier,
            Is.EqualTo(identifierEsperado));

        Assert.That(airplaneType.Description,
            Is.Null);
    }

    [Test]
    public void AirplaneType_Modificar_Propiedades()
    {
        // Arrange
        AirplaneType airplaneType = new AirplaneType();

        int typeIdEsperado = 2;
        string identifierEsperado = "B737-800";
        string descriptionEsperada = "Boeing 737-800";

        // Act
        airplaneType.TypeId = typeIdEsperado;
        airplaneType.Identifier = identifierEsperado;
        airplaneType.Description = descriptionEsperada;

        // Assert
        Assert.That(airplaneType.TypeId,
            Is.EqualTo(typeIdEsperado));

        Assert.That(airplaneType.Identifier,
            Is.EqualTo(identifierEsperado));

        Assert.That(airplaneType.Description,
            Is.EqualTo(descriptionEsperada));
    }
}
