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
using Microsoft.AspNet.Identity.EntityFramework;

namespace DribblyAPI.Controllers
{
    [RoutePrefix("api/UserProfiles")]
    public class UserProfilesController : ApiController
    {
        private ApplicationDbContext djb = new ApplicationDbContext();
        private UserProfileRepository _repo = new UserProfileRepository(new ApplicationDbContext());
        private CityRepository _cityRepo = new CityRepository(new ApplicationDbContext());
        private FileRepository _fileRepo = new FileRepository();
        private UserPhotoRepository _userPhotoRepo = new UserPhotoRepository(new ApplicationDbContext());
        private UserViewRepository _userViewRepo = new UserViewRepository(new ApplicationDbContext());
        private TeamRepository _teamRepo = new TeamRepository(new ApplicationDbContext());

        // GET: api/UserProfiles
        public IHttpActionResult GetUserProfiles()
        {
            return Ok(_repo.GetAll().ToList<UserProfile>());
        }

        /// <summary>
        /// The method used to get list of users for autocompletion
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [Route("UserViews/{userName}")]
        public IHttpActionResult GetUserViews(string userName)
        {
            try
            {
                return Ok(_userViewRepo.FindBy(u => u.userName.Contains(userName)));
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to retrieve user list.";
                return InternalServerError(ex);
            }
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

        [Route("GetManagedTeams/{userId}")]
        public IHttpActionResult GetManagedTeams(string userId)
        {
            try
            {
                IEnumerable<Team> managedTeams = _teamRepo.FindBy(t => t.managerId == userId);
                return Ok(managedTeams);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to retrieve managed teams.";
                return InternalServerError(ex);
            }
        }

        // PUT: api/UserProfiles/5
        [Route("Update")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserProfile(UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(userProfile.city != null)
            {
                if (userProfile.city.longName == "" || userProfile.city.shortName == "")
                {
                    return BadRequest("invalid city");
                }

                if (userProfile.city.country != null)
                {
                    if (userProfile.city.country.longName == "" || userProfile.city.country.shortName == "")
                    {
                        return BadRequest("city has invalid country details");
                    }
                }else
                {
                    return BadRequest("country is missing");
                }
            }
            else
            {
                userProfile.cityId = null;
            }

            try
            {
                City tmpCity = null;
                if (userProfile.city != null)
                {
                    tmpCity = _cityRepo.AddOrGet(userProfile.city);
                    userProfile.cityId = tmpCity.cityId;
                    userProfile.city = null;
                }else
                {
                    userProfile.cityId = null;
                }
                
                _repo.Edit(userProfile);
                _repo.Save();
                userProfile.city = tmpCity;
            }
            catch (DribblyException ex)
            {
                if (!_repo.Exists(userProfile.userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(userProfile);
        }

        [HttpPut]
        [Route("UpdateProfilePic/{userId}/{photoId}")]
        public IHttpActionResult UpdatePrimaryPhoto(string userId, int photoId)
        {
            try
            {
                _repo.UpdateProfilePic(userId, photoId);
                return Ok();
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to update primary photo";
                return InternalServerError(ex);
            }
        }

        [Route("GetMainProfile/{userId}")]
        [ResponseType(typeof(UserProfile))]
        public IHttpActionResult GetMainProfile(string userId)
        {
            try
            {
                MainProfileView userProfile = _repo.GetMainProfileDetailsById(userId);
                return Ok(userProfile);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Error loading main profile.";
                return InternalServerError(ex);
            }
        }

        [Route("UploadPhoto/{userId}/{setAsProfilePic?}")]
        public IHttpActionResult UploadProfilePic([FromUri]string userId, [FromUri] bool setAsProfilePic = false)
        {
            string uploadedFileName = "";

            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

            if (files.Count > 0)
            {
                try
                {
                    uploadedFileName = _fileRepo.Upload(files[0], userId + "/photos/");
                    UserPhoto photo = new UserPhoto() { fileName = uploadedFileName, userId = userId, uploadDate = DateTime.Now };
                    _userPhotoRepo.Add(photo);
                    _userPhotoRepo.Save();
                    if (setAsProfilePic)
                    {
                        _repo.UpdateProfilePic(userId, photo.id);
                    }
                    return Ok(photo);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            else
            {
                return BadRequest("No files to upload.");
            }
        }

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