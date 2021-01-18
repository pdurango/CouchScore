using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class ScorecardUserSelection
    {
        public int Id { get; set; }
        
        //public virtual Scorecard Scorecard { get; set; }
        //public virtual ScorecardMatch ScorecardMatch { get; set; }
        public virtual ScorecardMatchOption ScorecardMatchOption { get; set; }
        public virtual ScorecardLinkedUser ScorecardLinkedUser { get; set; }
    }
}
