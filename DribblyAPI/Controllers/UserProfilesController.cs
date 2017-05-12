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
    [RoutePrefix("api/UserProfiles")]
    public class UserProfilesController : ApiController
    {
        private ApplicationDbContext djb = new ApplicationDbContext();
        private UserProfileRepository _repo = new UserProfileRepository(new ApplicationDbContext());

        // GET: api/UserProfiles
        public IHttpActionResult GetUserProfiles()
        {
            return Ok(_repo.GetAll().ToList<UserProfile>());
        }

        [Route("{userName}")]
        [ResponseType(typeof(UserProfile))]
        public IHttpActionResult GetUserProfile(string userName)
        {
            try
            {
                UserProfile userProfile = _repo.FindSingleByUserName(userName);
                return Ok(userProfile);

            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Error loading profile details.";
                return InternalServerError(ex);
            }
        }

        /**

        // PUT: api/UserProfiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserProfile(string id, UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userProfile.userId)
            {
                return BadRequest();
            }

            db.Entry(userProfile).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
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

        // POST: api/UserProfiles
        [ResponseType(typeof(UserProfile))]
        public IHttpActionResult PostUserProfile(UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserProfiles.Add(userProfile);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserProfileExists(userProfile.userId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userProfile.userId }, userProfile);
        }

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(UserProfile))]
        public IHttpActionResult DeleteUserProfile(string id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            db.UserProfiles.Remove(userProfile);
            db.SaveChanges();

            return Ok(userProfile);
        }

        private bool UserProfileExists(string id)
        {
            return db.UserProfiles.Count(e => e.userId == id) > 0;
        }

        **/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}