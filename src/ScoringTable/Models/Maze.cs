using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringTable.Models
{
    public class Maze
    {
        public string Id { get; set; }
        public bool BestTeam { get; set; }
        public bool WorstTeam { get; set; }
        public int Score { get; set; }
        public bool Solved { get; set; }    
    }
}
