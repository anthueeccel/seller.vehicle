using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("MvcClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MvcClient:BaseUrl"] ?? "http://localhost:5051");
});

var app = builder.Build();

// In memory db (initial tests)
var vehicles = new List<Vehicle>
{
    new Vehicle(1, "Toyota", "Corolla", 2023, true, 1205),
    new Vehicle(2, "Honda", "Civic", 2022, true, null),
    new Vehicle(3, "Opel", "Corsa", 2025, true, 1205)
};

var vehicleGroup = app.MapGroup("/vehicles");

vehicleGroup.MapGet("/{id:int}", async (int id, IHttpClientFactory httpClientFactory) =>
{
    var vehicle = vehicles.FirstOrDefault(v => v.Id == id);
    if (vehicle is null)
        return Results.NotFound("Vehicle not found.");

    try
    {
        var client = httpClientFactory.CreateClient("MvcClient");
        var clientResponse = await client.GetAsync($"/api/clients/{vehicle.ClientId}");

        if (clientResponse.IsSuccessStatusCode)
        {
            var clientData = await clientResponse.Content.ReadFromJsonAsync<object>();
            return Results.Ok(new
            {
                vehicle = new
                {
                    vehicle.Id,
                    vehicle.Make,
                    vehicle.Model,
                    vehicle.Year,
                    vehicle.IsActive,
                    vehicle.ClientId
                },
                client = clientData
            });
        }

        return Results.Ok(new
        {
            vehicle = new
            {
                vehicle.Id,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year,
                vehicle.IsActive,
                vehicle.ClientId
            },
            message = "Not able to retrieve Client's info. try again."
        });
    }
    catch
    {
        return Results.Ok(new
        {
            vehicle = new
            {
                vehicle.Id,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year,
                vehicle.IsActive,
                vehicle.ClientId
            },
            message = "Not able to retrieve Client's info. try again."
        });
    }
});

vehicleGroup.MapPost("/", (Vehicle newVehicle) =>
{
    vehicles.Add(newVehicle);
    return Results.Created($"/vehicles/{newVehicle.Id}", newVehicle);
});

vehicleGroup.MapPut("/{id:int}", async (int id, Vehicle updatedVehicle, IHttpClientFactory httpClientFactory) =>
{
    var index = vehicles.FindIndex(v => v.Id == id);
    if (index == -1) return Results.NotFound("Vehicle not found.");

    var client = httpClientFactory.CreateClient("MvcClient");
    var clientResponse = await client.GetAsync($"/api/clients/{updatedVehicle.ClientId}");

    if (!clientResponse.IsSuccessStatusCode)
    {
        return Results.BadRequest("Invalid ClientId. Client not found.");
    }

    if (clientResponse.IsSuccessStatusCode)
    {
        vehicles[index] = updatedVehicle with { Id = id };
    }
    ;

    return Results.Created($"/vehicles/{id}", vehicles[index]);
});

vehicleGroup.MapPatch("/{id:int}/inactive", (int id) =>
{
    var vehicle = vehicles.FirstOrDefault(v => v.Id == id);
    if (vehicle == null) return Results.NotFound("Vehicle not found.");

    var index = vehicles.IndexOf(vehicle);
    vehicles[index] = vehicle with { IsActive = false };

    return Results.Ok($"Vehicle is {id} inactive now.");
});

app.Run();