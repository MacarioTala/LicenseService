using FluentNHibernate.Mapping;

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
            Map(x => x.ExpirationDate);
            Map(x => x.Internal);
            Map(x => x.LicenseKey).Unique();//Changing this to be unique
            Map(x => x.LineNumber);
            Map(x => x.ModifiedDateTime);
            Map(x => x.NumberOfUsers);
            Map(x => x.OrderNumber);
            Map(x => x.Used);

            References(x => x.LicenseMode)
                .Column("LicenseModeId")
                .Fetch.Select()
                .LazyLoad();

            References(x => x.Company)
                .Column("CompanyId")
                .Nullable()
                .Cascade.SaveUpdate()
                .Fetch.Select()
                .LazyLoad();

            // Removing Concept of LicenseGroups

            HasMany(x => x.LicenseModules)
                .KeyColumn("LicenseId")
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Select()
                .LazyLoad();
        }
    }
}