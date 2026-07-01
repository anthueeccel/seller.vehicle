public record Vehicle
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public bool IsActive { get; set; }
    public int? ClientId { get; set; }


    public Vehicle(int id, string make, string model, int year, bool isActive, int? clientId)
    {
        Id = id;
        Make = make;
        Model = model;
        Year = year;
        IsActive = isActive;
        ClientId = clientId;
    }
}