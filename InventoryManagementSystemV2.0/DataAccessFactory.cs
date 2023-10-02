using System.Configuration;

namespace InventoryManagementSystemV2
{
    public class DataAccessFactory : IDataAccessFactory
    {
        public IProductRepository GetDataAccess()
        {
            var databaseType = ConfigurationManager.AppSettings["DatabaseType"];

            return databaseType switch
            {
                "SQL" => new ProductSql(),
                "MongoDb" => new ProductMongoDb(),
                _ => throw new ArgumentException("Invalid database type: " + databaseType)
            };
        }
    }
}