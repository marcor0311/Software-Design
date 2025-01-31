namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Console.WriteLine("OneTimeSetUp - Initializing test suite...");
            // Aquí puedes colocar cualquier inicialización que deba realizarse una vez antes de todas las pruebas.
        }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUp - Initializing test...");
            // Inicialización que se ejecutará antes de cada prueba.
            database = new Database(); // Reinicia la base de datos antes de cada prueba.
        }

        [Test]
        public void Constructor_WithValidData_ShouldInitializeDatabase()
        {
            // Arrange
            int[] testData = { 1, 2, 3, 4, 5 };

            // Act
            database = new Database(testData);

            // Assert
            Assert.AreEqual(testData.Length, database.Count);
        }

        [Test]
        public void Constructor_WithTooManyElements_ShouldThrowException()
        {
            // Arrange
            int[] testData = new int[17]; // Array of 17 elements

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new Database(testData));
        }

        [Test]
        public void Add_WithValidElement_ShouldAddToDatabase()
        {
            // Arrange
            database = new Database(1, 2, 3);

            // Act
            database.Add(4);

            // Assert
            Assert.AreEqual(4, database.Count);
        }

        [Test]
        public void Add_WithFullDatabase_ShouldThrowException()
        {
            // Arrange
            database = new Database(
                1, 2, 3, 4, 5, 6, 7, 8,
                9, 10, 11, 12, 13, 14, 15, 16); // Array of 16 elements

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => database.Add(17));
        }

        [Test]
        public void Remove_WithElements_ShouldRemoveLastElement()
        {
            // Arrange
            database = new Database(1, 2, 3);

            // Act
            database.Remove();

            // Assert
            Assert.AreEqual(2, database.Count);
        }

        [Test]
        public void Remove_FromEmptyDatabase_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void Fetch_ShouldReturnArrayWithAllElements()
        {
            // Arrange
            int[] testData = { 1, 2, 3, 4, 5 };
            database = new Database(testData);

            // Act
            int[] fetchedData = database.Fetch();

            // Assert
            CollectionAssert.AreEqual(testData, fetchedData);
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("TearDown - Cleaning up after test...");
            // Cualquier limpieza necesaria después de cada prueba.
            database = null;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("OneTimeTearDown - Cleaning up test suite...");
            // Cualquier limpieza necesaria después de todas las pruebas.
        }
    }
}
