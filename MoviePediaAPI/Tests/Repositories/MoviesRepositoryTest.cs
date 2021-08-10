﻿using Entities.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Persistance;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Repositories
{
    public class MoviesRepositoryTest
    {
        private readonly Mock<IConfiguration> config;
        private readonly Mock<IConfigurationSection> configSection;
        public MoviesRepositoryTest()
        {
            config = new Mock<IConfiguration>();
            configSection = new Mock<IConfigurationSection>();
            config.Setup(x => x.GetSection(It.IsAny<string>())).Returns(configSection.Object);
        }

        [Fact]
        public async Task GetMoviesReturnsAllMovies()
        {
            configSection.Setup(x => x.Value).Returns("json1.json");
            IMoviesRepository repo = new MoviesRepository(config.Object);
            var movies = await repo.GetMovies();
            Assert.Equal(2, movies.Movies.Count());
            Assert.Equal(7.4, movies.Movies.First().ImdbRating);
            Assert.Equal("Harry Potter and the Chamber of Secrets", movies.Movies.First().Title);
            Assert.Equal("ENGLISH", movies.Movies.First().Language);
            Assert.Equal("NOW_SHOWING", movies.Movies.First().ListingType);
            Assert.Equal("PUNE", movies.Movies.First().Location);
            Assert.Equal(2, movies.Movies.First().SoundEffects.Count());
            Assert.Equal(3, movies.Movies.First().Stills.Count());
        }

        [Fact]
        public async Task GetMoviesThrowsErrorIfFileNotFound()
        {
            configSection.Setup(x => x.Value).Returns("json2.json");
            IMoviesRepository repo = new MoviesRepository(config.Object);
            await Assert.ThrowsAsync<FileNotFoundException>(() => repo.GetMovies());
        }
    }
}
