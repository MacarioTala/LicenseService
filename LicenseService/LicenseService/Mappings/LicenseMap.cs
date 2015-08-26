using FluentNHibernate.Mapping;
using LicenseService.Entities;

namespace LicenseService.Mappings
{
    public class LicenseMap : ClassMap<License>
    {
        public LicenseMap()
        {
            Table("[Company].[License]");
            OptimisticLock.None();
            LazyLoad();

            Id(x => x.LicenseId)
                .GeneratedBy.Identity();

            Map(x => x.Active);
            Map(x => x.EulaAccepted)
                .Column("EULAAccepted");
      
            Map(x => x.Internal);
            Map(x => x.LicenseKey).Unique();//Changing this to be unique
            Map(x => x.LineNumber);
            Map(x => x.ModifiedDateTime);
            Map(x => x.NumberOfUsers);
            Map(x => x.OrderNumber);
            Map(x => x.Used);

           // removed license mode, expiration date

            References(x => x.Company)
                .Column("CompanyId")
                .Nullable()
                .Cascade.SaveUpdate()
                .Fetch.Select()
                .LazyLoad();

            // Removing Concept of LicenseGroups

           // removing license module
        }
    }
}