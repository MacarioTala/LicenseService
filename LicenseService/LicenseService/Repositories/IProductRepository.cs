using LicenseService.Entities;

namespace LicenseService.Repositories
{
    public interface IProductRepository
    {
        Product GetProductByName(string productName);
    }
}
