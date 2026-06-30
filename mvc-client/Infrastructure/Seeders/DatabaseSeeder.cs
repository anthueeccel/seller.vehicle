using mvc_client.Models;

public class DatabaseSeeder
{
    private readonly ClientRepository _clientRepository;
    private readonly UserRepository _userRepository;

    public DatabaseSeeder(
        ClientRepository clientRepository,
        UserRepository userRepository)
    {
        _clientRepository = clientRepository;
        _userRepository = userRepository;
    }

    public void Seed()
    {
        SeedClientsAndUsers();
    }

    private void SeedClientsAndUsers()
    {
        if (_userRepository.Any())
            return;

        // Seed Admin Client
        var adminClient = new Client
        {
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@admin.com",
            CountryPhoneCode = "+34",
            Phone = "6111234567"

        };
        _clientRepository.Save(adminClient);

        // Seed John Doe Client
        var johnClient = new Client
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@email.com",
            CountryPhoneCode = "+34",
            Phone = "6117654321"

        };
        _clientRepository.Save(johnClient);

        // Seed Admin User (references Admin Client)
        _userRepository.Save(new User
        {
            Client = adminClient,
            Role = UserRole.Admin,
            IsActive = true
        });

        // Seed Client User (references John Doe Client)
        _userRepository.Save(new User
        {
            Client = johnClient,
            Role = UserRole.Client,
            IsActive = true
        });
    }
}