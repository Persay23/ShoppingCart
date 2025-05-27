using ShoppingCartIntern;
using Xunit;

namespace ShoppingCartIntern.Tests
{
    public class CartTests
    {
        [Fact]
        public void AddProduct_ShouldAddToCart()
        {
            var cart = new Cart();
            cart.AddProduct("Banana", 0.80m);

            Assert.Equal(0.80m, cart.GetTotalAmount());
        }

        [Fact]
        public void GetTotalAmount_ShouldSumAllProductsCorrectly()
        {
            var cart = new Cart();
            cart.AddProduct("Apple", 1.20m);
            cart.AddProduct("Orange", 1.00m);

            var total = cart.GetTotalAmount();

            Assert.Equal(2.20m, total);
        }

        [Fact]
        public void RemoveProductAt_ValidIndex_ShouldRemoveProduct()
        {
            var cart = new Cart();
            cart.AddProduct("Mango", 1.50m);
            cart.AddProduct("Pineapple", 2.50m);

            bool result = cart.RemoveProduct(0);

            Assert.True(result);
            Assert.Equal(2.50m, cart.GetTotalAmount());
        }

        [Fact]
        public void RemoveProductAt_InvalidIndex_ShouldReturnFalse()
        {
            var cart = new Cart();

            bool result = cart.RemoveProduct(5);

            Assert.False(result);
        }

        [Fact]
        public void GetCheapestItemPrice_ShouldReturnCorrectValue()
        {
            var cart = new Cart();
            cart.AddProduct("A", 5.00m);
            cart.AddProduct("B", 3.00m);
            cart.AddProduct("C", 4.00m);

            var cheapest = cart.CheapestProduct();

            Assert.Equal(3.00m, cheapest);
        }

        [Fact]
        public void GetCheapestItemPrice_EmptyCart_ShouldReturnZero()
        {
            var cart = new Cart();

            var cheapest = cart.CheapestProduct();

            Assert.Equal(0m, cheapest);
        }

        public class BudgetLogicTests
        {
            [Fact]
            public void CartTotalWithinBudget_ReturnsTrue()
            {
                Cart cart = new();
                cart.AddProduct("Apple", 1.20m);
                cart.AddProduct("Banana", 0.80m);
                decimal budget = 5.00m;

                decimal total = cart.GetTotalAmount();

                Assert.True(total <= budget);
            }

            [Fact]
            public void CartTotalExceedsBudget_ReturnsFalse()
            {
                Cart cart = new();
                cart.AddProduct("Pineapple", 2.50m);
                cart.AddProduct("Mango", 1.50m);
                cart.AddProduct("Orange", 1.00m);
                decimal budget = 4.00m;

                decimal total = cart.GetTotalAmount();

                Assert.False(total <= budget);
            }
        }
    }
}
