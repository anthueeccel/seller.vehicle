using mvc_client.Models;
using NHibernate;

public class ClientRepository
{
    private readonly ISessionFactory _factory;

    public ClientRepository(ISessionFactory factory)
    {
        _factory = factory;
    }

    public IList<Client> GetAll()
    {
        using var session = _factory.OpenSession();

        return session.Query<Client>().ToList();
    }

    public Client? Get(int id)
    {
        using var session = _factory.OpenSession();

        return session.Query<Client>().FirstOrDefault(c => c.Id == id);
    }

    public void Save(Client client)
    {
        using var session = _factory.OpenSession();
        using var tx = session.BeginTransaction();

        session.Save(client);

        tx.Commit();
    }

    public void Update(Client client)
    {
        using var session = _factory.OpenSession();
        using var tx = session.BeginTransaction();

        session.Update(client);

        tx.Commit();
    }

    public void Delete(int id)
    {
        using var session = _factory.OpenSession();
        using var tx = session.BeginTransaction();

        var client = session.Get<Client>(id);

        if (client != null)
            session.Delete(client);

        tx.Commit();
    }
}