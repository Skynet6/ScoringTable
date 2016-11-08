using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScoringTable.Models;

namespace ScoringTable.Controllers
{
    public class HomeController : Controller
    {
        private readonly string defaultMazeGameApiUrl = "http://192.168.0.50:12345/";
        private readonly string defaultDbApiUrl = "http://192.168.0.50:8080/";

        public IActionResult Index()
        {
            //var scoringService = new ScoringService(defaultMazeGameApiUrl, defaultDbApiUrl);
            var scoringService = new ScoringService();
            var model = scoringService.GetAllTeams();

            //var timeLeft = scoringService.GetTimeLeft("timer_1");
            var timeLeft = scoringService.GetTimeLeft("");

            ViewData["TimeLeft"] = timeLeft;
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
