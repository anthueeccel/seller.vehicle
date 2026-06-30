using FluentNHibernate.Mapping;
using mvc_client.Models;

public class CountryPhoneCodeMap : ClassMap<CountryPhoneCode>
{
    public CountryPhoneCodeMap()
    {
        Table("COUNTRYPHONECODE");

        Id(x => x.Id)
            .Column("ID")
            .GeneratedBy.Identity();

        Map(x => x.Name).Column("NAME");
        Map(x => x.DialCode).Column("DIALCODE");
        Map(x => x.Code).Column("CODE");
    }
}