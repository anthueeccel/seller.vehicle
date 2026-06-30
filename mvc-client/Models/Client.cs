using System.ComponentModel.DataAnnotations;

namespace mvc_client.Models;

public class Client
{
    public virtual int Id { get; set; }

    public virtual string FirstName { get; set; } = string.Empty;

    public virtual string LastName { get; set; } = string.Empty;

    [RegularExpression(@"^[\w-]{5,200}$", ErrorMessage = "Address must have 5 to 200 characters.")]
    public virtual string Address { get; set; } = string.Empty;

    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email formart: email@email.com.")]
    public virtual string Email { get; set; } = string.Empty;

    [RegularExpression(@"^\d{6,11}$", ErrorMessage = "Phone must have 6 to 11 digits - numbers only.")]
    public virtual string Phone { get; set; } = string.Empty;

    public virtual string? CountryPhoneCode { get; set; }
}