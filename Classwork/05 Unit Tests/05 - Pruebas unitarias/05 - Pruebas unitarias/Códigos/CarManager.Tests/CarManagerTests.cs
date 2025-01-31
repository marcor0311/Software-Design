using NUnit.Framework;
using System;

namespace CarManager.Tests
{
    [TestFixture]
    public class CarManagerTests
    {
        private Car car;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Console.WriteLine("OneTimeSetUp - Initializing test suite...");
            // Cualquier inicialización que necesite hacerse una vez antes de todas las pruebas.
        }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUp - Initializing test...");
            // Inicialización necesaria antes de cada prueba.
            car = new Car("Toyota", "Corolla", 6.5, 50);
        }

        [Test]
        public void Constructor_WithValidData_ShouldInitializeCar()
        {
            // Assert
            Assert.AreEqual("Toyota", car.Make);
            Assert.AreEqual("Corolla", car.Model);
            Assert.AreEqual(6.5, car.FuelConsumption);
            Assert.AreEqual(0, car.FuelAmount); // FuelAmount es de solo lectura
            Assert.AreEqual(50, car.FuelCapacity); // FuelCapacity es de solo lectura
        }

        [Test]
        public void Constructor_WithNullOrEmptyMake_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Car(null, "Corolla", 6.5, 50));
            Assert.Throws<ArgumentException>(() => new Car("", "Corolla", 6.5, 50));
        }

        [Test]
        public void Constructor_WithNullOrEmptyModel_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Car("Toyota", null, 6.5, 50));
            Assert.Throws<ArgumentException>(() => new Car("Toyota", "", 6.5, 50));
        }

        [Test]
        public void Constructor_WithZeroOrNegativeFuelConsumption_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Car("Toyota", "Corolla", 0, 50));
            Assert.Throws<ArgumentException>(() => new Car("Toyota", "Corolla", -6.5, 50));
        }

        [Test]
        public void Constructor_WithZeroOrNegativeFuelCapacity_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Car("Toyota", "Corolla", 6.5, 0));
            Assert.Throws<ArgumentException>(() => new Car("Toyota", "Corolla", 6.5, -50));
        }

        [Test]
        public void Refuel_WithValidAmount_ShouldIncreaseFuelAmount()
        {
            // Act
            car.Refuel(20);

            // Assert
            Assert.AreEqual(20, car.FuelAmount);
        }

        [Test]
        public void Refuel_WithNegativeAmount_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => car.Refuel(-20));
        }

        [Test]
        public void Drive_WithEnoughFuel_ShouldDecreaseFuelAmount()
        {
            // Arrange
            car.Refuel(30); // Tanque lleno

            // Act
            car.Drive(200);

            // Assert
            Assert.AreEqual(30 - 13, car.FuelAmount, 0.01); // 200 km a 6.5 l/100km = 13 litros
        }

        [Test]
        public void Drive_WithNotEnoughFuel_ShouldThrowInvalidOperationException()
        {
            // Arrange: No hay suficiente combustible para conducir 200 km
            car.Refuel(10); // solo 10 litros, insuficiente para conducir 200 km

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => car.Drive(200));
        }


        [Test]
        public void Drive_WithZeroDistance_ShouldNotChangeFuelAmount()
        {
            // Arrange
            car.Refuel(30); // Tanque lleno

            // Act
            car.Drive(0);

            // Assert
            Assert.AreEqual(30, car.FuelAmount);
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("TearDown - Cleaning up after test...");
            // Limpieza después de cada prueba.
            car = null;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("OneTimeTearDown - Cleaning up test suite...");
            // Limpieza después de todas las pruebas.
        }
    }
}
