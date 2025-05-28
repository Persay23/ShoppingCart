namespace ShoppingCart
{
    // The class of a shopping cart that holds products
    public class Cart
    {
        private readonly List<Product> products;

        public Cart() => products = [];
        public void AddProduct(string name, decimal price) =>
            products.Add(new Product(name, price));

        // Method to get the total amount of products in the cart
        public decimal GetTotalAmount() => products.Sum(p => p.Price);

        // Method to display the products in the cart or available products depending on the context
        public static void ShowProducts(List<Product> products)
        {
            Console.WriteLine("Items:");
            int index = 1;
            foreach (var product in products)
            {
                Console.WriteLine($"{index}. {product.Name}: {product.Price}");
                index++;
            }
        }

        // Method to get the list of products in the cart
        public List<Product> GetSelectedProducts() => products;

        // Method to remove a product from the cart by index
        public bool RemoveProduct(int index)
        {
            if (index >= 0 && index < products.Count)
            {
                products.RemoveAt(index);
                return true;
            }
            return false;
        }

        // Method to check if the cart is`nt empty
        public bool NotEmpty() => products.Count != 0;

        // Method to get the cheapest product in the cart
        public decimal CheapestProduct()
        {
            return products.Count > 0 ? products.Min(p => p.Price) : 0m;
        }
    }
}
