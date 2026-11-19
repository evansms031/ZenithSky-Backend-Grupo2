
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Test;

public class AirplaneTest
{

    //Verifica que se guarden los datos del avión

    [Test]
    public void Airplane_Constructor_Con_PropiedadesInicializadas()
    {
        // Arrange
        int airplaneIdEsperado = 1;
        int capacityEsperada = 186;
        int typeIdEsperado = 2;
        int airlineIdEsperado = 1;
        string identifierEsperado = "B737-800";
        string descriptionEsperada = "Boeing 737-800";
        string airlineNameEsperado = "ZenithSky Airlines";

        // Act
        Airplane avion = new Airplane(
            airplaneIdEsperado,
            capacityEsperada,
            typeIdEsperado,
            airlineIdEsperado,
            identifierEsperado,
            descriptionEsperada,
            airlineNameEsperado
        );

        // Assert
        Assert.That(avion.AirplaneId, Is.EqualTo(airplaneIdEsperado));
        Assert.That(avion.Capacity, Is.EqualTo(capacityEsperada));
        Assert.That(avion.TypeId, Is.EqualTo(typeIdEsperado));
        Assert.That(avion.AirlineId, Is.EqualTo(airlineIdEsperado));
        Assert.That(avion.Identifier, Is.EqualTo(identifierEsperado));
        Assert.That(avion.Description, Is.EqualTo(descriptionEsperada));
        Assert.That(avion.AirlineName, Is.EqualTo(airlineNameEsperado));
    }

    //Verificar propiedades iniciales de un airplane

    [Test]
    public void Airplane_Constructor_Vacio()
    {
        // Arrange

        // Act
        Airplane avion = new Airplane();

        // Assert
        Assert.That(avion.AirplaneId, Is.EqualTo(0));
        Assert.That(avion.Capacity, Is.EqualTo(0));
        Assert.That(avion.TypeId, Is.EqualTo(0));
        Assert.That(avion.AirlineId, Is.EqualTo(0));
        Assert.That(avion.Identifier, Is.EqualTo(""));
        Assert.That(avion.Description, Is.Null);
        Assert.That(avion.AirlineName, Is.EqualTo(""));
    }

    //Probar el set

    [Test]
    public void Airplane_Modificar_Propiedades()
    {
        // Arrange
        Airplane avion = new Airplane();

        // Act
        avion.AirplaneId = 2;
        avion.Capacity = 200;
        avion.TypeId = 1;
        avion.AirlineId = 2;
        avion.Identifier = "A320";
        avion.Description = "Airbus A320";
        avion.AirlineName = "Avianca";

        // Assert
        Assert.That(avion.AirplaneId, Is.EqualTo(2));
        Assert.That(avion.Capacity, Is.EqualTo(200));
        Assert.That(avion.TypeId, Is.EqualTo(1));
        Assert.That(avion.AirlineId, Is.EqualTo(2));
        Assert.That(avion.Identifier, Is.EqualTo("A320"));
        Assert.That(avion.Description, Is.EqualTo("Airbus A320"));
        Assert.That(avion.AirlineName, Is.EqualTo("Avianca"));
    }
}
