using MongoDB.Driver;
using System.Configuration;

namespace InventoryManagementSystemV2
{
    public class ProductMongoDb : IProductRepository
    {
        private readonly IMongoCollection<Product> collection;
        private string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
        private string databaseName = "Products";
        private string collectionName = "Products";

        public ProductMongoDb()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            collection = database.GetCollection<Product>(collectionName);
        }
        public void DeleteProduct(string name)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(p => p.Name, name);

                collection.DeleteOne(filter);
            }
            catch (MongoException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public Product GetProduct(string name)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(p => p.Name, name);

                return collection.Find(filter).First();
            }
            catch (MongoException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public void InsertProduct(Product product)
        {
            try
            {
                collection.InsertOne(product);
            }
            catch (MongoBulkWriteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public List<Product> SelectAllProducts()
        {
            try
            {
                var productList = collection.Find(p => p.Name == p.Name).ToList();

                return productList;
            }
            catch (MongoException ex)
            {
                Console.WriteLine("MongoDB Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            return new List<Product>();
        }
        public void UpdateProduct(Product product, string name)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(p => p.Name, name);

                collection.UpdateOne(filter, Builders<Product>.Update.Set(p => p, product));
            }
            catch (MongoException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
