using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CouchScore.DAL;
using CouchScore.Models;

namespace CouchScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScorecardsController : ControllerBase
    {
        private readonly ScorecardContext _context;

        public ScorecardsController(ScorecardContext context)
        {
            _context = context;
        }

        // GET: api/Scorecards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scorecard>>> GetScorecards()
        {
            return await _context.Scorecards.ToListAsync();
        }

        // GET: api/Scorecards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scorecard>> GetScorecard(int id)
        {
            var scorecard = await _context.Scorecards.FindAsync(id);

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
        public async Task<IActionResult> PutScorecard(int id, Scorecard scorecard)
        {
            if (id != scorecard.Id)
            {
                return BadRequest();
            }

            _context.Entry(scorecard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<Scorecard>> PostScorecard(Scorecard scorecard)
        {
            _context.Scorecards.Add(scorecard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScorecard", new { id = scorecard.Id }, scorecard);
        }

        // DELETE: api/Scorecards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Scorecard>> DeleteScorecard(int id)
        {
            var scorecard = await _context.Scorecards.FindAsync(id);
            if (scorecard == null)
            {
                return NotFound();
            }

            _context.Scorecards.Remove(scorecard);
            await _context.SaveChangesAsync();

            return scorecard;
        }

        private bool ScorecardExists(int id)
        {
            return _context.Scorecards.Any(e => e.Id == id);
        }
    }
}
