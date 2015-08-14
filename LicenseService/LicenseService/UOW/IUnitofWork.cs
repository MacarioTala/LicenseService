using NHibernate;

namespace LicenseService
{
    public interface IUnitOfWork
    {
        ISession Session { get; }
        void BeginTransaction();
        void Commit();
    }
}
