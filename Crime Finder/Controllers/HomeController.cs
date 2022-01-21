using Crime_Finder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Crime_Finder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CrimeService CrimeService;
        private DateService DateService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            CrimeService = new CrimeService();
            DateService = new DateService();
        }

        public async Task<IActionResult> Index()
        {
            DateModel Date = await CrimeService.GetValidLatestUpdate();
            ViewBag.LatestUpdate = DateService.GetDateAsString(Date);
            ViewBag.Dates = DateService.GetValidDates(Date);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Index(string longitude, string latitude, string date)
        {
            try
            {
                DateModel Date = await CrimeService.GetValidLatestUpdate();
                ViewBag.LatestUpdate = DateService.GetDateAsString(Date);
                ViewBag.Dates = DateService.GetValidDates(Date);
                if (String.IsNullOrWhiteSpace(longitude) || String.IsNullOrWhiteSpace(latitude))
                {
                    ViewBag.Error += "Error: Longitude or Latitude coordinates invalid.";
                    return View();
                }

                string url = CrimeService.APIUrl(longitude, latitude, date);
                var crimes = await CrimeService.GetCrimesFromJson(url);
                if (crimes == null) ViewBag.Error += "Error: No crimes found within range.";
                else
                {
                    ViewBag.CrimeCategorys = CrimeService.GetCrimeCategories(crimes);
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error += "Error: Something went wrong." + e.Message;
                return View();
            }
        }
    }
}
