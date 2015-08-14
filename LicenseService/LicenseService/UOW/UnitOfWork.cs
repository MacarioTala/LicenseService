using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace LicenseService.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ISessionFactory SessionFactory;
        private ITransaction _transaction;

        public ISession Session { get; private set; }

        static UnitOfWork()
        {
            SessionFactory = Fluently.Configure()
                 .Database(MsSqlConfiguration.MsSql2008
                 .ConnectionString(MapperConnectionStrings.CurrentConnection)
                 )
                 .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Product>())
                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true)
                 )
                 .BuildSessionFactory();
        }

        public UnitOfWork()
        {
            Session = SessionFactory.OpenSession();
        }

        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                Session.Close();
            }
        }
    }
}