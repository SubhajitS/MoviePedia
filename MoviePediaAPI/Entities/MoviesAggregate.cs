﻿using Entities.Repositories;
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
            title = title.Trim();
            if (string.IsNullOrEmpty(title))
                return Movies;
            return Movies.Where(x => string.Equals(x.Title, title, StringComparison.OrdinalIgnoreCase)).
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
            language = language.Trim();
            location = location.Trim();

            if (Movies == null)
                await this.GetAllMovies();

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
    }
}
