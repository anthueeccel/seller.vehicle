using mvc_client.Models;
using NHibernate;
using NHibernate.Linq;

public class UserRepository
{
    private readonly ISessionFactory _factory;

    public UserRepository(ISessionFactory factory)
    {
        _factory = factory;
    }

    public bool Any()
    {
        using var session = _factory.OpenSession();
        return session.Query<User>().Any();
    }

    public IList<User> GetAll()
    {
        using var session = _factory.OpenSession();
        return session.Query<User>().Fetch(u => u.Client).ToList();
    }

    public User? Get(int id)
    {
        using var session = _factory.OpenSession();
        return session.Query<User>().Fetch(u => u.Client).FirstOrDefault(u => u.Id == id);
    }

    public void Save(User user)
    {
        using var session = _factory.OpenSession();
        using var tx = session.BeginTransaction();
        session.Save(user);
        tx.Commit();
    }

    public void Update(User user)
    {
        using var session = _factory.OpenSession();
        using var tx = session.BeginTransaction();
        session.Update(user);
        tx.Commit();
    }

    public void Delete(int id)
    {
        using var session = _factory.OpenSession();
        using var tx = session.BeginTransaction();
        var user = session.Get<User>(id);
        if (user != null)
            session.Delete(user);
        tx.Commit();
    }
}