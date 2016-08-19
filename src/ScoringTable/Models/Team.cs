using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringTable.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Maze> Mazes { get; set; } = new List<Maze>();
        public int Sum => Mazes.Sum(s => s.Score);
    }
}
