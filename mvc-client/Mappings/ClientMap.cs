using FluentNHibernate.Mapping;
using mvc_client.Models;

public class ClientMap : ClassMap<Client>
{
    public ClientMap()
    {
        Table("CLIENT");

        Id(x => x.Id)
            .Column("ID")
            .GeneratedBy.Identity();

        Map(x => x.FirstName).Column("FIRSTNAME");
        Map(x => x.LastName).Column("LASTNAME");
        Map(x => x.Email).Column("EMAIL");
        Map(x => x.Phone).Column("PHONE");
        Map(x => x.CountryPhoneCode).Column("COUNTRYPHONECODE");
    }
}
