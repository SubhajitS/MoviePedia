using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviePediaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesAggregate _movies;
        public MoviesController(MoviesAggregate movies)
        {
            _movies = movies;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Movie>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> All()
        {
            var allMovies = await _movies.GetAllMovies();
            if (allMovies == null)
                return NoContent();
            return Ok(allMovies);
        }

        [HttpGet("/{ID}")]
        [ProducesResponseType(typeof(Movie), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ByID(string ID)
        {
            var movie = await _movies.GetMovie(ID);
            if (movie == null)
                return NotFound();
            return Ok(movie);
        }

        [HttpGet("/Search")]
        [ProducesResponseType(typeof(IEnumerable<Movie>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ByTitle([FromQuery]string Title)
        {
            var movies = await _movies.GetMoviesByTitle(Title);
            if (movies == null)
                return NotFound();
            return Ok(movies);
        }

        [HttpGet("/Filter")]
        [ProducesResponseType(typeof(IEnumerable<Movie>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ByTitle([FromQuery] string Language, [FromQuery] string Location)
        {
            var movies = await _movies.GetFilteredMovies(Language, Location);
            if (movies == null)
                return NotFound();
            return Ok(movies);
        }
    }
}
