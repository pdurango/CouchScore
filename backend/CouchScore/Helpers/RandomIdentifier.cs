using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouchScore.DAL;

namespace CouchScore.Helpers
{
    public class RandomIdentifier
    {
        /*
        public readonly ScorecardContext m_context;
        public RandomIdentifier(ScorecardContext context)
        {
            m_context = context;
        }
        */
        public string GenerateUniqueIdentifier()
        {
            string guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            guidString = guidString.Replace("=", "", StringComparison.OrdinalIgnoreCase);
            guidString = guidString.Replace("+", "", StringComparison.OrdinalIgnoreCase);
            guidString = guidString.Replace("/", "", StringComparison.OrdinalIgnoreCase);

            return guidString;

            /*Shouldn't really need any of the bottom part, can be removed
            for (int i = 0; i < 10; i++)
            {
                if (IsIdentifierUnique(uniqueIdentifer))
                    return uniqueIdentifer;
            }
            return "";
            */
        }

        /*
        public bool IsIdentifierUnique(string id)
        {
            int count = m_context.Scorecards.Count(u => u.URL == id);
            return count == 0;
        }
        */
    }
}
