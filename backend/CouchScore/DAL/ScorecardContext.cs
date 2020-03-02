using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CouchScore.Models;

namespace CouchScore.DAL
{
    public class ScorecardContext : DbContext
    {
        public ScorecardContext(DbContextOptions<ScorecardContext> options) : base(options)
        {
        }

        public DbSet<Scorecard> Scorecards { get; set; }
        public DbSet<ScorecardMatch> ScorecardMatches { get; set; }
        public DbSet<ScorecardMatchOption> ScorecardMatchOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
        }
    }
}
