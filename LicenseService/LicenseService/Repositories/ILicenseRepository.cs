using System.Collections.Generic;
using LicenseService.Entities;

namespace LicenseService
{
    public interface ILicenseRepository
    {
        License GetLicenseByLicenseKey(string licensekey);

        void CreateLicense(License licenseToCreate);

        void UpdateLicense(License licenseToUpdate);
        List<License> GetLicensesByLicenseKeys(List<string> licenseKeyList);
    }
}
