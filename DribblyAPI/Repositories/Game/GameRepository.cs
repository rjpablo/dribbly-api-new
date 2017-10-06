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

        public string leaveGameAsTeam(int gameId, int teamId)
        {
            try
            {
                GameTeam team = ctx.GameTeams.Include(g=>g.gamePlayers).SingleOrDefault(t => t.gameId == gameId && t.teamId == teamId);

                if (team == null)
                {
                    return "Team is not playing in the game.";
                }

                Game game = FindSingleBy(g => g.gameId == gameId);

                if (game == null)
                {
                    return "Game details not found";
                }

                if (game.teamAId == teamId)
                {
                    game.teamAId = null;
                }
                else
                {
                    game.teamBId = null;
                }
                
                ctx.GameTeams.Remove(team);
                Edit(game);
                Save();

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public string ApproveJoinAsTeamRequest(int requestId)
        {
            try
            {
                GameTeamRequest request = null;

                request = ctx.GameTeamRequests.FirstOrDefault(r => r.id == requestId);

                if (request == null)
                {
                    return "Request details not found";
                }

                Game game = ctx.Games.FirstOrDefault(g => g.gameId == request.gameId);

                if (game == null)
                {
                    return "Game details not found";
                }

                GameTeam gameTeam = ctx.GameTeams.FirstOrDefault(t => t.gameId == request.gameId && t.teamId == request.teamId);

                if (gameTeam != null)
                {
                    ctx.GameTeamRequests.Remove(request);
                    ctx.SaveChanges();
                    return "Team is already playing in the game.";
                }
                else
                {
                    List<GameTeam> gameTeams = ctx.GameTeams.Where(t=>t.gameId ==request.gameId).ToList<GameTeam>();

                    if (gameTeams != null && gameTeams.Count == 2)
                    {
                        return "Cannot approve request. Two teams are already playing in the game.";
                    }

                    gameTeam = new GameTeam();
                    gameTeam.teamId = request.teamId;
                    gameTeam.gameId = request.gameId;

                    #region Add gamePlayer for each team member

                    List<GamePlayer> gamePlayers = new List<GamePlayer>();
                    List<TeamPlayer> teamMembers = ctx.TeamPlayers.Where(m=>m.teamId == request.teamId).ToList<TeamPlayer>();
                    foreach (TeamPlayer player in teamMembers)
                    {
                        GamePlayer gamePlayer = new GamePlayer()
                        {
                            playerId = player.playerId
                        };
                        gamePlayers.Add(gamePlayer);
                    }

                    gameTeam.gamePlayers = gamePlayers;

                    #endregion

                    if (game.teamAId == null)
                    {
                        game.teamAId = request.teamId;
                    }
                    else
                    {
                        game.teamBId = request.teamId;
                    }

                    Edit(game);
                    ctx.GameTeams.Add(gameTeam);
                    ctx.GameTeamRequests.Remove(request);
                    ctx.SaveChanges();

                    return "";

                }
                
            }
            catch (DribblyException ex)
            {
                throw (ex);
            }

        }
    }
}