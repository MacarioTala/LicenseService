using System;
using System.Collections.Generic;
using System.Linq;
using LicenseService.Constants;
using LicenseService.Entities;
using LicenseService.Enums;
using LicenseService.Extensions;
using LicenseService.Repositories;

namespace LicenseService.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;

        public LicenseService(ILicenseRepository licenseRepository, ICompanyRepository companyRepository, IProductRepository productRepository)
        {
            _licenseRepository = licenseRepository;
            _companyRepository = companyRepository;
            _productRepository = productRepository;
        }

        public void AcceptEulaByUserId()
        {
            throw new NotImplementedException();
        }

        public bool HasUserAcceptedEula(Guid userId)
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

        /// <summary>
        /// This is the interface that NAV requires. This is the only API accessible externally.
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="lineNumber"></param>
        /// <param name="action"></param>
        /// <param name="validateOnly"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public LicenseMessage[] LicenseImport(string orderNumber, int lineNumber, int action, bool validateOnly,
          List<ProductOrder> products)
        {
            return
                LicenseImport(new NavOrder
                {
                    Action = action,
                    OrderNumber = orderNumber,
                    LineNumber = lineNumber,
                    ProductOrders = products
                });
        }

        public LicenseMessage[] LicenseImport(NavOrder navOrder)
        {
            var licenseKeyList = navOrder.ProductOrders.Select(productOrder => productOrder.LicenseKey).ToList();
            var licenseList = _licenseRepository.GetLicensesByLicenseKeys(licenseKeyList);

            var validationMessages = navOrder.ValidateThis(licenseList);//TODO: entered:20150825. ValidateThis should throw exceptions instead of sending back license messages. 

            if (validationMessages.Any()) return validationMessages.ToArray();

            validationMessages = navOrder.Action == (int) ActionEnum.Create ? CreateNewProductsForOrder(navOrder) : UpdateProductsForOrder(navOrder);

            return validationMessages.ToArray();
        }

        private List<LicenseMessage> UpdateProductsForOrder(NavOrder navOrder)
        {
            throw new NotImplementedException();
        }

        private List<LicenseMessage> CreateNewProductsForOrder(NavOrder navOrder)
        {
            return navOrder.ProductOrders.Select(productOrder => new LicenseMessage
            {
                Code = SuccessGuids.CreatedGuid, 
                Message = "Added license for product:" + productOrder.ProductName, 
                MessageValues = CreateLicenseForOrder(productOrder,navOrder.OrderNumber).ConvertToMessageValueArray(), 
                Severity = (int) MessageStateEnum.Information
            }).ToList();
        }

        private LicensePackage CreateLicenseForOrder(ProductOrder productOrder, string orderNumber)
        {
            //TODO: Might have performance hit. 
            var licenseToCreate = new License
                {
                    LicenseKey = new Guid().ToString(),
                    OrderNumber = orderNumber,
                    Product = _productRepository.GetProductByName(productOrder.ProductName)
                    //Create rest of license object here
                };
                var package = licenseToCreate.GetLicensePackageFromLicense();
                _licenseRepository.CreateLicense(licenseToCreate);

            
            return package;
        }

      
    }
}