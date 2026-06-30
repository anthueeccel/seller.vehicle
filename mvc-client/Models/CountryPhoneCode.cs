namespace mvc_client.Models;

public class CountryPhoneCode
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string DialCode { get; set; } = string.Empty;
    public virtual string Code { get; set; } = string.Empty;
}