using CouchScore.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class Scorecard
    {
        public string Id { get; set; } //will act as url
                                       // public string URL { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ScorecardLinkedUser> ScorecardLinkedUsers { get; set; }
        public virtual ICollection<ScorecardMatch> ScorecardMatches { get; set; }

        public Scorecard()
        {
            RandomIdentifier random = new RandomIdentifier();
            this.Id = random.GenerateUniqueIdentifier(); //probable overkill, can 
            this.CreatedDate = DateTime.Now;
        }
    }
}
    