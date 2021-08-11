using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MoviePediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var reqPathErrorContext = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var details = reqPathErrorContext.Error.StackTrace ?? "Malpractice behaviour detected";
            var instance = reqPathErrorContext.Path ?? "Malpractice behaviour detected";
            var title = reqPathErrorContext.Error.Message ?? "Malpractice behaviour detected";
            var type = reqPathErrorContext.Error.GetType().Name ?? "Malpractice behaviour detected";
            var traceid = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            int statusCode = 500;

            _logger.LogError(reqPathErrorContext.Error, title, details, traceid);

            return Problem("Please contact administrator", instance, statusCode, "Something went wrong", type);
        }
    }
}
