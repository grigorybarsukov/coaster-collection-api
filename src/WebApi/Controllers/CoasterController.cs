using Microsoft.AspNetCore.Mvc;
using WebApi.Persistence;
using WebApi.Persistence.Model;
using WebApi.Providers;

namespace WebApi.Controllers;

[ApiController]
[Route("api/coasters")]
public class CoasterController : ControllerBase
{
    private readonly ICoasterRepository _coasterRepository;
    private readonly IImageProvider _imageProvider;
    private const string PngImageMimeType = "image/png";

    public CoasterController(ICoasterRepository coasterRepository, IImageProvider imageProvider)
    {
        _coasterRepository = coasterRepository;
        _imageProvider = imageProvider;
    }

    [HttpGet("all")]
    [Produces(typeof(Coaster[]))]
    public IActionResult GetAllCoasters()
    {
        return Ok(_coasterRepository.GetAll());
    }

    [HttpGet]
    [Produces(typeof(Coaster[]))]
    public IActionResult Get([FromQuery] CoasterParameters parameters)
    {
        return Ok(_coasterRepository.Get(parameters));
    }

    [HttpGet]
    [Route("{id:int}/image")]
    [ProducesResponseType(typeof(File), 200)]
    public IActionResult GetImage([FromRoute] int id, [FromQuery] bool reverse)
    {
        try
        { 
            var imageStream = _imageProvider.GetImage(id, reverse);
            return new FileStreamResult(imageStream, PngImageMimeType);
        }
        catch (FileNotFoundException)
        {
            return NotFound();
        }
    }
}