namespace TMDB.Models
{
    public class Resultado
    {
        public int page { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
        public List<Movie>? results { get; set; }
    }
}
