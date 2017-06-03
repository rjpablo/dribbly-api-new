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

        // GET: api/Games
        public IHttpActionResult GetGames()
        {
            
            try
            {
                return Ok(_repo.GetAll());
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to retrieve list of games. Please try again later.";
                return InternalServerError(ex);
            }
        }

        [Route("{id:int}")]
        [ResponseType(typeof(Game))]
        public IHttpActionResult GetGame(int id)
        {
            try
            {
                Game game = _repo.FindBy(g => g.gameId == id).SingleOrDefault();
                if (game == null)
                {
                    return NotFound();
                }

                return Ok(game);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to retrieve game details. Please try again later.";
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
        [Route("Add")]
        public IHttpActionResult PostGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repo.Add(game);
                return Ok();
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