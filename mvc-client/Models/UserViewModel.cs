using mvc_client.Models;

public class UserViewModel
{
    public User User { get; set; } = null!;
    public string ClientFullName { get; set; } = string.Empty;
}