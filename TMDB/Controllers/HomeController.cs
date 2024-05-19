using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TMDB.Models;

using TMDB.Services;

namespace TMDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicioApi;

        public HomeController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index(string movieName)
        {
            Movie movie = new Movie();
            List<RelatedMovie> relatedMovies = [];
            TradeViewModel model = new TradeViewModel();
            ViewBag.Accion = "Película";
            
            if (movieName != "") {
                 movie = await _servicioApi.GetMovie(movieName);
                 relatedMovies = await _servicioApi.GetRelatedMovies(movie.id);
            }

            movie = await _servicioApi.GetMovie(movieName);
            relatedMovies = await _servicioApi.GetRelatedMovies(movie.id);

            model.movie = movie;
            model.relatedMovies = relatedMovies;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
