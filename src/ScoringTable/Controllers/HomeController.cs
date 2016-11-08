using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScoringTable.Models;

namespace ScoringTable.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<Config> _optionsAccessor;

        public HomeController(IOptions<Config> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }
        public IActionResult Index()
        {
            var options = _optionsAccessor.Value;
            var scoringService = new ScoringService(options.MazeGameApiUrl, options.DbApiUrl);
            var model = scoringService.GetAllTeams();

            var timeLeft = scoringService.GetTimeLeft(options.TimerName);

            ViewData["TimeLeft"] = timeLeft;
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
