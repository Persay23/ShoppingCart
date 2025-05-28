using Xunit;

namespace ShoppingCart.Tests
{
    public class CartTests
    {
        [Fact]
        public void AddProduct_ShouldAddToCart()
        {
            // Arrange
            var cart = new Cart();

            // Act
            cart.AddProduct("Banana", 0.80m);

            // Assert
            Assert.Equal(0.80m, cart.GetTotalAmount());
        }

        [Fact]
        public void GetTotalAmount_ShouldSumAllProducts()
        {
            // Arrange
            var cart = new Cart();

            cart.AddProduct("Apple", 1.20m);
            cart.AddProduct("Orange", 1.00m);

            // Act
            var total = cart.GetTotalAmount();

            // Assert
            Assert.Equal(2.20m, total);
        }

        [Fact]
        public void RemoveProduct_ShouldRemoveProduct()
        {
            // Arrange
            var cart = new Cart();
            cart.AddProduct("Mango", 1.50m);
            cart.AddProduct("Pineapple", 2.50m);

            // Act
            bool result = cart.RemoveProduct(0);

            // Assert
            Assert.True(result);
            Assert.Equal(2.50m, cart.GetTotalAmount());
        }

        [Fact]
        public void RemoveProduct_InvalidIndex_ShouldReturnFalse()
        {
            // Arrange
            var cart = new Cart();

            // Act & Assert
            Assert.False(cart.RemoveProduct(5));
        }

        [Fact]
        public void CheapestProduct_ShouldReturnCorrectValue()
        {
            // Arrange
            var cart = new Cart();
            cart.AddProduct("A", 5.00m);
            cart.AddProduct("B", 3.00m);
            cart.AddProduct("C", 4.00m);

            // Act
            var cheapest = cart.CheapestProduct();

            // Assert
            Assert.Equal(3.00m, cheapest);
        }

        [Fact]
        public void CheapestProduct_EmptyCart_ShouldReturnZero()
        {
            // Arrange
            var cart = new Cart();

            // Act
            var cheapest = cart.CheapestProduct();

            // Assert
            Assert.Equal(0m, cheapest);
        }

        public class BudgetLogicTests
        {
            [Fact]
            public void BudgetCheck_ReturnsTrue()
            {
                // Arrange
                Cart cart = new();
                cart.AddProduct("Apple", 1.20m);
                cart.AddProduct("Banana", 0.80m);
                decimal budget = 5.00m;

                // Act
                decimal total = cart.GetTotalAmount();

                // Assert
                Assert.True(total <= budget);
            }

            [Fact]
            public void BudgetCheck_TooLow_ShouldReturnsFalse()
            {
                // Arrange
                Cart cart = new();
                cart.AddProduct("Pineapple", 2.50m);
                cart.AddProduct("Mango", 1.50m);
                cart.AddProduct("Orange", 1.00m);
                decimal budget = 4.00m;

                // Act
                decimal total = cart.GetTotalAmount();

                // Assert
                Assert.False(total <= budget);
            }
        }
    }
}
