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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Globalization;
using CouchScore.Services;

/*Helpful Links
 * 
 * https://stackoverflow.com/questions/22680446/entity-framework-rollback-and-remove-bad-migration
 * 
 */
namespace CouchScore.Controllers
{
    [Authorize]
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class ScorecardsController : ControllerBase
    {
        private readonly DataContext m_context;
        private IUserService _userService;

        public ScorecardsController(DataContext context, IUserService userService)
        {
            m_context = context;
            _userService = userService;
        }

        // GET: api/Scorecards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scorecard>>> GetScorecards()
        {
            var userId = _userService.GetUserIdFromToken(this.User);
            return await m_context.Scorecards
                .Where(scorecard => scorecard.CreatedBy == userId)
                .ToListAsync();
        }

        // GET: api/Scorecards
        [HttpGet("linked")]
        public async Task<ActionResult<IEnumerable<Scorecard>>> GetScorecardsLinked()
        {
            var userId = _userService.GetUserIdFromToken(this.User);

            //Only selects queries that user has created, but misses others that they participate in
            /*
            List<Scorecard> scorecards = await m_context.Scorecards
                .Where(scorecard => scorecard.CreatedBy == userId)
                .ToListAsync();
            */

            var linkedScorecards = await m_context.Scorecards.FromSqlRaw(
                $"SELECT s.* FROM Scorecard s " +
                "INNER JOIN ScorecardLinkedUser slu ON s.Id = slu.ScorecardId " +
                "WHERE slu.UserId = @id", new Microsoft.Data.SqlClient.SqlParameter("@id", userId))
                .ToListAsync();
            return linkedScorecards;

            /*
             * If you've created a scorecards, you SHOULD be a linked member of said scorecard
             * so we can gaurantee that the creator will be in ScorecardLinkedUsers for that 
             * Scorecard */




            /*
            var linkedScorecards = await m_context.Scorecards.Join(
                m_context.ScorecardLinkedUsers,
                s => s.Id,
                slu => slu.Scorecard.Id,
                (scorecard, scorecardLinkedUser) => new
                {
                    Scorecard = scorecard,
                    ScorecardLinkedUser = scorecardLinkedUser
                })
                .Where(user => user.ScorecardLinkedUser.Id == userId)
                .Select(s => s.Scorecard)
                .ToListAsync<Scorecard>();


            return linkedScorecards;
            */
            /*
    var products = await m_context.Scorecards
    .Where(s => s.ScorecardLinkedUsers.All(slu => slu.User.Id == userId))
    .ToListAsync();
        return products;
    */


            /*
            var linkedScorecards = await m_context.Scorecards.Join(
                    m_context.ScorecardLinkedUsers,
                    s => s.Id,
                    slu => slu.Scorecard.Id,
                    (scorecard, scorecardLinkedUser) => new
                    {
                        Scorecard = scorecard,
                    })
                .Where(user => user.ScorecardLinkedUser.Id == userId);
            return linkedScorecards;
            */
        }

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
        public async Task<ActionResult<string>> PostScorecard([FromBody] Scorecard scorecard)
        {
            int userId = _userService.GetUserIdFromToken(this.User);
            scorecard.CreatedBy = userId;
            m_context.Scorecards.Add(scorecard);

            foreach (ScorecardMatch match in scorecard.ScorecardMatches)
            {
                m_context.ScorecardMatches.Add(match);

                foreach (ScorecardMatchOption option in match.ScorecardMatchOptions)
                    m_context.ScorecardMatchOptions.Add(option);
            }

            var user = m_context.Users.First(u => u.Id == userId);
            var linkedUser = new ScorecardLinkedUser() { Scorecard = scorecard, User = user };
            m_context.ScorecardLinkedUsers.Add(linkedUser);

            await m_context.SaveChangesAsync();

            return CreatedAtAction("PostScorecard", new { id = scorecard.Id });
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
