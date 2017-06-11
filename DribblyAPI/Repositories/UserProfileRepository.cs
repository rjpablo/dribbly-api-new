using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DribblyAPI.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DribblyAPI.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfile>
    {
        public UserProfileRepository(ApplicationDbContext _ctx) : base(_ctx)
        {

        }   

        public IEnumerable<UserProfile> FindByUserName(string userName)
        {
            var identityUser = ctx.Set<IdentityUser>().SingleOrDefault<IdentityUser>();

            return FindBy(u => u.userId == identityUser.Id);
        }

        public UserProfile FindSingleByUserName(string userName)
        {
            try
            {
                var identityUser = ctx.Set<IdentityUser>().SingleOrDefault<IdentityUser>(u=>u.UserName == userName);
                var allUsers = ctx.Set<IdentityUser>().ToList<IdentityUser>();

                return ctx.Set<UserProfile>().Where(u=>u.userId == identityUser.Id).Include(u=>u.profilePic).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(string userId)
        {
            return ctx.Set<UserProfile>().Count(u => u.userId == userId) > 0;
        }

        public void UpdateProfilePic(string userId, int profilePicId)
        {
            try
            {
                UserProfile p = ctx.Set<UserProfile>().SingleOrDefault(u => u.userId == userId);
                p.profilePicId = profilePicId;
                Edit(p);
                Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeletePhoto(string fileName, string userId)
        {
            try
            {
                UserPhoto photo = ctx.Set<UserPhoto>().SingleOrDefault(p => p.fileName == fileName && p.userId == userId);
                if(photo != null)
                {
                    ctx.Set<UserPhoto>().Remove(photo);
                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public MainProfileView GetMainProfileDetailsById(string userId)
        {
            try
            {
                MainProfileView prof = new MainProfileView() { userId = userId };

                prof.photos = ctx.Set<UserPhoto>().Where(p => p.userId == prof.userId);

                return prof;
            }
            catch (Exception)
            {

                throw;
            }            

        }

    }
}