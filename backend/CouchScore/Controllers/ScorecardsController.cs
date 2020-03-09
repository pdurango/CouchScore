using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CouchScore.DAL;
using CouchScore.Models;
using CouchScore.Helpers;

namespace CouchScore.Controllers
{
    [Route("api/scorecard")] //api/[controller]
    [ApiController]
    public class ScorecardsController : ControllerBase
    {
        private readonly ScorecardContext m_context;

        public ScorecardsController(ScorecardContext context)
        {
            m_context = context;
        }

        // GET: api/Scorecards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scorecard>>> GetScorecards()
        {
            return await m_context.Scorecards.ToListAsync();
        }

        /*
        // GET: api/Scorecards
        [HttpGet("strucure")]
        public Scorecard GetScorecardStructure()
        {
            List<ScorecardMatchOption> options = new List<ScorecardMatchOption>();
            ScorecardMatchOption option = new ScorecardMatchOption();
            options.Add(option);

            List<ScorecardMatch> matches = new List<ScorecardMatch>();
            ScorecardMatch match = new ScorecardMatch();
            match.ScorecardMatchOptions = options;
            matches.Add(match);

            Scorecard scorecard = new Scorecard();
            scorecard.ScorecardMatches = matches;
            
            return scorecard;
        }
        */
        // GET: api/Scorecards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scorecard>> GetScorecard(string id)
        {
            Scorecard scorecard = await m_context.Scorecards
            .Include(scorecard => scorecard.ScorecardMatches)
            .ThenInclude(scorecardMatches => scorecardMatches.ScorecardMatchOptions)
            .Where(scorecard => scorecard.Id == id)
            .FirstOrDefaultAsync();
            //Scorecard scorecard = await m_context.Scorecards.FindAsync(id);

            if (scorecard == null)
            {
                return NotFound();
            }

            return scorecard;
        }

        // PUT: api/Scorecards/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScorecard(string id, [FromBody] Scorecard scorecard)
        {
            if (id != scorecard.Id)
            {
                return BadRequest();
            }

            foreach (ScorecardMatch match in scorecard.ScorecardMatches)
            {
                if(match.Id.Equals(null))
                {
                    match.Scorecard = scorecard;
                    m_context.ScorecardMatches.Add(match);
                }
                else
                    m_context.ScorecardMatches.Update(match);

                foreach (ScorecardMatchOption option in match.ScorecardMatchOptions)
                {
                    if (option.Id.Equals(null))
                    {
                        option.ScorecardMatch = match;
                        m_context.ScorecardMatchOptions.Add(option);
                    }
                    else
                        m_context.ScorecardMatchOptions.Update(option);
                }
            }

            m_context.Entry(scorecard).State = EntityState.Modified;

            try
            {
                await m_context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScorecardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Scorecards
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<string>> PostScorecard()
        {
            RandomIdentifier random = new RandomIdentifier();
            Scorecard scorecard = new Scorecard() { Id = random.GenerateUniqueIdentifier(), CreatedDate = DateTime.Now};
           //ScorecardMatch scorecardMatch = new ScorecardMatch() {  }
            m_context.Scorecards.Add(scorecard);

            //////////////
            ScorecardMatch match = new ScorecardMatch();
            match.Scorecard = scorecard;
            m_context.ScorecardMatches.Add(match);

            List<ScorecardMatchOption> options = new List<ScorecardMatchOption>();
            ScorecardMatchOption op1 = new ScorecardMatchOption();
            ScorecardMatchOption op2 = new ScorecardMatchOption();
            op1.ScorecardMatch = match;
            op2.ScorecardMatch = match;
            options.Add(op1);
            m_context.ScorecardMatchOptions.Add(op1);
            options.Add(op2);
            m_context.ScorecardMatchOptions.Add(op2);
            //////////
            
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetScorecard", new { id = scorecard.Id }, scorecard);
        }

        // DELETE: api/Scorecards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteScorecard(string id)
        {
            Scorecard scorecard = await m_context.Scorecards.FindAsync(id);

            if (scorecard == null)
            {
                return NotFound();
            }

            List<ScorecardMatch> matches = m_context.ScorecardMatches
                .Where(match => match.Scorecard.Id == id).ToList();

            List<ScorecardMatchOption> options = new List<ScorecardMatchOption>();
            foreach (ScorecardMatch match in matches)
            {
                List<ScorecardMatchOption> matchOptions = m_context.ScorecardMatchOptions
                    .Where(o => o.ScorecardMatch.Id == match.Id).ToList();
                options.AddRange(matchOptions);
            }

            //Need to delete children in proper order (foreign key referenced)
            m_context.ScorecardMatchOptions.RemoveRange(options);
            m_context.ScorecardMatches.RemoveRange(matches);
            m_context.Scorecards.Remove(scorecard);
            await m_context.SaveChangesAsync();

            return Ok();
        }

        private bool ScorecardExists(string id)
        {
            return m_context.Scorecards.Any(e => e.Id == id);
        }
    }
}
