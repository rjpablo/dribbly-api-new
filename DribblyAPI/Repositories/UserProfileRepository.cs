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

                return FindBy(u => u.userId == identityUser.Id).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}