using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class Scorecard
    {
        public int Id { get; set; }
        public string URL { get; set; }

        public ICollection<ScorecardMatch> ScorecardMatches { get; set; }
    }
}
