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
        public DbSet<PlayerListItem> PlayerListItem { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }
        public DbSet<GameTeam> GameTeams { get; set; }
        public DbSet<FullDetailedTeam> FullDetailedTeams { get; set; }
        public DbSet<UserView> UserViews { get; set; }

        public System.Data.Entity.DbSet<DribblyAPI.Entities.PlayerProfile> PlayerProfiles { get; set; }
    }
}