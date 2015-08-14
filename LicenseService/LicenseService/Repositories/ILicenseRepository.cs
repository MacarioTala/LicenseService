namespace LicenseService
{
    public interface ILicenseRepository
    {
        License GetLicenseByLicenseKey(string licensekey);

    }
}
