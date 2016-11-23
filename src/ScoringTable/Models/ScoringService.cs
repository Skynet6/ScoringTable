using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ScoringTable.Models.DAL;
using ScoringTable.Models.DAL.SolvedMazes;

namespace ScoringTable.Models
{
    public class ScoringService
    {
        public string MazeGameApiUrl { get;}
        public string DbApiUrl { get;}

        public ScoringService(){}

        public ScoringService(string mazeGameApiUrl, string dbApiUrl)
        {
            MazeGameApiUrl = mazeGameApiUrl;
            DbApiUrl = dbApiUrl;
        }

        public List<Team> GetAllTeams()
        {
            var teams = new List<Team>();

            if (string.IsNullOrEmpty(DbApiUrl))
                return PrepareTestData();

            var result = getDataFromRestAPI(DbApiUrl, "restapi/teams").Result;

            var dalTeams = JsonConvert.DeserializeObject<DALTeams>(result);

            GetScoresForTeams(dalTeams, teams);

            var solvedMazes = GetSolvedMazes();
            foreach (var solvedMaze in solvedMazes)
            {
                var team = teams.SingleOrDefault(t => t.Id == solvedMaze.teamId);
                if (team != null && team.Mazes.Any())
                {
                    var maze = team.Mazes.FirstOrDefault(m => m.Id == solvedMaze.mazeId);
                    if (maze != null)
                        maze.Solved = true;
                }
            }

            return teams.OrderBy(t => t.Sum).ToList();
        }

        private static List<Team> PrepareTestData()
        {
            return new List<Team>
            {
                new Team {Mazes = new List<Maze> {new Maze {Score = 22, BestTeam = true},new Maze {Score = 33, WorstTeam = true, Solved = true} }, Name = "Cool Team", Id = "1"},
                new Team {Mazes = new List<Maze> {new Maze {Score = 45, WorstTeam = true, Solved = true},new Maze {Score = 84, BestTeam = true} }, Name = "Super Team", Id = "2"}
            };
        }

        private void GetScoresForTeams(DALTeams dalTeams, List<Team> teams)
        {
            var maxScore = int.MinValue;
            var minScore = int.MaxValue;

            foreach (var dalTeam in dalTeams._embedded.teams)
            {
                var team = new Team {Id = dalTeam.externalId, Name = dalTeam.description};
                //getting scores for teams
                var mazes = GetTeam(team.Id);
                foreach (var maze in mazes)
                {
                    var tempMaze = new Maze {Id = maze.mazeId, Score = maze.score};

                    if (maze.score <= minScore && maze.score != 0)
                    {
                        minScore = maze.score;
                        tempMaze.BestTeam = true;
                        teams.ForEach(t =>
                        {
                            if (t.Mazes.Any())
                            {
                                var innerMaze = t.Mazes.FirstOrDefault(m => m.Id == maze.mazeId);
                                if (innerMaze != null && innerMaze.Score != maze.score)
                                    innerMaze.BestTeam = false;
                            }
                        });
                    }
                    else if (maze.score >= maxScore && maze.score != 0)
                    {
                        maxScore = maze.score;
                        tempMaze.WorstTeam = true;
                        teams.ForEach(t =>
                        {
                            if (t.Mazes.Any())
                            {
                                var innerMaze = t.Mazes.FirstOrDefault(m => m.Id == maze.mazeId);
                                if (innerMaze != null && innerMaze.Score != maze.score)
                                    innerMaze.WorstTeam = false;
                            }
                        });
                    }

                    team.Mazes.Add(tempMaze);
                }
                teams.Add(team);
            }
        }

        public List<SolvedMaze> GetSolvedMazes()
        {
            var result = getDataFromRestAPI(DbApiUrl, "restapi/solvedMazes").Result;

            var dalSolvedMazes = JsonConvert.DeserializeObject<SolvedMazes>(result);

            return dalSolvedMazes._embedded.solvedMazes;
        }

        public List<DAL.Maze> GetTeam(string id)
        {
            var result = getDataFromRestAPI(MazeGameApiUrl, $"maze-game/points/teamMazes/{id}").Result;
            var scores = JsonConvert.DeserializeObject<Scores>(result);

            return scores.mazes;
        }

        public int GetTimeLeft(string timerId)
        {
            if (string.IsNullOrEmpty(timerId))
                return 100; // return test data

            var result = getDataFromRestAPI(MazeGameApiUrl, $"maze-game/timer/getRemainingTime/{timerId}").Result;
            var timer = JsonConvert.DeserializeObject<Timer>(result);
            return timer?.remainingTime ?? 1;
        }

        private static async Task<string> getDataFromRestAPI(string baseAddress, string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout =TimeSpan.FromSeconds(10);

                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return data;
                }
            }
            return String.Empty;
        }

        private static async Task<string> postDataToRestAPI(string baseAddress, string path, object json)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonInString = JsonConvert.SerializeObject(json);

                // HTTP POST
                HttpResponseMessage response = await client.PostAsync("api/products", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    Uri jsonUrl = response.Headers.Location;

                    //// HTTP PUT
                    //gizmo.Price = 80;   // Update price
                    //response = await client.PutAsync(jsonUrl, json);

                    //// HTTP DELETE
                    //response = await client.DeleteAsync(gizmoUrl);
                }
            }
            return String.Empty;
        }
    }
}
