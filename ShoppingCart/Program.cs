namespace ShoppingCart
{
    static class Program
    {
        static void Main()
        {
            Cart cart = new();
            List<Product> availableProducts = GetAvailableProducts();

            // Display available products
            Console.WriteLine("Available products:");
            Cart.ShowProducts(availableProducts);

            UserCart(cart, availableProducts);
            decimal budget = GetBudget();

            // Check if the total amount of the cart is within the budget
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

        // Method that take user inputs and adds products to the cart
        static void UserCart(Cart cart, List<Product> products)
        {
            Console.WriteLine("\nEnter product names to add to the cart (type 'checkout' to finish):");

            // Loop to get user input for product names
            while (true)
            {
                Console.Write("\nProduct name: ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input) || input.Equals("checkout", StringComparison.OrdinalIgnoreCase)) break;

                // Check if the input is a valid product name
                var selected = products.FirstOrDefault(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

                // If product is found, add it to the cart
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

        // Method to get the specific budget from the user
        static decimal GetBudget()
        {
            decimal budget;
            Console.Write("\nEnter your budget: ");

            // Loop to ensure that budget is a valid
            while (!decimal.TryParse(Console.ReadLine(), out budget) || budget < 0)
            {
                Console.Write("Invalid input. Enter valid budget: ");
            }
            return budget;
        }

        // Method to check if the budget is valid, not zero, and if the cart total is lower than the budget remove items if necessary
        static bool BudgetCheck(Cart cart, decimal budget)
        {
            decimal total = cart.GetTotalAmount();

            // Check if the cart is within set budget
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

        // Method to remove items from the cart if the total amount exceeds the budget
        static bool RemoveItems(Cart cart, decimal budget)
        {

            while (cart.GetTotalAmount() > budget && cart.NotEmpty())
            {
                Console.WriteLine($"\nYour total - {cart.GetTotalAmount()}, exceeds your budget - {budget}.");
                Console.Write("Would you like to remove items? (yes/no): ");
                string? input = Console.ReadLine()?.Trim();

                // Check if the user wants to remove items
                if (string.Equals(input, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    // Show the products in the cart and ask for an index to remove
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

        // Method to get a list of available products
        static List<Product> GetAvailableProducts() =>
        [
            new Product("Apple", 1.20m),
            new Product("Banana", 0.80m),
            new Product("Orange", 2.00m),
            new Product("Mango", 3.50m),
            new Product("Lemon", 0.40m),
            new Product("Pear", 1.10m),
            new Product("Plum", 1.30m),
            new Product("Kiwi", 4.00m),
        ];
    }
}