using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class Product(string name, decimal price)
    {
        public string Name { get; } = name;
        public decimal Price { get; } = price;
    }
}
