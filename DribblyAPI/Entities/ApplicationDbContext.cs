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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /* PlayerListItem */
            //modelBuilder.Entity<PlayerListItem>().Property(p => p.profilePicId).IsOptional();

            /* FullDetailedTeam */
            //modelBuilder.Entity<FullDetailedTeam>().Property(t => t.cityId).IsOptional();
            //modelBuilder.Entity<FullDetailedTeam>().Property(t => t.coachId).IsOptional();
            //modelBuilder.Entity<FullDetailedTeam>().Property(t => t.homeCourtId).IsOptional();

            base.OnModelCreating(modelBuilder);
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
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<TeamMemberListItem> TeamMemberListItems { get; set; }
        public DbSet<MemberInvitation> JoinTeamInvitations { get; set; }
        public DbSet<MemberRequest> JoinTeamRequests { get; set; }
        public DbSet<MemberRequestListItem> JoinTeamRequestListItems { get; set; }
        public DbSet<MemberInvitationListItem> JoinTeamInvitationListItems { get; set; }
        public DbSet<GamePlayerRequest> GamePlayerRequests { get; set; }
        public DbSet<GameTeamRequest> GameTeamRequests { get; set; }
        public DbSet<UserBlockedUser> UserBlockedUsers { get; set; }
        public DbSet<GameBannedUser> GameBannedUsers { get; set; }
        public DbSet<TeamBannedPlayer> TeamBannedPlayers { get; set; }
        public DbSet<TeamFollower> TeamFollowers { get; set; }
        public DbSet<GameFollower> GameFollowers { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }


        public System.Data.Entity.DbSet<DribblyAPI.Entities.PlayerProfile> PlayerProfiles { get; set; }
    }
}