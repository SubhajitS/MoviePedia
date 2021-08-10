using Entities;
using Xunit;

namespace Tests
{
    public class MoviesAggregateTest
    {
        [Fact]
        public void LoadAllMovies()
        {
            var MA = new MoviesAggregate();
            Assert.NotNull(MA.Movies);
            Assert.Empty(MA.Movies);
        }

        [Fact]
        public void FilterMoviesByTitleReturnsEmptyList()
        {
            var MA = new MoviesAggregate();
            var filterMovies = MA.GetMoviesByTitle("Test");
            Assert.NotNull(filterMovies);
            Assert.Empty(filterMovies);
        }

        [Fact]
        public void FilterMovies()
        {
            var MA = new MoviesAggregate();
            var filterMovies = MA.GetFilteredMovies("English", "Kolkata");
            Assert.NotNull(filterMovies);
            Assert.Empty(filterMovies);
        }
    }
}
