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

        [Route("JoinGameAsPlayer/{playerId}/{teamId}/{gameId}/")]
        public IHttpActionResult JoinGameAsPlayer(string playerId, int teamId, int gameId)
        {
            try
            {
                GamePlayerRequest request = _gamePlayerReqRepo.FindSingleBy(r => r.gameId == gameId && r.playerId == playerId);

                if(request != null)
                {
                    if (request.isBanned)
                    {
                        return BadRequest("You can't join this game because you are banned.");
                    }
                    else if (request.teamId != teamId)
                    {
                        return BadRequest("You can't join this game because you have requested to join the other team." +
                            " You need to cancel your first request before you can join this team.");
                    }
                    else
                    {
                        return BadRequest("You've already sent a request to join this game.");
                    }

                }else
                {
                    request = new GamePlayerRequest();
                    request.gameId = gameId;
                    request.playerId = playerId;
                    request.dateRequested = DateTime.Now;
                    request.isBanned = false;
                    request.teamId = teamId;

                    _gamePlayerReqRepo.Add(request);
                    _gamePlayerReqRepo.Save();

                    //TODO: Send notif to game creator
                }

                return Ok(request);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to send request.";
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

        [Route("CancelJoinGameAsPlayer/{playerId}/{teamId}/{gameId}/")]
        public IHttpActionResult CancelJoinGameAsPlayer(string playerId, int teamId, int gameId)
        {
            try
            {
                GamePlayerRequest request = _gamePlayerReqRepo.FindSingleBy(r => r.gameId == gameId && r.playerId == playerId && r.teamId == teamId);

                if (request != null)
                {
                    _gamePlayerReqRepo.Delete(request);
                    _gamePlayerReqRepo.Save();

                    //TODO: Send notif to game creator and team manager

                    return Ok(request);
                }
                else
                {
                    return BadRequest("You do not have a request to join this game.");
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
        public IHttpActionResult PostGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repo.Add(game);
                _repo.Save();
                return Ok(game);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An internal error occurred while trying to same game details. Please try again later.";
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