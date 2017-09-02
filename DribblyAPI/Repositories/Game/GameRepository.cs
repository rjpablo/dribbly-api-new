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

        public Game GetGameDetails(int gameId)
        {
            return ctx.Set<Game>().Include(g => g.teamA).Include(g => g.teamB).Include(g => g.court).Where(g=>g.gameId == gameId).SingleOrDefault();
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
    }
}