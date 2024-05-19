using TMDB.Models;

namespace TMDB.Services

{
    public interface IServicio_API
    {
        Task<Movie> GetMovie(string movieTitle);
        Task<List<RelatedMovie>> GetRelatedMovies(int movieId);
    }
}
