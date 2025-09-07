using Microsoft.AspNetCore.Mvc;
using ClassProjectApp.Services;

namespace ClassProjectApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdditionApiController : ControllerBase
    {
        private readonly AdditionService _additionService;

        public AdditionApiController(AdditionService additionService)
        {
            _additionService = additionService;
        }

        [HttpGet("add")]
        public IActionResult Add([FromQuery] int a, [FromQuery] int b)
        {
            var result = _additionService.Add(a, b);
            return Ok(new { result });
        }
    }
}