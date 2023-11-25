using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternalManagement.Controllers;

[ApiController]
[Route("[Controller]")]
public class ClientController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ClientController> _logger;
    private readonly IClientServices _clientServices;

    public ClientController(ILogger<ClientController> logger, IClientServices clientServices)
    {
        _logger = logger;
        _clientServices = clientServices;
    }

    [HttpGet()]
    public async Task<IActionResult> GetClients()
    {
        try
        {
            return Ok(await _clientServices.GetClients());
        }
        catch
        {
            return BadRequest(new List<Client>());
        }
    }

    [HttpPost()]
    public async Task<IActionResult> CreateClient([FromBody] NewClientVM client)
    {
        try
        {
            await _clientServices.CreateClient(client);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateClient([FromBody] ClientVM client)
    {
        try
        {
            await _clientServices.UpdateClient(client);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        try
        {
            await _clientServices.DeleteClient(id);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
