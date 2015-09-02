using System;
using System.Collections.Generic;
using LicenseService.Repositories;

namespace LicenseService.Services
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public List<string> GetCompanyList()
        {
            throw new NotImplementedException();
            var companies = _companyRepository.GetCompanyList();
            foreach (var company in companies)
            {
                
            }
        }
    }
}
