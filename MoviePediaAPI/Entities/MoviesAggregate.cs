using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Entities
{
    public class MoviesAggregate
    {
        public MoviesAggregate()
        {
            LoadAllMovies();
        }

        public IEnumerable<Movie> AllMovies { get; private set; }

        public IEnumerable<Movie> GetMoviesByTitle(string title)
        {
            title = title.Trim();
            if (string.IsNullOrEmpty(title))
                return AllMovies;
            return AllMovies.Where(x => string.Equals(x.Title, title, StringComparison.OrdinalIgnoreCase)).
                Select(x => new Movie()
                {
                    Title = x.Title,
                    ID = x.ID,
                    ListingType = x.ListingType,
                    Language = x.Language,
                    Location = x.Location
                });
        }

        public IEnumerable<Movie> GetFilteredMovies(string language, string location)
        {
            language = language.Trim();
            location = location.Trim();

            return AllMovies.Where(x => string.Equals(x.Language, language, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(x.Location, location, StringComparison.OrdinalIgnoreCase)).
                Select(x => new Movie()
                {
                    Title = x.Title,
                    ID = x.ID,
                    ListingType = x.ListingType,
                    Language = x.Language,
                    Location = x.Location
                });
        }

        private void LoadAllMovies()
        {
            AllMovies = new List<Movie>();
        }
    }
}
