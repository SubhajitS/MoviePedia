using Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class MoviesAggregate
    {
        private readonly IMoviesRepository _moviesRepository;
        public MoviesAggregate(IMoviesRepository repository)
        {
            _moviesRepository = repository;
        }

        public IEnumerable<Movie> Movies { get; private set; }

        public async Task<IEnumerable<Movie>> GetMoviesByTitle(string title)
        {
            if (Movies == null)
                await this.GetAllMovies();
            
            if (string.IsNullOrEmpty(title))
                return Movies;
            title = title.Trim();
            return Movies.Where(x => x.Title.ToLower().Contains(title.ToLower())).
                Select(x => new Movie()
                {
                    Title = x.Title,
                    ImdbID = x.ImdbID,
                    ListingType = x.ListingType,
                    Language = x.Language,
                    Location = x.Location
                }).ToList();
        }

        public async Task<IEnumerable<Movie>> GetFilteredMovies(string language, string location)
        {
            if (Movies == null)
                await this.GetAllMovies();

            if (!string.IsNullOrEmpty(language))
                language = language.Trim();
            if (!string.IsNullOrEmpty(location))
                location = location.Trim();

            if (string.IsNullOrEmpty(language) && string.IsNullOrEmpty(location))
                return Movies;            

            return Movies.Where(x => string.Equals(x.Language, language, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(x.Location, location, StringComparison.OrdinalIgnoreCase)).
                Select(x => new Movie()
                {
                    Title = x.Title,
                    ImdbID = x.ImdbID,
                    ListingType = x.ListingType,
                    Language = x.Language,
                    Location = x.Location
                }).ToList();
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            this.Movies = await _moviesRepository.GetMovies();
            return this.Movies;
        }

        public async Task<Movie> GetMovie(string id)
        {
            if (Movies == null)
                await this.GetAllMovies();
            return Movies.FirstOrDefault(x => string.Equals(x.ImdbID, id, StringComparison.OrdinalIgnoreCase));                
        }
    }
}
