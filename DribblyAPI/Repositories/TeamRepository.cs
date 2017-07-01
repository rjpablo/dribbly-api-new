using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DribblyAPI.Entities;
using DribblyAPI.Models;
using System.Data.SqlClient;

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

        public List<TeamMemberListItem> getMembers(int teamId)
        {
            using(var db = new ApplicationDbContext())
            {
                var members = db.TeamMemberListItems.AsQueryable().Where(m => m.teamId == teamId).ToList<TeamMemberListItem>();
                return members;
            }
            
        }

        public UserToTeamRelation getUserToTeamRelation(int teamId, string userId)
        {
            var teamIdParam = new SqlParameter("@teamId", teamId);
            var userIdParam = new SqlParameter("@userId", userId);

            var result = ctx.Database
                .SqlQuery<UserToTeamRelation>("GetUserToTeamRelation @userId, @teamId", userIdParam, teamIdParam)
                .SingleOrDefault();

            return result;
        }

    }
}