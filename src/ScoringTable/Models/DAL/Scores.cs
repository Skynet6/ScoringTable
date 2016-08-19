using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringTable.Models.DAL
{
    public class Maze
    {
        public string mazeId { get; set; }
        public int score { get; set; }
    }

    public class Scores
    {
        public List<Maze> mazes { get; set; }
    }
}
