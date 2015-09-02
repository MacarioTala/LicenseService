using System.Collections.Generic;

namespace LicenseService.Repositories
{
    public interface ICompanyRepository
    {
        Company GetCompanyByCompanyName();
        IEnumerable<Company> GetCompanyList();
    }
}
