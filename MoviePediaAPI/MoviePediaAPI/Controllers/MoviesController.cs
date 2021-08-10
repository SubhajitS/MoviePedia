using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviePediaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        public MoviesController()
        {

        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok();
        }
    }
}
