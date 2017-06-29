using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DribblyAPI.Entities;

namespace DribblyAPI.Repositories
{
    public class TeamRepository : BaseRepository<Team>
    {
        public TeamRepository(ApplicationDbContext _ctx) : base(_ctx)
        {
        }

        public IEnumerable<FullDetailedTeam> getFullDetailedTeams()
        {
            return ctx.Set<FullDetailedTeam>().Include(t=> t.creator).Include(t=>t.manager).Include(t => t.city.country).Include(t => t.homeCourt);
        }

        public FullDetailedTeam getTeamByName(string teamName)
        {
            return ctx.Set<FullDetailedTeam>().Include(t => t.creator).Include(t => t.manager).Include(t => t.city.country).Include(t => t.homeCourt).FirstOrDefault(t => t.teamName == teamName);
        }
    }
}