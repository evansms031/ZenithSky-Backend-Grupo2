
using System;
using Microsoft.Extensions.Configuration;
using ZenithSkyGrupo2.Api.Data;
using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Domain;

namespace ZenithSkyGrupo2.Test.Business
{
    public class AirplaneServiceTest
    {
        private AirplaneService CrearService()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<SqlConnectionFactory>()
                .Build();

            SqlConnectionFactory factory =
                new SqlConnectionFactory(configuration);

            AirplaneData data = new AirplaneData(factory);

            return new AirplaneService(data);
        }

        [Test]
        public void Insertar_CapacidadInvalida_Test()
        {
            // Arrange
            AirplaneService service = CrearService();

            Airplane avion = new Airplane(
                0, -50, 1, 1, "A320", "Airbus A320", "Avianca"
            );

            // Act y Assert
            var excepcion = Assert.Throws<ArgumentException>(
                () => service.Insertar(avion)
            );

            Assert.That(excepcion.Message,
                Is.EqualTo("La capacidad debe ser mayor que cero."));
        }

        [Test]
        public void Insertar_TipoInexistente_Test()
        {
            // Arrange
            AirplaneService service = CrearService();

            Airplane avion = new Airplane(
                0, 200, 999999, 1, "A320", "Airbus A320", "Avianca"
            );

            // Act y Assert
            var excepcion = Assert.Throws<ArgumentException>(
                () => service.Insertar(avion)
            );

            Assert.That(excepcion.Message,
                Is.EqualTo("El tipo de avión no existe."));
        }

        [Test]
        public void Insertar_AerolineaInexistente_Test()
        {
            // Arrange
            AirplaneService service = CrearService();

            Airplane avion = new Airplane(
                0, 200, 1, 999999, "A320", "Airbus A320", "Avianca"
            );

            // Act y Assert
            var excepcion = Assert.Throws<ArgumentException>(
                () => service.Insertar(avion)
            );

            Assert.That(excepcion.Message,
                Is.EqualTo("La aerolínea no existe."));
        }

        [Test]
        public void Actualizar_CapacidadInvalida_Test()
        {
            // Arrange
            AirplaneService service = CrearService();

            Airplane avion = new Airplane(
                1, 0, 1, 1, "A320", "Airbus A320", "Avianca"
            );

            // Act y Assert
            var excepcion = Assert.Throws<ArgumentException>(
                () => service.Actualizar(avion)
            );

            Assert.That(excepcion.Message,
                Is.EqualTo("La capacidad debe ser mayor que cero."));
        }
    }
}
