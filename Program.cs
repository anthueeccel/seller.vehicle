var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Im memory db (initial tests)
var vehicles = new List<Vehicle>
{
    new Vehicle(1, "Toyota", "Corolla", 2023, true),
    new Vehicle(2, "Honda", "Civic", 2022, true)
};

var vehicleGroup = app.MapGroup("/vehicles");

vehicleGroup.MapGet("/{id:int}", (int id) =>
{
    var vehicle = vehicles.FirstOrDefault(v => v.Id == id);
    return vehicle is not null ? Results.Ok(vehicle) : Results.NotFound("Vehicle not found.");
});

vehicleGroup.MapPost("/", (Vehicle newVehicle) =>
{
    vehicles.Add(newVehicle);
    return Results.Created($"/vehicles/{newVehicle.Id}", newVehicle);
});

vehicleGroup.MapPut("/{id:int}", (int id, Vehicle updatedVehicle) =>
{
    var index = vehicles.FindIndex(v => v.Id == id);
    if (index == -1) return Results.NotFound("Vehicle not found.");
    vehicles[index] = updatedVehicle with { Id = id }; // Mantém o ID original
    return Results.NoContent();
});

vehicleGroup.MapPatch("/{id:int}/inactive", (int id) =>
{
    var vehicle = vehicles.FirstOrDefault(v => v.Id == id);
    if (vehicle == null) return Results.NotFound("Vehicle not found.");

    // Atualiza o status para inativo usando C# Record
    var index = vehicles.IndexOf(vehicle);
    vehicles[index] = vehicle with { IsActive = false };

    return Results.Ok($"Vehicle is {id} inactive now.");
});

app.Run();
