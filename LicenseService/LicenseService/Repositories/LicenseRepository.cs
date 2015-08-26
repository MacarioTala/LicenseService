using System;
using System.Collections.Generic;
using System.Linq;
using LicenseService.Entities;
using LicenseService.UOW;
using NHibernate;
using NHibernate.Linq;

namespace LicenseService.Repositories
{
   public class LicenseRepository : ILicenseRepository
   {
       private readonly IUnitOfWork _unitOfWork;
       protected ISession Session { get { return _unitOfWork.Session; } }

       public LicenseRepository(UnitOfWork unitOfWork)
       {
           _unitOfWork = (UnitOfWork)unitOfWork;
       }

       public List<License> GetLicensesByLicenseId(Guid licenseId)
       {
           throw new NotImplementedException();
       }


       public License GetLicenseByLicenseKey(string licensekey)
       {
           var resultList = Session.Query<License>().Where(x => x.LicenseKey == licensekey);

           if (resultList.Count() > 1) { throw new Exception("LicenseKey is not Unique!");}
           return resultList.FirstOrDefault();
       }

       public void CreateLicense(License licenseToCreate)
       {
           Session.Save(licenseToCreate);
       }

       public void UpdateLicense(License licenseToUpdate)
       {
           Session.Update(licenseToUpdate);
       }

       public List<License> GetLicensesByLicenseKeys(List<string> licenseKeys)
       {
           return Session.Query<License>().Where(x => licenseKeys.Contains(x.LicenseKey)).ToList();
       }

    }
}
