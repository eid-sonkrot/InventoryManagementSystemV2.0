namespace InventoryManagementSystemV2
{
    public interface IProductRepository
    {
        void InsertProduct(Product product);
        void DeleteProduct(string name);
        void UpdateProduct(Product product, string name);
        Product GetProduct(string name);
        List<Product> SelectAllProducts();
    }
}
