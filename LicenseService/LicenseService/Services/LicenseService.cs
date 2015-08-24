using System;
using System.Collections.Generic;
using System.Linq;
using LicenseService.Entities;

namespace LicenseService.Services
{
    public class LicenseService
    {
        public void AcceptEulaByUserId()
        {
            throw new NotImplementedException();
        }

        public bool EulaAcceptedByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void DeactivateLicense(int licenseId)
        {
           throw new NotImplementedException();
        }

        public void RemoveUserFromLicense(License license, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void AddUserToLicense(int licenseId, Guid userId)//TODO: was InsertLicenseUser
        {
         throw new NotImplementedException();
        }

        public void UpdateLicenseCompany(Company company, string licenseKey)
        {
            //Get the license associated with the license key and assign it to the company.  
           throw new NotImplementedException();
        }

        public LicenseMessage[] LicenseImport(string orderNumber, int lineNumber, int action, bool validateOnly,
          List<ProductOrder> products)
        {
           throw new NotImplementedException();
        }

        public LicenseMessage[] LicenseImport(NavOrder navOrder)
        {
            throw new NotImplementedException();
        }

        private static void AssignDefaultLicenseKeys(IEnumerable<ProductOrder> products)
        {
            foreach (var product in products.Where(product => string.IsNullOrEmpty(product.LicenseKey)))
            {
                product.LicenseKey = Guid.NewGuid().ToString();
            }
        }

      
    }
}

