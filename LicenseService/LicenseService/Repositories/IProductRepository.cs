namespace LicenseService
{
    public interface IProductRepository
    {
        Product GetProductByName(string productName);
    }
}
