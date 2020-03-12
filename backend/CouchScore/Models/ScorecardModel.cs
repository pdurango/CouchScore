using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class Scorecard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } //will act as url
        // public string URL { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public virtual ICollection<ScorecardLinkedUser> ScorecardLinkedUsers { get; set; }
        public virtual ICollection<ScorecardMatch> ScorecardMatches { get; set; }
    }
}
