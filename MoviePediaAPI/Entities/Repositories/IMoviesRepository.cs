using System.Threading.Tasks;

namespace Entities.Repositories
{
    public interface IMoviesRepository
    {
        Task<MoviesAggregate> GetMovies();
    }
}
