using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class ScorecardLinkedUser
    {
        public int Id { get; set; }

        public virtual Scorecard Scorecard { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<ScorecardUserSelection> ScorecardUserSelections { get; set; }
    }
}
