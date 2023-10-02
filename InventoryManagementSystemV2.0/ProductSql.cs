using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace InventoryManagementSystemV2
{
    public class ProductSql : IProductRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        public ProductSql()
        {
            if (!DatabaseExists())
                CreateDatabase();
        }
        public bool DatabaseExists()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }
        public void CreateDatabase()
        {
            var databaseName = "Products";
            var createDatabase = string.Format(SqlQuerie.CreateDatabase, databaseName);
            var createTable = string.Format(SqlQuerie.CreateProductsTable, databaseName);

            connectionString = connectionString.Split(new String[] { "Database=" }, System.StringSplitOptions.None).First();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(createDatabase, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error creating database: " + ex.Message);
                        return; // Exit method if database creation fails
                    }
                }
                using (var command = new SqlCommand(createTable, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error creating table: " + ex.Message);
                    }
                }
            }
        }
        public void DeleteProduct(string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(SqlQuerie.DeleteProduct, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        public Product GetProduct(string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(SqlQuerie.SelectProduct, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Product
                                {
                                    Name = reader["Name"].ToString(),
                                    Price = Convert.ToDouble(reader["Price"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"])
                                };
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return null; // Product not found
        }

        public void InsertProduct(Product product)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(SqlQuerie.InsertProduct, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        public void UpdateProduct(Product product, string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(SqlQuerie.UpdateProduct, connection))
                {
                    command.Parameters.AddWithValue("@CurrentName", name);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public List<Product> SelectAllProducts()
        {

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(SqlQuerie.SelectAllProducts, connection))
                {
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            return Enumerable.Cast<IDataRecord>(reader)
                           .Select(p => new Product
                           {
                               Name = p["Name"].ToString(),
                               Price = Convert.ToDouble(p["Price"]),
                               Quantity = Convert.ToInt32(p["Quantity"])
                           })
                           .ToList();
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return null;
                    }
                }
            }
        }
    }
}