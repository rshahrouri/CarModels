using CarModels.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarModels.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelsController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public ModelsController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int modelyear, string make)
    {
        var models = await _serviceProvider.GetRequiredService<IModelsService>()
            .Get(modelyear, make);

        var response = new
        {
            Models = models?.Select(m => m.Model_Name) ?? Enumerable.Empty<string>()
        };

        return Ok(response);
    }
}
