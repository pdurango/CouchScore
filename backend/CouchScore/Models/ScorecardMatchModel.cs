using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class ScorecardMatch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }

        public ICollection<ScorecardMatchOption> ScorecardMatchOptions { get; set; }
        //public ICollection<User> Users { get; set; }
        public virtual Scorecard Scorecard { get; set; }

        public ScorecardMatch()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
