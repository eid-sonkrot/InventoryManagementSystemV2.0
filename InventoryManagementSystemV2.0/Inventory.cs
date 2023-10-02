using ConsoleTables;

namespace InventoryManagementSystemV2
{
    public class Inventory
    {
        private List<Product> Products = new List<Product>();
        //add Product to Products List
        public void AddProduct(Product newProduct)
        {
            if (!this.Products.Exists(x => x.Name.Equals(newProduct.Name)))
            {
                this.Products.Add(newProduct);
                Console.WriteLine("The product addition is complete.");
            }
            else
            {
                Console.WriteLine(@"The product you want to add already exists.\n
                     If you want to update the product information,
                     please select that option from the main list.");
            }
            System.Threading.Thread.Sleep(1000);
        }
        //Creat Taple Display All Products in Products List
        // Print the table to the console
        public void DisplayProducts()
        {
            var dataTable = ConsoleTable.From(this.Products);

            dataTable.Write(Format.Alternative);
        }
        public void FindProduct(string name)
        {
            if (this.Products.Find(x => x.Name.Equals(name)) is not null)
            {

                var product = this.Products.Find(x => x.Name.Equals(name));
                var dataTable = ConsoleTable.From(new List<Product> { product });
                // Print the table to the console
                dataTable.Write(Format.Alternative);
            }
            else
            {
                Console.WriteLine(@"The product you want is not exists.
                     If you want to add the product 
                     please select add option from the main list.");
            }
            System.Threading.Thread.Sleep(1000);
        }
        //check if the value is valid or not 
        public bool TryParseValue(string userInput, Type propertyType, out object parsedValue)
        {
            try
            {
                parsedValue = Convert.ChangeType(userInput, propertyType);
                return true;
            }
            catch
            {
                parsedValue = null;
                return false;
            }
        }
        public void EditProduct(string name)
        {
            if (this.Products.Find(x => x.Name.Equals(name)) is not  null)
            {

                var product = this.Products.Find(x => x.Name.Equals(name));
                // Get the type of the Product class using reflection
                var productType = typeof(Product);
                // Get all properties of the Product class
                var properties = productType.GetProperties();
                // Add the property names as table headers
                var Head = properties.Select(x => x.Name).ToArray();

                // loop on each properties 
                for (var i = 0; i < properties.Length; i++)
                {
                    //give user choice to edit the  propertie
                    Console.WriteLine($"if you want edite The {properties[i].Name} Product Tybe 1");
                    var choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        var IsValid = false;
                        object parsedValue;

                        do
                        {
                            // Inform the user to enter the New value of Propareties 
                            Console.Write($"Enter the product's  {properties[i].Name}   : ");
                            //check the valadtion of The user input value 
                            if (TryParseValue(Console.ReadLine(), properties[i].PropertyType, out parsedValue))
                            {
                                IsValid = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter an " + properties[i].PropertyType + " Value.");
                            }
                        } while (!IsValid);
                        properties[i].SetValue(product, parsedValue, null);
                    }
                }
            }
            else
            {
                Console.WriteLine(@"The product you want Edit dose not exists.
                     If you want to add the product 
                     please select add option from the main list.");
                System.Threading.Thread.Sleep(1000);
            }
        }
        public void RemoveProduct(string name)
        {
            if (this.Products.Find(x => x.Name.Equals(name)) is not null)
            {
                var product = this.Products.Find(x => x.Name.Equals(name));
                this.Products.Remove(product);

                Console.WriteLine("The product removal is complete.");
            }
            else
            {
                Console.WriteLine("The product you want Remove dose not exists.");
            }
        }
    }
}