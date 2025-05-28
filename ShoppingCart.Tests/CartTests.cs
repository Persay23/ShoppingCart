using Xunit;

namespace ShoppingCart.Tests
{
    public class CartTests
    {
        [Fact]
        // This test checks if a product can be added to the cart and the total amount is correct.
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
        // This test checks if products can be added to the cart and the amount is correct.
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
        // This test checks if a product can be removed from the cart and the amount is correct.
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
        // This test checks if removing a product by index that does not exist returns false.
        public void RemoveProduct_InvalidIndex_ShouldReturnFalse()
        {
            // Arrange
            var cart = new Cart();

            // Act & Assert
            Assert.False(cart.RemoveProduct(5));
        }

        [Fact]
        // This test checks if the cheapest product in the cart is returned correctly.
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
        // This test checks if the cheapest product in an empty cart returns zero.
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
            // This test checks if the amount of products in the cart is within a budget.
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
            // This test checks if the amount of products in the cart exceeds a budget.
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
