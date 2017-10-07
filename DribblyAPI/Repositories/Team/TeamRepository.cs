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
            return ctx.Set<FullDetailedTeam>().Include(t => t.creator).Include(t => t.manager).Include(t => t.city.country).Include(t => t.homeCourt).Where(t => !t.isTemporary);
        }

        public FullDetailedTeam getTeamByName(int teamId)
        {
            return ctx.Set<FullDetailedTeam>().Include(t => t.creator).Include(t => t.manager).Include(t => t.city.country).Include(t => t.homeCourt).FirstOrDefault(t => t.teamId == teamId);
        }

        public List<TeamMemberListItem> getMembers(int teamId)
        {
            using(var db = new ApplicationDbContext())
            {
                var members = db.TeamMemberListItems.AsQueryable().Where(m => m.teamId == teamId).ToList<TeamMemberListItem>();
                return members;
            }
            
        }

        public List<MemberRequestListItem> getMemberRequests(int teamId)
        {
            using (var db = new ApplicationDbContext())
            {
                var requests = db.JoinTeamRequestListItems.Where(r=>r.teamId == teamId).ToList();
                return requests;
            }
        }

        public List<MemberInvitationListItem> getMemberInvites(int teamId)
        {
            using (var db = new ApplicationDbContext())
            {
                var invites = db.JoinTeamInvitationListItems.Where(r => r.teamId == teamId).ToList();
                return invites;
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

        public IEnumerable<TeamGame> getTeamGames(int teamId)
        {
            var teamIdParam = new SqlParameter("@teamId", teamId);

            var result = ctx.Database
                .SqlQuery<TeamGame>("GetTeamGames  @teamId", teamIdParam);

            return result;
        }

        public bool addOrUpdateTeamPlayer(int teamId, string playerId)
        {
            using(TeamPlayerRepository tpRepo = new TeamPlayerRepository(new ApplicationDbContext()))
            {
                try
                {
                    TeamPlayer player;
                    TeamPlayer existingPlayer = tpRepo.FindSingleBy(p => p.playerId == playerId && p.teamId == teamId);

                    if(existingPlayer != null)
                    {
                        player = existingPlayer;
                        player.isCurrentMember = true;
                        player.dateJoined = DateTime.Now;
                        player.dateLeft = null;
                    }else
                    {
                        player = new TeamPlayer()
                        {
                            playerId = playerId,
                            teamId = teamId,
                            dateJoined = DateTime.Now,
                            isCurrentMember = true
                        };
                        tpRepo.Add(player);
                    }

                    tpRepo.Save();

                    return true;
                }
                catch (DribblyException ex)
                {
                    ex.UserMessage = "Failed to add player to team";
                    throw (ex);
                }
                
            }
        }

    }
}