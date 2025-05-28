namespace ShoppingCart
{
    // The class of a product with a name and price
    public class Product(string name, decimal price)
    {
        public string Name { get; set; } = name;
        public decimal Price { get; set; } = price;
    }
}