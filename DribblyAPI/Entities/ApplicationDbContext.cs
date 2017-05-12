using DribblyAPI.Entities;
using DribblyAPI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DribblyAPI
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext()
            : base("AuthContext")
        {
            
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public System.Data.Entity.DbSet<DribblyAPI.Entities.PlayerProfile> PlayerProfiles { get; set; }
    }
}