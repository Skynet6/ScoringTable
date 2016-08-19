using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringTable.Models.DAL
{
    public class DALTeams
    {
        public Embedded _embedded { get; set; }
        public Links2 _links { get; set; }
        public Page page { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Team2
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Team2 team { get; set; }
    }

    public class Team
    {
        public string externalId { get; set; }
        public string description { get; set; }
        public int score { get; set; }
        public Links _links { get; set; }
    }

    public class Embedded
    {
        public List<Team> teams { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Profile
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public Self2 self { get; set; }
        public Profile profile { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }

}
