using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringTable.Models.DAL.SolvedMazes
{
    public class SolvedMazes
    {
        public Embedded _embedded { get; set; }
        public Links2 _links { get; set; }
        public Page page { get; set; }
    }
    public class Self
    {
        public string href { get; set; }
    }

    public class SolvedMaze2
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public SolvedMaze2 solvedMaze { get; set; }
    }

    public class SolvedMaze
    {
        public string teamId { get; set; }
        public string mazeId { get; set; }
        public Links _links { get; set; }
    }

    public class Embedded
    {
        public List<SolvedMaze> solvedMazes { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Profile
    {
        public string href { get; set; }
    }

    public class Search
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public Self2 self { get; set; }
        public Profile profile { get; set; }
        public Search search { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }
}
