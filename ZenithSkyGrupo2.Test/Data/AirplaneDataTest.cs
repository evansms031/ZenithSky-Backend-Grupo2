
using Microsoft.Extensions.Configuration;
using ZenithSkyGrupo2.Api.Data;

namespace ZenithSkyGrupo2.Test.Data
{
    public class AirplaneDataTest
    {
        private AirplaneData CrearData()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<SqlConnectionFactory>()
                .Build();

            SqlConnectionFactory factory =
                new SqlConnectionFactory(configuration);

            return new AirplaneData(factory);
        }

        //Consultar lista
        [Test]
        public void ObtenerTodos_Test()
        {
            // Arrange
            AirplaneData data = CrearData();

            // Act
            var aviones = data.ObtenerTodos();

            // Assert
            Assert.That(aviones, Is.Not.Null);
        }

        //Encontrar un avión existente con ID 1
        [Test]
        public void ObtenerPorId_Test()
        {
            // Arrange
            AirplaneData data = CrearData();

            // Act
            var avion = data.ObtenerPorId(1);

            // Assert
            Assert.That(avion, Is.Not.Null);
            Assert.That(avion.AirplaneId, Is.EqualTo(1));
        }

        //Devuelve null cuando no existe
        [Test]
        public void ObtenerPorId_NoExistente_Test()
        {
            // Arrange
            AirplaneData data = CrearData();

            // Act
            var avion = data.ObtenerPorId(999999);

            // Assert
            Assert.That(avion, Is.Null);
        }

        //Filtrar exitosamente por tipo

        [Test]
        public void ObtenerPorTipo_Test()
        {
            // Arrange
            AirplaneData data = CrearData();

            // Act
            var aviones = data.ObtenerTodos(typeId: 1);

            // Assert
            Assert.That(aviones, Is.Not.Null);

            foreach (var avion in aviones)
            {
                Assert.That(avion.TypeId, Is.EqualTo(1));
            }
        }

        //Filtrar exitosamente por aerolinea

        [Test]
        public void ObtenerPorAerolinea_Test()
        {
            // Arrange
            AirplaneData data = CrearData();

            // Act
            var aviones = data.ObtenerTodos(airlineId: 1);

            // Assert
            Assert.That(aviones, Is.Not.Null);

            foreach (var avion in aviones)
            {
                Assert.That(avion.AirlineId, Is.EqualTo(1));
            }
        }
    }
}
