﻿using System;
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
using HomeBot.Services.Movies.Storage;
using HomeBot.Services.Movies;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using System.Collections.Specialized;
using Quartz.Impl;
using Quartz;
using HomeBot.Services.Tasks.Movie;
using Quartz.Spi;
using HomeBot.Services.DbServices;

namespace HomeBot.Controllers
{
    public class HomeController : Controller
    {
        IServiceProvider _serviceProvider;
        ILogService _logService;
        public HomeController(IServiceProvider serviceProvider, ILogService _ogService)
        {
            _serviceProvider = serviceProvider;
            _logService = _ogService;
        }


        public IActionResult Index()
        {
           // MovieDownloadTask.Start(_serviceProvider);
            ViewBag.Logs = _logService.GetTop50Logs();
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
