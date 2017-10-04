using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DribblyAPI.Models;
using System.Data.SqlClient;

namespace DribblyAPI.Repositories
{
    public class GameRepository : BaseRepository<Game>
    {
        public GameRepository(ApplicationDbContext _ctx) : base(_ctx)
        {

        }

        public List<Game> GetGames()
        {
            return ctx.Set<Game>().Include(g => g.teamA).Include(g => g.teamB).Include(g => g.court).ToList();
        }

        public void banUser(int gameId, string userId)
        {
            GameBannedUser user = ctx.Set<GameBannedUser>().SingleOrDefault(u => u.userId == userId && u.gameId == gameId);

            if (user == null)
            {
                user = new GameBannedUser();
                user.gameId = gameId;
                user.userId = userId;

                ctx.GameBannedUsers.Add(user);
            }
        }

        public Game GetGameDetails(int gameId)
        {
            Game game = ctx.Set<Game>().Include(g => g.teamA).Include(g => g.teamB).Include(g => g.court).Where(g => g.gameId == gameId).SingleOrDefault();
            game.creator = ctx.Set<UserView>().SingleOrDefault(u => u.userId == game.creatorId);
            return game;
        }

        public UserToGameTeamRelation getUserToGameTeamRelation(string userId, int teamId, int gameId)
        {
            try
            {
                var teamIdParam = new SqlParameter("@teamId", teamId);
                var userIdParam = new SqlParameter("@userId", userId);
                var gameIdParam = new SqlParameter("@gameId", gameId);

                var result = ctx.Database
                    .SqlQuery<UserToGameTeamRelation>("GetUserToGameTeamRelation @userId, @teamId, @gameId", userIdParam, teamIdParam, gameIdParam)
                    .SingleOrDefault();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public UserToGameRelationship getUserToGameRelation(string userId, int gameId)
        {
            try
            {
                var userIdParam = new SqlParameter("@userId", userId);
                var gameIdParam = new SqlParameter("@gameId", gameId);

                var result = ctx.Database
                    .SqlQuery<UserToGameRelationship>("GetUserToGameRelation @userId, @gameId", userIdParam, gameIdParam)
                    .SingleOrDefault();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<GameTeam> GetGameTeams(int gameId)
        {
            return ctx.GameTeams.Where(gt => gt.gameId == gameId).ToList<GameTeam>();
        }

        public List<GameTeamRequestViewModel> GetRequestingTeams(int gameId)
        {
            var requests = (from rq in ctx.Set<GameTeamRequest>()
                            join t in ctx.Set<Team>() on rq.teamId equals t.teamId
                            where(rq.gameId == gameId)
                            select new GameTeamRequestViewModel {
                                id = rq.id,
                                gameId = rq.gameId,
                                teamId = rq.teamId,
                                teamName = t.teamName,
                                teamLogoUrl = t.logoUrl,
                                dateRequested = rq.dateRequested,
                                isReady = rq.isReady
                            }).ToList<GameTeamRequestViewModel>();

            return requests;
        }
    }
}