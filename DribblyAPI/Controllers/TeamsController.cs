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
    [RoutePrefix("api/Teams")]
    public class TeamsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TeamRepository _repo = new TeamRepository(new ApplicationDbContext());
        private CityRepository _cityRepo = new CityRepository(new ApplicationDbContext());
        private TeamPlayerRepository _teamPlayerRepo = new TeamPlayerRepository(new ApplicationDbContext());

        // GET: api/Teams
        [Route("All")]
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
        [Route("Members/{teamId}")]
        public IHttpActionResult GetMembers(int teamId)
        {
            try
            {
                List<TeamMemberListItem> members = _repo.getMembers(teamId);
                return Ok(members);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Could not retrieve current members";
                return InternalServerError(ex);
            }
        }

        [Route("GetTeam/")]
        public IHttpActionResult GetTeam(string teamName)
        {
            teamName = Uri.UnescapeDataString(teamName);
            FullDetailedTeam team = _repo.getTeamByName(teamName);
            if (team != null)
            {
                return Ok(team);
            }
            else
            {
                return InternalServerError(new DribblyException("Team details not found"));
            }
        }

        // PUT: api/Teams/5
        [HttpPut]
        [Route("Update/")]
        public IHttpActionResult PutTeam(FullDetailedTeam tempTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Team team = _repo.FindBy(t => t.teamId == tempTeam.teamId).FirstOrDefault();

                if(team == null)
                {
                    return InternalServerError(new DribblyException("The team that you are trying to update was not found."));
                }

                City tmpCity = null;
                if (tempTeam.city != null)
                {
                    string cityErr = Helper.validateCity(tempTeam.city);
                    if (!string.IsNullOrEmpty(cityErr))
                    {
                        return BadRequest(cityErr);
                    }

                    tmpCity = _cityRepo.AddOrGet(tempTeam.city);
                    tempTeam.cityId = tmpCity.cityId;
                    tempTeam.city = null;
                }
                else
                {
                    tempTeam.cityId = null;
                }

                team.cityId = tempTeam.cityId;
                team.managerId = tempTeam.managerId;

                _repo.Edit(team);
                _repo.Save();
                return Ok(team);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An error occurred while trying to update team details. Please try again later.";
                return InternalServerError(ex);
            }
        }

        // POST: api/Teams
        [HttpPost]
        [Route("Register")]
        public IHttpActionResult PostTeam(Team team)
        {
            try
            {
                if(team != null)
                {
                    team.dateCreated = DateTime.Now;

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    db.Teams.Add(team);
                    db.SaveChanges();
                    return Ok(team);
                }
                else
                {
                    return BadRequest("No team details provided.");
                }
                
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An unexpected error occurred. Please try again later.";
                return InternalServerError(ex);
            }
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