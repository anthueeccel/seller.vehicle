using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

public static class NHibernateHelper
{
    private static ISessionFactory _sessionFactory;

    public static ISessionFactory GetSessionFactory(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("OracleDb");

        return _sessionFactory ??= Fluently.Configure()
            .Database(
                OracleDataClientConfiguration.Oracle10
                    .Driver<NHibernate.Driver.OracleManagedDataClientDriver>()
                    .Dialect<NHibernate.Dialect.Oracle12cDialect>()
                    .ConnectionString(connectionString)
            )
            .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<ClientMap>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
    }
}
