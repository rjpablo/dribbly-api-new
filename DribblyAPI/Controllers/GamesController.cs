using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DribblyAPI.Entities;
using DribblyAPI.Repositories;
using DribblyAPI.Models;

namespace DribblyAPI.Controllers
{
    [RoutePrefix("api/Games")]
    public class GamesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private GameRepository _repo = new GameRepository(new ApplicationDbContext());
        private GamePlayerRequestRepository _gamePlayerReqRepo = new GamePlayerRequestRepository(new ApplicationDbContext());
        private TeamRepository _teamRepo = new TeamRepository(new ApplicationDbContext());
        private GameTeamRepository _gameTeamRepo = new GameTeamRepository(new ApplicationDbContext());

        // GET: api/Games
        public IHttpActionResult GetGames()
        {
            
            try
            {
                return Ok(_repo.GetGames());
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to retrieve list of games. Please try again later.";
                return InternalServerError(ex);
            }
        }

        [Route("GetGameDetails/{gameId}")]
        [ResponseType(typeof(Game))]
        public IHttpActionResult GetGame(int gameId)
        {
            try
            {
                Game game = _repo.GetGameDetails(gameId);
                if (game == null)
                {
                    return InternalServerError(new DribblyException("Game details not found."));
                }

                return Ok(game);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to retrieve game details. Please try again later.";
                return InternalServerError(ex);
            }

        }

        [Route("JoinGameAsPlayer")]
        public IHttpActionResult JoinGameAsPlayer(JoinGameAsPlayerCredentials credentials)
        {
            try
            {
                int? otherTeamId = null; //the Id of the opponent team
                Team otherTeam = null; //the opponent team

                #region validations

                Game game = _repo.FindSingleBy(g => g.gameId == credentials.gameId);
                if (game == null)
                {
                    return BadRequest("Game does not exist. It may have been cancelled.");
                }

                if(game.teamAId == credentials.teamId && game.teamBId != null) //player is joining team A
                {
                    otherTeamId = game.teamBId;
                }else if (game.teamBId == credentials.teamId && game.teamAId != null) //player is joining team B
                {
                    otherTeamId = game.teamAId;
                }

                if(otherTeamId != null)
                {
                    otherTeam = _teamRepo.FindSingleBy(t => t.teamId == otherTeamId);
                }

                Team team = _teamRepo.FindSingleBy(t => t.teamId == credentials.teamId);
                if(team == null)
                {
                    return BadRequest("Team does not exist.");
                }

                #region Check the relationship of the player and the team he's joining

                UserToTeamRelation rel1 = _teamRepo.getUserToTeamRelation(credentials.teamId, credentials.playerId);
                if (rel1.isCurrentMember)
                {
                    return BadRequest("You're already a member of this team.");
                }

                if (!team.isTemporary)
                {
                    if (rel1.hasRequested)
                    {
                        return BadRequest("You have already sent a request to join this team awaiting approval.");
                    }
                    else if (rel1.isInvited)
                    {
                        //TODO: Provide an option for the user to accept the invite without going to team's page.
                        return BadRequest("You have already been invited to join this team. Go to the team's page to accept the invitation.");
                    }
                }

                #endregion

                #region Check the relationship of the player and the opponent team
                if(otherTeam != null)
                {
                    UserToTeamRelation rel = _teamRepo.getUserToTeamRelation(otherTeam.teamId, credentials.playerId);
                    if(rel!= null)
                    {
                        if (rel.isCurrentMember)
                        {
                            return BadRequest("You can't join this team because you are a member of the opponent team.");
                        }

                        if (!team.isTemporary)
                        {
                            if (rel.hasRequested)
                            {
                                return BadRequest(@"You have sent a request to join the opponent team.
                                You need to cancel the request before you can join this team.");
                            }
                        }
                    }                    
                }
                #endregion

                GameTeam gameTeam = _gameTeamRepo.FindSingleBy(gt => gt.gameId == credentials.gameId && gt.teamId == credentials.teamId);
                if(gameTeam == null)
                {
                    return BadRequest("Team is not registered to game.");
                }

                if (!team.isTemporary)
                {
                    return BadRequest("You have to go to this team's page to join.");
                }
                else if (team.requiresPassword && team.password != credentials.password)
                {
                    return BadRequest("Incorrect password");
                }

                #endregion validations

                //Add player to team members
                _teamRepo.addOrUpdateTeamPlayer(credentials.teamId, credentials.playerId);

                GamePlayer player = new GamePlayer();

                player.playerId = credentials.playerId;
                player.gameTeamId = gameTeam.gameTeamId;

                using(GamePlayerRepository gpRepo = new GamePlayerRepository(new ApplicationDbContext()))
                {
                    gpRepo.Add(player);
                    gpRepo.Save();
                }

                return Ok();

            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Something went wrong. Please try again later.";
                return InternalServerError(ex);
            }

        }

        [Route("GetUserToGameTeamRelation/{playerId}/{teamId}/{gameId}/")]
        public IHttpActionResult GetUserToGameTeamRelation(string playerId, int teamId, int gameId)
        {
            try
            {
                if(_repo.Exists(g=>g.gameId == gameId))
                {
                    UserToGameTeamRelation rel = _repo.getUserToGameTeamRelation(playerId, teamId, gameId);
                    if(rel == null)
                    {
                        return BadRequest("Team is not registered to game.");
                    }
                    return Ok(rel);
                }
                else
                {
                    return BadRequest("Game does not exist.");
                }
                
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Something went wrong please try again.";
                return InternalServerError(ex);
            }

        }

        [Route("leaveGameAsPlayer/{playerId}/{teamId}/{gameId}/")]
        public IHttpActionResult LeaveGameAsPlayer(string playerId, int teamId, int gameId)
        {
            try
            {
                Team team = _teamRepo.FindSingleBy(t => t.teamId == teamId);
                if (team == null)
                {
                    return BadRequest("Team does not exist.");
                }else if (!team.isTemporary)
                {
                    return BadRequest("To leave this team, go to its page and select the 'Leave' option.");
                }

                Game game = _repo.FindSingleBy(g => g.gameId == gameId);
                if (game == null)
                {
                    return BadRequest("Game does not exist. It may have been cancelled.");
                }

                GameTeam gameTeam = _gameTeamRepo.FindSingleBy(gt => gt.gameId == gameId && gt.teamId == teamId);
                if (gameTeam == null)
                {
                    return BadRequest("The team is not registered to this game.");
                }
                
                GamePlayer gamePlayer;

                using (GamePlayerRepository gpRepo = new GamePlayerRepository(new ApplicationDbContext()))
                {
                    gamePlayer = gpRepo.FindSingleBy(p => p.gameTeamId == gameTeam.gameTeamId && p.playerId == playerId);
                    if(gamePlayer != null)
                    {
                        gpRepo.Delete(gamePlayer);
                        TeamPlayer tp;
                        using (TeamPlayerRepository _tpRepo = new TeamPlayerRepository(new ApplicationDbContext()))
                        {
                            tp = _tpRepo.FindSingleBy(p => p.playerId == playerId && p.teamId == teamId);
                            if (tp != null)
                            {
                                _tpRepo.Delete(tp);
                                _tpRepo.Save();
                            }
                        }
                        gpRepo.Save();
                        if (tp == null)
                        {
                            return BadRequest("You are not a member of this team.");
                        }
                        return Ok();
                    }else
                    {
                        return BadRequest("You are not currently registered to this game.");
                    }
                }
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to cancel request.";
                return InternalServerError(ex);
            }
        }

        // PUT: api/Games/5
        [Route("Update/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGame(int id, Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.gameId)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Games
        [Route("Create")]
        public IHttpActionResult PostGame(NewGameViewModel gameVm)
        {
            Game game = new Game();
            game.allowedToJoinTeamA = gameVm.allowedToJoinTeamA;
            game.allowedToJoinTeamB = gameVm.allowedToJoinTeamB;
            game.courtId = gameVm.courtId;
            game.creatorId = gameVm.creatorId;
            game.dateCreated = gameVm.dateCreated;
            game.isProtected = gameVm.isProtected;
            game.password = gameVm.password;
            game.schedule = gameVm.schedule;
            game.title = gameVm.title;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(game.allowedToJoinTeamA == 0)
            { //If only individual players are allowed to join team A
                Team team = new Team();
                team.dateCreated = DateTime.Now;
                team.isTemporary = true;
                team.teamName = "(No name)";
                team.creatorId = game.creatorId;
                team.managerId = game.creatorId;
                team.isActive = true;

                if(gameVm.teamAPassword!=null && gameVm.teamAPassword.Trim() != "")
                {
                    team.requiresPassword = true;
                    team.password = gameVm.teamAPassword.Trim();
                }

                using(TeamRepository _tRepo = new TeamRepository(new ApplicationDbContext()))
                {
                    _tRepo.Add(team);
                    _tRepo.Save();
                }

                game.teamAId = team.teamId;

            }

            if (game.allowedToJoinTeamB == 0)
            { //If only individual players are allowed to join team B
                Team team = new Team();
                team.dateCreated = DateTime.Now;
                team.isTemporary = true;
                team.teamName = "(No name)";
                team.creatorId = game.creatorId;
                team.managerId = game.creatorId;
                team.isActive = true;

                if (gameVm.teamBPassword != null && gameVm.teamBPassword.Trim() != "")
                {
                    team.requiresPassword = true;
                    team.password = gameVm.teamBPassword.Trim();
                }

                using (TeamRepository _tRepo = new TeamRepository(new ApplicationDbContext()))
                {
                    _tRepo.Add(team);
                    _tRepo.Save();
                }
                
                game.teamBId = team.teamId;
            }

            try
            {
                _repo.Add(game);
                _repo.Save();

                if (game.allowedToJoinTeamA == 0 || game.allowedToJoinTeamB == 0)
                {
                    //Create entries in GameTeam table
                    using (GameTeamRepository _gtRepo = new GameTeamRepository(new ApplicationDbContext()))
                    {
                        if (game.allowedToJoinTeamA == 0)
                        {
                            GameTeam gtA = new GameTeam();
                            gtA.teamId = (int)game.teamAId;
                            gtA.gameId = game.gameId;
                            _gtRepo.Add(gtA);
                        }

                        if (game.allowedToJoinTeamB == 0)
                        {
                            GameTeam gtB = new GameTeam();
                            gtB.teamId = (int)game.teamBId;
                            gtB.gameId = game.gameId;
                            _gtRepo.Add(gtB);
                        }

                        _gtRepo.Save();

                    }
                }

                return Ok(game);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An internal error occurred while trying to save game details. Please try again later.";
                return InternalServerError(ex);
            }
        }

        [Route("Delete/{id:int}")]
        [ResponseType(typeof(Game))]
        public IHttpActionResult DeleteGame(int id)
        {
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            db.Games.Remove(game);
            db.SaveChanges();

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameExists(int id)
        {
            return db.Games.Count(e => e.gameId == id) > 0;
        }
    }
}