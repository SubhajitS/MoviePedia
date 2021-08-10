using Entities;
using Entities.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Persistance
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly IConfiguration _configuration;
        private const string _jsonPath = "JSONPath";
        public MoviesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            IEnumerable<Movie> movies = null;
            using (StreamReader r = new StreamReader(_configuration.GetSection(_jsonPath).Value))
            {
                string json = await r.ReadToEndAsync();
                movies = JObject.Parse(json).SelectToken("movies").ToObject<IEnumerable<Movie>>();
            }
            return movies;
        }
    }
}
