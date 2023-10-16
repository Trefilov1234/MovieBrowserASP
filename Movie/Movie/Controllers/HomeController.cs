using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Movie.Services;
using Movie.ViewModels;
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
        public async Task<IActionResult> Search(string title, int page = 1)
        {
            
            SearchViewModel searchViewModel=new SearchViewModel();
            try
            {
                MovieApiResponse result = await movieApiService.SearchByTitleAsync(title, page);
                //ViewBag.TotalPages = Math.Ceiling((double)result.TotalResults/10);
                //ViewBag.CurrentPage = page;
                searchViewModel.Title = title;
                searchViewModel.Movies = result.Cinemas;
                searchViewModel.Response=result.Response;
                searchViewModel.Error=result.Error;
                searchViewModel.CurrentPage= page;
                searchViewModel.TotalResults=result.TotalResults;
                searchViewModel.TotalPages= (int)Math.Ceiling((double)result.TotalResults / 10);
            }
            catch (Exception ex)
            {
                searchViewModel.Error = ex.Message;
            } 
            ViewBag.searchMovie = title;
            return View(searchViewModel);
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