using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class ScorecardMatchOption
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ScorecardMatch ScorecardMatch { get; set; }
        public virtual ICollection<ScorecardUserSelection> ScorecardUserSelections { get; set; }

        public ScorecardMatchOption()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
