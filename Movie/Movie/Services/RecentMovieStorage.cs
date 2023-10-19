using Movie.Models;
using System.Collections.Concurrent;

namespace Movie.Services
{
    public class RecentMovieStorage : IRecentMovieStorage
    {
        private ConcurrentQueue<Cinema> Cinemas { get; set; } =new ConcurrentQueue<Cinema>();
        public void Add(Cinema cinema)
        {
            if(!Cinemas.Contains(cinema))
            {
                Cinemas.Enqueue(cinema);
            }
            
            if(Cinemas.Count >6)
            {
                Cinemas.TryDequeue(out _);
            }
        }

        public IEnumerable<Cinema> GetRecent()
        {
            return Cinemas;
        }
    }
}
