using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.Persistence;

namespace WebApi.Controllers;

[ApiController]
[Route("api/coasters")]
public class CoasterController : ControllerBase
{
    private readonly ICoasterRepository _coasterRepository;
    private readonly ILogger<CoasterController> _logger;

    public CoasterController(ICoasterRepository coasterRepository,ILogger<CoasterController> logger)
    {
        _coasterRepository = coasterRepository;
        _logger = logger;
    }
    
    [HttpGet("all")]
    [Produces(typeof(Coaster[]))]
     public IActionResult GetAllCoasters()
     {
         return Ok(_coasterRepository.GetAll());
     }

     [HttpGet]
     [Produces(typeof(Coaster[]))]
    public IActionResult Get([FromQuery]CoasterParameters parameters)
    {
        return Ok(_coasterRepository.Get(parameters));
    }
}