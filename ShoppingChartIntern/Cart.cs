namespace ShoppingCart
{
    public class Cart
    {
        private readonly List<Product> products;

        public Cart() => products = [];
        public void AddProduct(string name, decimal price) =>
            products.Add(new Product(name, price));

        public decimal GetTotalAmount() => products.Sum(p => p.Price);

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

        public List<Product> GetSelectedProducts() => products;

        public bool RemoveProduct(int index)
        {
            if (index >= 0 && index < products.Count)
            {
                products.RemoveAt(index);
                return true;
            }
            return false;
        }

        public bool NotEmpty() => products.Count != 0;

        public decimal CheapestProduct()
        {
            return products.Count > 0 ? products.Min(p => p.Price) : 0m;
        }
    }
}
