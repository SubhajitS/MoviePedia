using Entities;
using Entities.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class MoviesAggregateTest
    {
        private readonly Mock<IMoviesRepository> repo;
        private readonly MoviesAggregate MA;

        public MoviesAggregateTest()
        {
            repo = new Mock<IMoviesRepository>();
            MA = new MoviesAggregate(repo.Object);
        }

        [Fact]
        public void MoviesCollectionShouldBeEmptyInitially()
        {
            Assert.Null(MA.Movies);
        }

        [Fact]
        public async Task MoviesCollectionShouldNotBeEmptyAfterCallingGetAllMovies()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var movies = await MA.GetAllMovies();
            Assert.Equal(2, movies.Count());
            Assert.Equal(movies, MA.Movies);
        }

        [Fact]
        public async Task FilterMoviesByTitleReturnsEmptyList()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetMoviesByTitle("Test");
            Assert.NotNull(filterMovies);
            Assert.Empty(filterMovies);
        }

        [Fact]
        public async Task FilterMoviesByTitleReturnsMatchingTitle()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetMoviesByTitle("The first one");
            Assert.NotNull(filterMovies);
            Assert.Single(filterMovies);
        }

        [Fact]
        public async Task FilterMoviesByTitleReturnsMatchsPartially()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetMoviesByTitle("The first");
            Assert.NotNull(filterMovies);
            Assert.Single(filterMovies);
        }

        [Fact]
        public async Task FilterMoviesByTitleReturnsAllIfTitleIsNull()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetMoviesByTitle(null);
            Assert.NotNull(filterMovies);
            Assert.Equal(2, filterMovies.Count());
        }

        [Fact]
        public async Task FilterMoviesDoesNotReturnIfMismatch()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetFilteredMovies("English", "Pune");
            Assert.NotNull(filterMovies);
            Assert.Empty(filterMovies);
        }

        [Fact]
        public async Task FilterMoviesReturnsAllIfLocationAndLanguageAreNull()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetFilteredMovies(null, null);
            Assert.NotNull(filterMovies);
            Assert.Equal(2, filterMovies.Count());
        }

        [Fact]
        public async Task FilterMoviesReturnsIfOnlyLanguageMatches()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetFilteredMovies("Bengali", "Pune");
            Assert.NotNull(filterMovies);
            Assert.Single(filterMovies);
            Assert.Equal("The first one", filterMovies.First().Title);
        }

        [Fact]
        public async Task FilterMoviesReturnsIfOnlyLanguageMatchesAndLocationIsNull()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetFilteredMovies("Bengali", null);
            Assert.NotNull(filterMovies);
            Assert.Single(filterMovies);
            Assert.Equal("The first one", filterMovies.First().Title);
        }

        [Fact]
        public async Task FilterMoviesReturnsIfOnlyLocationMatches()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetFilteredMovies("English", "Kolkata");
            Assert.NotNull(filterMovies);
            Assert.Equal(2, filterMovies.Count());
            Assert.Equal("Just by a whisker", filterMovies.Last().Title);
        }

        [Fact]
        public async Task FilterMoviesReturnsIfOnlyLocationMatchesButLanguageIsNUll()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetFilteredMovies(null, "Kolkata");
            Assert.NotNull(filterMovies);
            Assert.Equal(2, filterMovies.Count());
            Assert.Equal("Just by a whisker", filterMovies.Last().Title);
        }

        [Fact]
        public async Task FilterMoviesReturnsIfBothMatches()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                },
                new Movie()
                {
                    Title = "Down by not out",
                    ImdbID = "imdb3",
                    Location = "Pune",
                    Language = "Hindi"
                }
            });
            var filterMovies = await MA.GetFilteredMovies("Hindi", "Kolkata");
            Assert.NotNull(filterMovies);
            Assert.Equal(3, filterMovies.Count());
        }

        [Fact]
        public async Task GetMovieReturnsIfIDMatches()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var movieDetail = await MA.GetMovie("IMDB1");
            Assert.NotNull(movieDetail);
            Assert.Equal("The first one", movieDetail.Title);
        }

        [Fact]
        public async Task GetMovieReturnsNullIfIDDoesnotMatche()
        {
            repo.Setup(x => x.GetMovies()).ReturnsAsync(new List<Movie>
            {
                new Movie()
                {
                    Title = "The first one",
                    ImdbID = "imdb1",
                    Location = "Kolkata",
                    Language = "Bengali"
                },
                new Movie()
                {
                    Title = "Just by a whisker",
                    ImdbID = "imdb2",
                    Location = "Kolkata",
                    Language = "Hindi"
                }
            });
            var movieDetail = await MA.GetMovie("IMDB10");
            Assert.Null(movieDetail);
        }
    }
}
