using mvc_client.Models;
using NHibernate;

public class CountryPhoneCodeRepository
{
    private readonly ISessionFactory _factory;

    public CountryPhoneCodeRepository(ISessionFactory factory)
    {
        _factory = factory;
    }

    public CountryPhoneCode? Get(int id)
    {
        using var session = _factory.OpenSession();
        return session.Get<CountryPhoneCode>(id);
    }

    public IList<CountryPhoneCode> GetAll()
    {
        using var session = _factory.OpenSession();
        return session.Query<CountryPhoneCode>().ToList();
    }

    public void Save(CountryPhoneCode code)
    {
        using var session = _factory.OpenSession();
        using var tx = session.BeginTransaction();
        session.Save(code);
        tx.Commit();
    }

    public bool Any()
    {
        using var session = _factory.OpenSession();
        return session.Query<CountryPhoneCode>().Any();
    }
}