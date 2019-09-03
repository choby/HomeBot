using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeBot.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using HtmlAgilityPack;
using HomeBot.Services.MovieDownload.Storage;
using HomeBot.Services.MovieDownload;

namespace HomeBot.Controllers
{
    public class HomeController : Controller
    {

        IMovieDownload _movieDownload;
        HtmlWeb _webClient;
        IStorageMedium _storageMedium;
        public HomeController(IStorageMedium storageMedium, IMovieDownload movieDownload,
            HtmlWeb webClient)
        {
            _storageMedium = storageMedium;
            _movieDownload = movieDownload;
            _webClient = webClient;
        }


        public IActionResult Index()
        {

            // this.downloadMagnet("magnet:?xt=urn:btih:7757f5c4d12b3fcc9a34552a3522cd5525032844&dn=Dark.Phoenix.2019.1080p.BluRay.x264-GECKOS&tr=http%3A%2F%2Ftracker.trackerfix.com%3A80%2Fannounce&tr=udp%3A%2F%2F9.rarbg.me%3A2740&tr=udp%3A%2F%2F9.rarbg.to%3A2790");
            //var magnet = getMagnet("http://gaoqing.la/cold-blood.html");
            //_storageMedium.Store("xx");
            _movieDownload.Excute();
            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
