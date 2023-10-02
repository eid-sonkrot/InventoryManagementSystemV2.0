namespace InventoryManagementSystemV2
{
    public interface IDataAccessFactory
    {
        IProductRepository GetDataAccess();
    }
}
