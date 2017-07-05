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
using DribblyAPI.Enums;

namespace DribblyAPI.Controllers
{
    [RoutePrefix("api/Teams")]
    public class TeamsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TeamRepository _repo = new TeamRepository(new ApplicationDbContext());
        private CityRepository _cityRepo = new CityRepository(new ApplicationDbContext());
        private TeamPlayerRepository _teamPlayerRepo = new TeamPlayerRepository(new ApplicationDbContext());
        private JoinTeamInvitationRepository _joinTeamInviteRepo = new JoinTeamInvitationRepository(new ApplicationDbContext());
        private JoinTeamRequestRepository _joinTeamRequestRepo = new JoinTeamRequestRepository(new ApplicationDbContext());

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

        [Route("GetMemberRequests/{teamId}")]
        public IHttpActionResult GetMemberRequests(int teamId)
        {
            try
            {
                List<MemberRequestListItem> members = _repo.getMemberRequests(teamId);
                return Ok(members);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Could not retrieve member requests.";
                return InternalServerError(ex);
            }
        }

        [Route("GetMemberInvitations/{teamId}")]
        public IHttpActionResult GetMemberInvitations(int teamId)
        {
            try
            {
                IEnumerable<MemberInvitationListItem> invites = _repo.getMemberInvites(teamId);
                return Ok(invites);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Could not retrieve member requests.";
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

        [Route("GetRequests/{teamId}")]
        public IHttpActionResult GetRequests(int teamId)
        {
            try
            {
                var requests = _joinTeamRequestRepo.FindBy(r => r.teamId == teamId);
                if (requests != null)
                {
                    return Ok(requests);
                }
                else
                {
                    return InternalServerError(new DribblyException("Team details not found"));
                }
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An error occurred while retrieving membership requests.";
                return InternalServerError(ex);
            }
        }

        [Route("GetUserToTeamRelation/{teamId}/{userId}")]
        public IHttpActionResult GetUserToTeamRelation(int teamId, string userId)
        {
            try
            {
                return Ok(_repo.getUserToTeamRelation(teamId, userId));
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "An unexpected error occurred.";
                return InternalServerError(ex);
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

        [Route("Invite")]
        public IHttpActionResult Invite(MemberInvitation invite)
        {
            try
            {
                UserToTeamRelation relation;
                string validationError = validateTeamAction(invite.teamId, invite.playerId, TeamActions.invite, out relation);
                if (validationError.Length > 0)
                {
                    return BadRequest(validationError);
                }

                invite.dateInvited = DateTime.Now;
                _joinTeamInviteRepo.Add(invite);
                _joinTeamInviteRepo.Save();
                return Ok(invite);

            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Sending invitation failed. Please try again.";
                return InternalServerError(ex);
            }
        }

        [Route("JoinTeam/{playerId}/{teamId}")]
        public IHttpActionResult Join(string playerId, int teamId)
        {
            try
            {
                UserToTeamRelation relation;
                string validationError = validateTeamAction(teamId, playerId, TeamActions.join, out relation);
                if (validationError.Length > 0)
                {
                    return BadRequest(validationError);
                }

                if (!relation.isOwner)
                {
                    MemberRequest request = new MemberRequest()
                    {
                        playerId = playerId,
                        teamId = teamId
                    };

                    request.dateRequested = DateTime.Now;
                    _joinTeamRequestRepo.Add(request);
                    _joinTeamRequestRepo.Save();
                    return Ok();
                }
                else
                { //if requestor is also the owner, add as player immediately
                    _repo.addOrUpdateTeamPlayer(teamId, playerId);
                    return Ok();
                }
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Sending request failed. Please try again.";
                return InternalServerError(ex);
            }
        }

        [Route("LeaveTeam/{playerId}/{teamId}")]
        public IHttpActionResult Leave(string playerId, int teamId)
        {
            try
            {
                UserToTeamRelation relation;
                string validationError = validateTeamAction(teamId, playerId, TeamActions.leave, out relation);
                if (validationError.Length > 0)
                {
                    return BadRequest(validationError);
                }

                TeamPlayer player = _teamPlayerRepo.FindSingleBy(p => p.playerId == playerId && p.teamId == teamId);
                player.isCurrentMember = false;
                player.dateLeft = DateTime.Now;

                _teamPlayerRepo.Edit(player);
                _teamPlayerRepo.Save();

                return Ok();
                
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to leave team. Please try again later.";
                return InternalServerError(ex);
            }
        }

        [Route("DismissPlayer/{playerId}/{teamId}")]
        public IHttpActionResult Dismiss(string playerId, int teamId)
        {
            try
            {
                UserToTeamRelation relation;
                string validationError = validateTeamAction(teamId, playerId, TeamActions.dismiss, out relation);
                if (validationError.Length > 0)
                {
                    return BadRequest(validationError);
                }

                TeamPlayer player = _teamPlayerRepo.FindSingleBy(p => p.playerId == playerId && p.teamId == teamId);
                player.isCurrentMember = false;
                player.dateLeft = DateTime.Now;

                _teamPlayerRepo.Edit(player);
                _teamPlayerRepo.Save();

                return Ok();

            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to dismiss player. Please try again later.";
                return InternalServerError(ex);
            }
        }

        [Route("CancelRequest/{playerId}/{teamId}")]
        public IHttpActionResult CancelRequest(string playerId, int teamId)
        {
            try
            {
                UserToTeamRelation relation;
                string validationError = validateTeamAction(teamId, playerId, TeamActions.cancelRequest, out relation);
                if (validationError.Length > 0)
                {
                    return BadRequest(validationError);
                }

                MemberRequest request = _joinTeamRequestRepo.FindSingleBy(i => i.teamId == teamId && i.playerId == playerId);

                _joinTeamRequestRepo.Delete(request);
                _joinTeamRequestRepo.Save();

                return Ok();
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to cancel request. Please try again.";
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("RespondToInvitation/{teamId}/{playerId}/{accept}")]
        public IHttpActionResult RespondToInvitation(int teamId, string playerId, bool accept)
        {
            try
            {
                UserToTeamRelation relation;
                string validationError = validateTeamAction(teamId, playerId, TeamActions.respondToInvitation, out relation);
                if (validationError.Length > 0)
                {
                    return BadRequest(validationError);
                }

                MemberInvitation invite = _joinTeamInviteRepo.FindSingleBy(i => i.teamId == teamId && i.playerId == playerId);

                if (accept)
                {
                    TeamPlayer p = new TeamPlayer()
                    {
                        dateJoined = DateTime.Now,
                        playerId = invite.playerId,
                        teamId = invite.teamId
                    };

                    _teamPlayerRepo.Add(p);
                    _teamPlayerRepo.Save();

                    //TODO: Notify team owner

                }
                else
                {
                    //TODO: Notify team owner
                }

                _joinTeamInviteRepo.Delete(invite);
                _joinTeamInviteRepo.Save();

                return Ok();

            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Sending invitation failed. Please try again.";
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("RespondToRequest/{teamId}/{playerId}/{approve}")]
        public IHttpActionResult RespondToRequest(int teamId, string playerId, bool approve)
        {
            try
            {
                UserToTeamRelation relation;
                string validationError = validateTeamAction(teamId, playerId, TeamActions.respondToRequest, out relation);
                if(validationError.Length > 0)
                {
                    return BadRequest(validationError);
                }

                MemberRequest request = _joinTeamRequestRepo.FindSingleBy(i => i.teamId == teamId && i.playerId == playerId);

                if (approve)
                {
                    if (!relation.isMember)
                    {
                        _repo.addOrUpdateTeamPlayer(teamId, playerId);
                    }
                    else
                    {
                        TeamPlayer player = _teamPlayerRepo.FindSingleBy(p => p.playerId == playerId && p.teamId == teamId);
                        player.isCurrentMember = true;
                        player.dateLeft = null;
                        player.dateJoined = DateTime.Now;
                        _teamPlayerRepo.Edit(player);
                        _teamPlayerRepo.Save();
                    }
                    
                    //TODO: Notify player
                }
                else
                {
                    //TODO: Notify player
                }

                _joinTeamRequestRepo.Delete(request);
                _joinTeamRequestRepo.Save();

                return Ok();

            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to process request. Please try again.";
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

        #region Helper Functions

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string validateTeamAction(int teamId, string userId, TeamActions action, out UserToTeamRelation r)
        {
            string errMsg = "";
            r = null;

            if (!_repo.Exists(t=>t.teamId == teamId))
            {
                return "Team does not exist";
            }

            if(!_teamPlayerRepo.Exists(p=>p.playerId == userId))
            {
                return "Player not found";
            }

            r = _repo.getUserToTeamRelation(teamId, userId);

            switch (action)
            {
                case TeamActions.join:
                    if (r.isCurrentMember)
                    {
                        return "You're already a member of this team.";
                    }
                    else if (r.hasRequested)
                    {
                        return "You have already sent a request to join this team.";
                    }
                    break;

                case TeamActions.invite:
                    if (r.isCurrentMember)
                    {
                        return "This player is already a member of this team.";
                    }
                    else if (r.isInvited)
                    {
                        return "You have already sent an invitation to this user.";
                    }
                    break;

                case TeamActions.respondToInvitation:
                    if (r.isCurrentMember)
                    {
                        return "This player is already a member of this team.";
                    }
                    else if (!r.isInvited)
                    {
                        return "No invitation was found. It might have been cancelled.";
                    }
                    break;

                case TeamActions.respondToRequest:
                    if (r.isCurrentMember)
                    {
                        return "This player is already a member of this team.";
                    }
                    else if (!r.hasRequested)
                    {
                        return "Request was not found. It might have been cancelled.";
                    }
                    break;

                case TeamActions.leave:
                    if (!r.isCurrentMember)
                    {
                        return "You are not currently a member of this team.";
                    }
                    break;

                case TeamActions.dismiss:
                    if (!r.isCurrentMember)
                    {
                        return "Player is not currently a member of this team.";
                    }
                    break;

                case TeamActions.block:

                    break;
            }

            return errMsg;
        }

        #endregion
    }
}