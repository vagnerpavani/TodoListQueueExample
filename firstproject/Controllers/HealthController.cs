using System.Security.Claims;
using firstproject.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase {
    
    private readonly HealthService _healthService;
    public HealthController(HealthService healthService){
        _healthService = healthService;
    }

    [HttpGet]
    public IActionResult GetHealth()
    {
        string message = _healthService.GetHealthMessage();
		return Ok(message);
    }
}