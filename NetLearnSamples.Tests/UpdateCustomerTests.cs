using NetLearnSamples;
using System;
using System.Collections.Generic;
using Xunit;

namespace NetLearnSamples.Tests
{
    public class UpdateCustomerTests
    {
        [Fact]
        public void UpdateAge_WithValidAge_UpdatesCustomerAge()
        {
            // Arrange
            var customer = new Customer("John", 30);
            var newAge = 35;

            // Act
            customer.UpdateAge(newAge);

            // Assert
            Assert.Equal(newAge, customer.Age);
        }

        [Fact]
        public void UpdateAge_WithZeroAge_UpdatesCustomerAge()
        {
            // Arrange
            var customer = new Customer("Jane", 25);
            var newAge = 0;

            // Act
            customer.UpdateAge(newAge);

            // Assert
            Assert.Equal(newAge, customer.Age);
        }

        [Fact]
        public void UpdateAge_WithNegativeAge_ThrowsArgumentException()
        {
            // Arrange
            var customer = new Customer("Bob", 40);
            var newAge = -5;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => customer.UpdateAge(newAge));
            Assert.Equal("Age cannot be negative. (Parameter 'newAge')", exception.Message);
        }

        [Fact]
        public void UpdateAge_MultipleUpdates_LastUpdateWins()
        {
            // Arrange
            var customer = new Customer("Alice", 20);

            // Act
            customer.UpdateAge(25);
            customer.UpdateAge(30);
            customer.UpdateAge(35);

            // Assert
            Assert.Equal(35, customer.Age);
        }

        [Fact]
        public void UpdateAges_WithValidAge_UpdatesAllCustomersAge()
        {
            // Arrange
            var customers = new List<Customer?>
            {
                new Customer("John", 30),
                new Customer("Jane", 25),
                new Customer("Bob", 40)
            };
            var newAge = 50;

            // Act
            customers.UpdateAges(newAge);

            // Assert
            Assert.All(customers, c => Assert.Equal(newAge, c?.Age));
        }

        [Fact]
        public void UpdateAges_WithNullElements_SkipsNullCustomers()
        {
            // Arrange
            var customers = new List<Customer?>
            {
                new Customer("John", 30),
                null,
                new Customer("Bob", 40)
            };
            var newAge = 50;

            // Act
            customers.UpdateAges(newAge);

            // Assert
            Assert.Equal(50, customers[0]?.Age);
            Assert.Null(customers[1]);
            Assert.Equal(50, customers[2]?.Age);
        }

        [Fact]
        public void UpdateAges_WithEmptyList_DoesNotThrow()
        {
            // Arrange
            var customers = new List<Customer?>();
            var newAge = 50;

            // Act & Assert (should not throw)
            customers.UpdateAges(newAge);
        }

        [Fact]
        public void UpdateAges_WithAllNullElements_DoesNotThrow()
        {
            // Arrange
            var customers = new List<Customer?> { null, null, null };
            var newAge = 50;

            // Act & Assert (should not throw)
            customers.UpdateAges(newAge);
        }

        [Fact]
        public void UpdateAges_WithNegativeAge_ThrowsArgumentException()
        {
            // Arrange
            var customers = new List<Customer?>
            {
                new Customer("John", 30),
                new Customer("Jane", 25)
            };
            var newAge = -10;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => customers.UpdateAges(newAge));
            Assert.Equal("Age cannot be negative. (Parameter 'newAge')", exception.Message);
        }
    }
}
