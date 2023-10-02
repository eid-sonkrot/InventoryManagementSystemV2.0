using System.Text.RegularExpressions;

namespace InventoryManagementSystemV2
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        private string ValidName = @"^[A-Za-z]+$";

        public Product(string name, double price, int quantity)
        {
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
        }
        public Product()
        {
        }
        public void Input()
        {
            //Validating the input to ensure its correctness
            var isValid = false;
            var name = (string)null;

            do
            {
                // Inform the user to enter the product's name
                Console.Write("Enter the product's name: ");
                name = Console.ReadLine();
                if (Regex.IsMatch(name, this.ValidName))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a Name contain Alpapitic character");
                }
            } while (!isValid);
            this.Name = name;
            isValid = false;
            var price = 0.0;

            do
            {
                // Inform the user to enter the product's price
                Console.Write("Enter the product's price: ");
                if (double.TryParse(Console.ReadLine(), out price))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a name containing only alphabetical characters.");
                }

            } while (!isValid);
            this.Price = price;
            var quantity = 0;

            isValid = false;
            do
            {
                // Inform the user to enter the product's quantity
                Console.Write("Enter the product's quantity: ");
                if (int.TryParse(Console.ReadLine(), out quantity))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an intger number.");
                }

            } while (!isValid);
            this.Quantity = quantity;
        }
    }
}