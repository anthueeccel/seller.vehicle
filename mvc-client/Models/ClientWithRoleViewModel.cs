using mvc_client.Models;

public class ClientWithRoleViewModel
{
    public Client Client { get; set; } = null!;
    public UserRole? Role { get; set; }
}