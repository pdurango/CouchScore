using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class ScorecardMatchOption
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ScorecardMatch ScorecardMatch { get; set; }
    }
}
