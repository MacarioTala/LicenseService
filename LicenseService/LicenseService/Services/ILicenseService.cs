using System;
using System.Collections.Generic;
using LicenseService.Entities;

namespace LicenseService.Services
{
    public interface ILicenseService
    {
        void AcceptEulaByUserId();
        bool HasUserAcceptedEula(Guid userId);
        void DeactivateLicense(int licenseId);
        void RemoveUserFromLicense(License license, Guid userId);

        void AddUserToLicense(int licenseId, Guid userId)//TODO: was InsertLicenseUser
            ;

        void UpdateLicenseCompany(Company company, string licenseKey);

        /// <summary>
        /// This is the interface that NAV requires. This is the only API accessible externally.
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="lineNumber"></param>
        /// <param name="action"></param>
        /// <param name="validateOnly"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        LicenseMessage[] LicenseImport(string orderNumber, int lineNumber, int action, bool validateOnly,
            List<ProductOrder> products);

        LicenseMessage[] LicenseImport(NavOrder navOrder);
    }
}