using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class ScorecardMatch
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }

        public ICollection<ScorecardMatchOption> ScorecardMatchOptions { get; set; }
        //public ICollection<User> Users { get; set; }
        public virtual Scorecard Scorecard { get; set; }
    }
}
