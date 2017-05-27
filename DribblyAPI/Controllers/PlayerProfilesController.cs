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
using DribblyAPI;
using DribblyAPI.Entities;
using DribblyAPI.Repositories;
using DribblyAPI.Models;

namespace DribblyAPI.Controllers
{
    [RoutePrefix("api/Players")]
    public class PlayersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private PlayerRepository repo = new PlayerRepository( new ApplicationDbContext());

        // GET: api/Players
        public IHttpActionResult GetPlayerProfiles()
        {
            try
            {
                return Ok(repo.GetAll());
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An error occurred while retrieving player list.";
                return InternalServerError(ex);
            }
        }

        [Route("GetProfile/{userId}")]
        [ResponseType(typeof(PlayerProfile))]
        public IHttpActionResult GetPlayerProfile(string userId)
        {
            try
            {
                return Ok(repo.FindBy(u => u.userId == userId).SingleOrDefault());
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An error occurred while retrieving player profile.";
                return InternalServerError(ex);
            }
        }

        [Route("Search")]
        public IHttpActionResult SearchPlayers()
        {
            try
            {
                return Ok(repo.SearchPlayers());
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An error occurred while searching players.";
                return InternalServerError(ex);
            }
        }

        [Route("TopPlayers/{count}")]
        //[ResponseType(typeof(PlayerProfile))]
        public IHttpActionResult GetTopPlayers(int count = 10)
        {
            try
            {
                return Ok(repo.GetTopPlayers(count));
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An error occurred while top players.";
                return InternalServerError(ex);
            }
        }

        [Route("Update")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayerProfile(string id, PlayerProfile playerProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerProfile.userId)
            {
                return BadRequest();
            }

            db.Entry(playerProfile).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerProfileExists(id))
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

        [Route("Add")]
        [ResponseType(typeof(PlayerProfile))]
        public IHttpActionResult PostPlayerProfile(PlayerProfile playerProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Add(playerProfile);

            try
            {
                repo.Save();

                return Ok(playerProfile);
            }
            catch (DribblyException ex)
            {
                if(repo.FindBy(p=>p.userId == playerProfile.userId).Count() > 0)
                {
                    ex.UserMessage = "Player profile already exists";
                }
                else
                {
                    ex.UserMessage = "An error occurred while retrieving player profile.";
                }

                return InternalServerError(ex);
            }
        }

        [Route("Delete/{id}")]
        [ResponseType(typeof(PlayerProfile))]
        public IHttpActionResult DeletePlayerProfile(string id)
        {
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);
            if (playerProfile == null)
            {
                return NotFound();
            }

            db.PlayerProfiles.Remove(playerProfile);
            db.SaveChanges();

            return Ok(playerProfile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerProfileExists(string id)
        {
            return db.PlayerProfiles.Count(e => e.userId == id) > 0;
        }
    }
}