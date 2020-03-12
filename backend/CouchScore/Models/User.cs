using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchScore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ScorecardLinkedUser> ScorecardLinkedUsers { get; set; }
    }
}
