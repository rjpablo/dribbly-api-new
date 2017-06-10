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
using DribblyAPI.Repositories;
using DribblyAPI.Entities;
using DribblyAPI.Models;

namespace DribblyAPI.Controllers
{
    public class TeamsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TeamRepository _repo = new TeamRepository(new ApplicationDbContext());

        // GET: api/Teams
        public IHttpActionResult GetTeams()
        {
            try
            {
                var teams = _repo.getFullDetailedTeams();
                return Ok(teams);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Could not retrieve list of teams. Please try again later.";
                return InternalServerError(ex);
            }
        }

        // GET: api/Teams/5
        [Route("GetTeam/{id:int}")]
        public IHttpActionResult GetTeam(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // PUT: api/Teams/5
        [Route("Update/{id:int}")]
        public IHttpActionResult PutTeam(int id, Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.teamId)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Teams
        [Route("Create")]
        public IHttpActionResult PostTeam(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = team.teamId }, team);
        }

        // DELETE: api/Teams/5
        [Route("Delete/{id:int}")]
        public IHttpActionResult DeleteTeam(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int id)
        {
            return db.Teams.Count(e => e.teamId == id) > 0;
        }
    }
}