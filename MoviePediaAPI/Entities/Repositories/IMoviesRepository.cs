using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Repositories
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> GetMovies();
    }
}
