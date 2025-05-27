using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartIntern;

namespace ShoppingCartIntern
{
    static class Program
    {
        static void Main()
        {
            Cart cart = new();

            List<Product> availableProducts = AddProducts();

            Console.WriteLine("Available products:");
            Cart.ShowProducts(availableProducts);


            Console.WriteLine("\nEnter product names you want to add to cart (type 'checkout' to go to checkout):");

            while (true)
            {
                Console.Write("\nProduct name: ");
                string input = Console.ReadLine().Trim();

                if (input.Equals("checkout", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("Shopping completed. Here are the items in your cart: ");
                    break;
                }

                var selectedProduct = availableProducts
                    .FirstOrDefault(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

                if (selectedProduct != null)
                {
                    cart.AddProduct(selectedProduct.Name, selectedProduct.Price);
                    Console.WriteLine($"\n{selectedProduct.Name} added to cart.");
                    cart.ShowAllProducts();
                    Console.WriteLine($"Total amount: {cart.GetTotalAmount()}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }


            Console.Write("\nEnter your budget: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal budget))
            {
                if (cart.GetTotalAmount() <= budget) Console.WriteLine("Your shopping cart is within your budget. Purchase success");

                else Console.WriteLine("Your budget is too small for this cart. Purchase failed");
            }
            else Console.WriteLine("Please enter valid budget.");

            decimal cheapest = cart.CheapestProduct();

            if (budget < cheapest) Console.WriteLine($"\nYour budget ({budget}) is lower than the cheapest item in your cart ({cheapest}). " +
                $"You cannot afford any item. Purchase cancelled.");


            while (cart.GetTotalAmount() > budget && cart.NotEmpty())
            {
                Console.WriteLine($"\nYour total ({cart.GetTotalAmount()}) exceeds your budget ({budget}). " +
                    $"Would you like to remove items from your cart? (yes/no)");

                string choice = Console.ReadLine().Trim();
                if (!choice.Equals("yes", StringComparison.CurrentCultureIgnoreCase)) break;

                cart.ShowAllProducts();
                Console.Write("Enter the index of the item to remove: ");
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    if (cart.RemoveProduct(index - 1))
                    {
                        Console.WriteLine($"Item removed.");
                        cart.ShowAllProducts();
                    }
                    else Console.WriteLine("Invalid index.");
                }
                else Console.WriteLine("Please enter a valid number.");
            }
            Console.WriteLine("Your shopping cart is within your budget. Purchase success");
            Console.ReadLine();
        }

        static List<Product> AddProducts()
        {
            return
            [
                new Product("Apple", 1.20m),
                new Product("Banana", 0.80m),
                new Product("Orange", 1.00m),
                new Product("Mango", 1.50m),
                new Product("Pineapple", 2.50m)
            ];
        }
    }
}

