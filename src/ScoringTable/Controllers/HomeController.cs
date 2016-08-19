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
        public IActionResult Index()
        {
            var scoringService = new ScoringService();
            var model = scoringService.GetAllTeams(); //PrepareTestData();

            var timeLeft = scoringService.GetTimeLeft("timer_1");
            var numberOfChalenges = model.FirstOrDefault().Mazes.Count;
            //List<Maze> bestScores, worstScores;

            //CalculateEdgeScores(model, numberOfChalenges, out bestScores, out worstScores);

            //ViewData["BestScores"] = bestScores.Select(b => b.Id).ToList();
            //ViewData["WorstScores"] = worstScores.Select(b => b.Id).ToList();
            ViewData["TimeLeft"] = timeLeft;
            return View(model);
        }

        //private static List<Team> PrepareTestData()
        //{
        //    return new List<Team>
        //    {
        //        new Team {Mazes = new List<int> {1, 3, 0, 4}, Name = "Cool Team", Id = "1"},
        //        new Team {Mazes = new List<int> {8, 0, 3, 4}, Name = "Super Team", Id = "2"}
        //    };
        //}

        //private static void CalculateEdgeScores(List<Team> model, int numberOfChalenges, out List<Maze> bestScores, out List<Maze> worstScores)
        //{
        //    bestScores = new List<Maze>();
        //    worstScores = new List<Maze>();
        //    for (int i = 0; i < numberOfChalenges; i++)
        //    {
        //        bestScores.Add(new Maze { Id = i.ToString(), BestTeam = 0, Score = int.MaxValue });
        //        worstScores.Add(new Maze { Id = i.ToString(), BestTeam = 0, Score = int.MinValue });

        //        for (int j = 0; j < model.Count; j++)
        //        {
        //            if (model[j].Mazes[i] < bestScores[i].Score)
        //            {
        //                bestScores[i].Id = model[j].Id;
        //                bestScores[i].BestTeam = j;
        //                bestScores[i].Score = model[j].Mazes[i];
        //            }
        //            else if (model[j].Mazes[i] > worstScores[i].Score)
        //            {
        //                worstScores[i].Id = model[j].Id;
        //                worstScores[i].BestTeam = j;
        //                worstScores[i].Score = model[j].Mazes[i];
        //            }

        //        }
        //    }
        //}

        public IActionResult Error()
        {
            return View();
        }
    }
}
