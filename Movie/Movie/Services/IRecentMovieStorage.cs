using Movie.Models;

namespace Movie.Services
{
    public interface IRecentMovieStorage
    {
        void Add(Cinema cinema);
        IEnumerable<Cinema> GetRecent();
    }
}
