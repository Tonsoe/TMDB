using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using TMDB.Models;
using RestSharp;
using RestSharp.Serializers;
using Microsoft.VisualBasic;

namespace TMDB.Services
{
    public class Servicio_API : IServicio_API
    {
        private static string? _apiKey;

        public Servicio_API() {

            _apiKey = "4edb43e71c08bffeda4e483fbe143d51";
        }

        public async Task<Movie> GetMovie(string movieTitle)
        {
            Movie movie = new();
            Resultado resultados = new();

            var cliente = new HttpClient();

            var response = await cliente.GetAsync($"https://api.themoviedb.org/3/search/movie?query=" + movieTitle  + "&api_key=" + _apiKey);

            if (response.IsSuccessStatusCode) { 
                var json_response = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Resultado>(json_response);
                resultados.page = result.page;
                resultados.total_pages = result.total_pages;
                resultados.total_results = result.total_results;
                var peliculas = result.results;

                try {
                    movie.id = peliculas[0].id;
                    movie.title = peliculas[0].title;
                    movie.original_title = peliculas[0].original_title;
                    movie.vote_average = peliculas[0].vote_average;
                    movie.release_date = peliculas[0].release_date;
                    movie.overview = peliculas[0].overview;
                } catch (ArgumentOutOfRangeException ex) {
                    movie.id = 0;
                    movie.title = "";
                    movie.original_title = "";
                    movie.vote_average=0;
                    movie.overview = "";
                }
                
            }

            return movie;
        }

        public async Task<List<RelatedMovie>> GetRelatedMovies(int movieId)
        {
            List<RelatedMovie> relatedMovies = [];
            
            Resultado resultados = new();
            DateOnly date;

            var cliente = new HttpClient();

            var response = await cliente.GetAsync($"https://api.themoviedb.org/3/movie/" + movieId + "/recommendations?language=en-US&page=1&api_key=" + _apiKey);

            if (response.IsSuccessStatusCode) { 
                var json_response = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Resultado>(json_response);
                resultados.page = result.page;
                resultados.total_pages = result.total_pages;
                resultados.total_results = result.total_results;
                var peliculas = result.results;

                try
                {
                    for (int i = 0; i < 5; i++)
                    {
                        RelatedMovie relatedMovie = new();

                        relatedMovie.id = peliculas[i].id;
                        relatedMovie.title = peliculas[i].title;
                        date = DateOnly.Parse(peliculas[i].release_date);
                        relatedMovie.year = date.Year;
                        relatedMovies.Add(relatedMovie);
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {   
                }
            }


            return relatedMovies;
        }
    }
}
