using NUnit.Framework;
using System;

namespace ExtendedDatabase.Tests
{
    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;

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
            // Cualquier inicialización que necesite hacerse antes de cada prueba.
            var persons = new[]
            {
                new Person(1, "user1"),
                new Person(2, "user2"),
                new Person(3, "user3")
            };
            database = new Database(persons);
        }

        [Test]
        public void Constructor_WithValidData_ShouldInitializeDatabase()
        {
            // Act
            var count = database.Count;

            // Assert
            Assert.AreEqual(3, count);
        }

        [Test]
        public void Constructor_WithTooManyElements_ShouldThrowArgumentException()
        {
            // Arrange
            var tooManyPersons = new Person[17];

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Database(tooManyPersons));
        }

        [Test]
        public void Add_WithValidPerson_ShouldAddToDatabase()
        {
            // Arrange
            var newPerson = new Person(4, "user4");

            // Act
            database.Add(newPerson);

            // Assert
            Assert.AreEqual(4, database.Count);
        }

        [Test]
        public void Add_WithDuplicateUsername_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var duplicateUsername = new Person(4, "user1"); // Already exists

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => database.Add(duplicateUsername));
        }

        [Test]
        public void Add_WithDuplicateId_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var duplicateId = new Person(1, "userX"); // Already exists

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => database.Add(duplicateId));
        }

        [Test]
        public void Remove_WithElements_ShouldRemoveLastElement()
        {
            // Act
            database.Remove();

            // Assert
            Assert.AreEqual(2, database.Count);
        }

        [Test]
        public void Remove_FromEmptyDatabase_ShouldThrowInvalidOperationException()
        {
            // Arrange
            database = new Database(); // Base de datos vacía

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void FindByUsername_WithExistingUsername_ShouldReturnPerson()
        {
            // Act
            var user = database.FindByUsername("user1");

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual("user1", user.UserName);
        }

        [Test]
        public void FindByUsername_WithNonExistingUsername_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("nonexisting"));
        }

        [Test]
        public void FindByUsername_WithNullUsername_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(null));
        }

        [Test]
        public void FindById_WithExistingId_ShouldReturnPerson()
        {
            // Act
            var user = database.FindById(1);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(1, user.Id);
        }

        [Test]
        public void FindById_WithNonExistingId_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => database.FindById(100));
        }

        [Test]
        public void FindById_WithNegativeId_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1));
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
