using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Movie.Services;
using System.Diagnostics;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieApiService movieApiService;

        public HomeController(IMovieApiService movieApiService)
        {
            this.movieApiService = movieApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Movie(string id)
        {
            Cinema cinema = null;
            try
            {
                cinema = await movieApiService.SearchByIdAsync(id);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessages = ex.Message;
            }
            return View(cinema);
        }
        public async Task<IActionResult> Search(string title)
        {
            MovieApiResponse result = null;
            try
            {
                 result = await movieApiService.SearchByTitleAsync(title);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessages = ex.Message;
            } 
            ViewBag.searchMovie = title;
            return View(result);
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