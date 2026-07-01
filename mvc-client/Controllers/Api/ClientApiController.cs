using Microsoft.AspNetCore.Mvc;
using mvc_client.Models;

namespace mvc_client.Controllers.Api;

[Route("api/clients")]
[ApiController]
public class ClientApiController : ControllerBase
{
    private readonly ClientRepository _repository;

    public ClientApiController(ClientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var client = _repository.Get(id);

        if (client == null)
            return NotFound(new { message = $"Client with id {id} not found." });

        return Ok(new
        {
            client.Id,
            client.FirstName,
            client.LastName,
            client.Email,
            client.Phone,
            client.Address
        });
    }
}