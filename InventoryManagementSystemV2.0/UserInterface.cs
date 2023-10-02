using InventoryManagementSystemV2;

namespace InventoryManagementSystemV2
{
    public class UserInterface
    {
        public Inventory inventory = new Inventory();

        static void Main(string[] args)
        {
            var userInterface = new UserInterface();

            while (true)
            {
                Console.WriteLine("Welcome to the Inventory Management System!");
                userInterface.DisplayMenu();
                var choice = userInterface.GetChoiceFromUser();

                userInterface.HandleInventoryAction(choice);
                Console.Clear();
            }
        }
        public void DisplayMenu()
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Add a product");
            Console.WriteLine("2. View all products");
            Console.WriteLine("3. Edit a product");
            Console.WriteLine("4. Delete a product");
            Console.WriteLine("5. Search for a product");
            Console.WriteLine("6. Exit");
        }
        public int GetChoiceFromUser()
        {
            Console.Write("Enter your choice (1-6): ");
            var choice = 0;
            var isValidInput = int.TryParse(Console.ReadLine(), out choice);

            while (!isValidInput || choice < 1 || choice > 6)
            {
                Console.Clear();
                Console.WriteLine("Invalid Input plz chose a number from (1-6)");
                Console.WriteLine("Welcome to the Inventory Management System!");
                DisplayMenu();
                Console.Write("Enter your choice (1-6): ");
                isValidInput = int.TryParse(Console.ReadLine(), out choice);
            }
            return choice;
        }

        public void HandleInventoryAction(int choice)
        {


            switch (choice)
            {
                case 1:
                    //  add a product to the inventory
                    var product = new Product();

                    product.Input();
                    inventory.AddProduct(product);
                    break;
                case 2:
                    // display all products in the inventory
                    inventory.DisplayProducts();
                    Console.WriteLine("Press any key to Back to Main Menu...");
                    Console.ReadKey(); // Waits for the user to press any key
                    break;
                case 3:
                    // edit an existing product in the inventory
                    Console.Write("please Enter the Name of Product you want Edit: ");
                    inventory.EditProduct(Console.ReadLine());
                    break;
                case 4:
                    //  remove a product from the inventory
                    Console.Write("please Enter the Name of Product you want Remove: ");
                    inventory.RemoveProduct(Console.ReadLine());
                    Console.WriteLine("Press any key to Back to Main Menu...");
                    Console.ReadKey(); // Waits for the user to press any key
                    break;
                case 5:
                    //search for a product in the inventory
                    Console.Write("please enter product name : ");
                    inventory.FindProduct(Console.ReadLine());
                    Console.WriteLine("Press any key to Back to Main Menu...");
                    Console.ReadKey(); // Waits for the user to press any key
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Exiting the Inventory Management System. Goodbye!");
                    System.Threading.Thread.Sleep(1000);
                    return;
            }
        }
    }
}