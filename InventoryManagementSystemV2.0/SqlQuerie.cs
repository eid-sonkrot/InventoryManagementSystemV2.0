namespace InventoryManagementSystemV2
{
    public static class SqlQuerie
    {
        public const string CreateDatabase = "CREATE DATABASE {0}";
        public const string CreateProductsTable = @"
                                                     USE {0};
                                                     CREATE TABLE Products
                                                     (
                                                       Name NVARCHAR(255),
                                                       Price FLOAT,
                                                       Quantity INT
                                                     );";
        public const  string DeleteProduct = "DELETE FROM Products WHERE Name = @Name";
        public const string SelectProduct = "SELECT * FROM Products WHERE Name = @Name";
        public const string InsertProduct = "INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";
        public const string UpdateProduct = "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Name = @currentName";
        public const string SelectAllProducts = "SELECT * FROM Products";
    }
}
