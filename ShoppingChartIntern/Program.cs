namespace ShoppingCart
{
    static class Program
    {
        static void Main()
        {
            Cart cart = new();
            List<Product> availableProducts = GetAvailableProducts();

            Console.WriteLine("Available products:");
            Cart.ShowProducts(availableProducts);

            GetSelectedProducts(cart, availableProducts);
            decimal budget = GetBudget();

            if (!BudgetCheck(cart, budget))
            {
                Console.WriteLine("Purchase failed.");
            }
            else
            {
                Console.WriteLine("Purchase successful.");
            }

            Console.ReadLine();
        }

        static void GetSelectedProducts(Cart cart, List<Product> products)
        {
            Console.WriteLine("\nEnter product names to add to the cart (type 'checkout' to finish):");

            while (true)
            {
                Console.Write("\nProduct name: ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input) || input.Equals("checkout", StringComparison.OrdinalIgnoreCase)) break;

                var selected = products.FirstOrDefault(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
                if (selected != null)
                {
                    cart.AddProduct(selected.Name, selected.Price);
                    Console.WriteLine($"\n{selected.Name} added to cart.");
                    Cart.ShowProducts(cart.GetSelectedProducts());
                    Console.WriteLine($"Total amount: {cart.GetTotalAmount()}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }

            Console.WriteLine("\nShopping completed. Here are the items in your cart:");
            Cart.ShowProducts(cart.GetSelectedProducts());
        }

        static decimal GetBudget()
        {
            decimal budget;
            Console.Write("\nEnter your budget: ");
            while (!decimal.TryParse(Console.ReadLine(), out budget) || budget < 0)
            {
                Console.Write("Invalid input. Enter valid budget: ");
            }
            return budget;
        }

        static bool BudgetCheck(Cart cart, decimal budget)
        {
            decimal total = cart.GetTotalAmount();

            if (budget == 0)
            {
                Console.WriteLine("Your budget is zero. Purchase failed");
                return false;
            }
            else if (budget >= total)
            {
                Console.WriteLine("Your shopping cart is within your budget.");
                return true;
            }

            Console.WriteLine("Your budget is too small for this cart.");
            return RemoveItems(cart, budget);
        }

        static bool RemoveItems(Cart cart, decimal budget)
        {
            while (cart.GetTotalAmount() > budget && cart.NotEmpty())
            {
                Console.WriteLine($"\nYour total - {cart.GetTotalAmount()}, exceeds your budget - {budget}.");
                Console.Write("Would you like to remove items? (yes/no): ");
                string? input = Console.ReadLine()?.Trim();

                if (string.Equals(input, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    Cart.ShowProducts(cart.GetSelectedProducts());
                    Console.Write("Enter the index of the item to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && cart.RemoveProduct(index - 1))
                    {
                        Console.WriteLine("Item removed.");
                        Cart.ShowProducts(cart.GetSelectedProducts());
                    }
                    else
                    {
                        Console.WriteLine("Invalid input or index.");
                    }
                }
                else
                {
                    return false;
                }
            }
            return cart.GetTotalAmount() <= budget;
        }

        static List<Product> GetAvailableProducts() =>
        [
            new Product("Apple", 1.20m),
            new Product("Banana", 0.80m),
            new Product("Orange", 1.00m),
            new Product("Mango", 1.50m),
            new Product("Pineapple", 2.50m)
        ];
    }
}