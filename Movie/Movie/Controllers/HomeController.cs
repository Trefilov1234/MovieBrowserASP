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
        private readonly IRecentMovieStorage recentMovieStorage;

        public HomeController(IMovieApiService movieApiService,IRecentMovieStorage recentMovieStorage)
        {
            this.movieApiService = movieApiService;
            this.recentMovieStorage = recentMovieStorage;
        }

        public async Task<IActionResult> Index()
        {

            var result = recentMovieStorage.GetRecent();
            return View(result);
        }

        public async Task<IActionResult> Movie(string id)
        {
            Cinema cinema = null;
            try
            {
                cinema = await movieApiService.SearchByIdAsync(id);
                recentMovieStorage.Add(cinema);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessages = ex.Message;
            }
            return View(cinema);
        }
        public async Task<IActionResult> MovieModal(string id)
        {
            Cinema cinema = null;

            try
            {
                cinema = await movieApiService.SearchByIdAsync(id);
                recentMovieStorage.Add(cinema);
            }
            catch (Exception ex)
            {

                ViewBag.errorMessages = ex.Message;
            }
            return PartialView("_MovieModalPartial", cinema);
        }
        public async Task<IActionResult> SearchResult(string title, int page = 1, int countViewPage = 4)
        {
            SearchViewModel searchViewModel = new SearchViewModel();


            try
            {
                MovieApiResponse result = await movieApiService.SearchByTitleAsync(title, page);
                searchViewModel.Movies = result.Cinemas;
            }
            catch (Exception ex)
            {
                searchViewModel.Error = ex.Message;
            }

            return PartialView("_MovieListPartial", searchViewModel.Movies);
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