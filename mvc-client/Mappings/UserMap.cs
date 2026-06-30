using FluentNHibernate.Mapping;
using NHibernate.Type;
using mvc_client.Models;

public class UserMap : ClassMap<User>
{
    public UserMap()
    {
        Table("APPUSER");

        Id(x => x.Id)
            .Column("ID")
            .GeneratedBy.Identity();

        References(x => x.Client)
            .Column("CLIENTID")
            .ForeignKey("FK_USER_CLIENT")
            .Not.Nullable();

        Map(x => x.Role)
            .Column("ROLE")
            .CustomType<int>();

        Map(x => x.IsActive)
            .Column("ISACTIVE")
            .CustomType<BooleanType>()
            .Not.Nullable();
    }
}