using Movie.Models;

namespace Movie.Services
{
    public interface IMovieApiService
    {
        Task<MovieApiResponse> SearchByTitleAsync(string title);
        Task<Cinema> SearchByIdAsync(string id);
    }
}