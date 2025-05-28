using Xunit;

namespace ShoppingCart.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_ShouldStoreNameAndPriceCorrectly()
        {
            // Arrange & Act
            var product = new Product ("Apple", 1.20m);

            // Assert
            Assert.Equal("Apple", product.Name);
            Assert.Equal(1.20m, product.Price);
        }
    }
}