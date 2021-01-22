using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CouchScore.Models;

namespace CouchScore.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        /*
         * Scorecards
         */
        public DbSet<Scorecard> Scorecards { get; set; }
        public DbSet<ScorecardMatch> ScorecardMatches { get; set; }
        public DbSet<ScorecardMatchOption> ScorecardMatchOptions { get; set; }
        public DbSet<ScorecardLinkedUser> ScorecardLinkedUsers { get; set; }
        public DbSet<ScorecardUserSelection> ScorecardUserSelections { get; set; }

        /*
         * Users
         */
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
        }
    }
}
