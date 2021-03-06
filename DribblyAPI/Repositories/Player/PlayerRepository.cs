﻿using DribblyAPI.Entities;
using DribblyAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DribblyAPI.Repositories
{
    public class PlayerRepository : BaseRepository<PlayerProfile>
    {
        public PlayerRepository(ApplicationDbContext _ctx) : base(_ctx)
        {

        }

        public IEnumerable<PlayerProfile> GetTopPlayers(int count = 10)
        {
            IEnumerable<PlayerProfile> players;

            players = GetAll().Take(count);

            return players;

        }

        public PlayerProfile GetPlayerProfile(string userId)
        {
            try
            {
                PlayerProfile player = ctx.PlayerProfiles.SingleOrDefault(u => u.userId == userId);

                if (player != null)
                {
                    player.teams = ctx.TeamPlayers.Include(t=>t.team).Where(t =>t.playerId == player.userId && !t.team.isTemporary).ToList<TeamPlayer>();
                    player.games = (from g in ctx.Set<Game>()
                                    join t in ctx.Set<GameTeam>() on g.gameId equals t.gameId
                                    join p in ctx.Set<GamePlayer>() on t.gameTeamId equals p.gameTeamId
                                    where p.playerId == player.userId
                                    select g).ToList<Game>();
                }
                return player;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public IEnumerable<PlayerListItem> SearchPlayers(PlayerSearchCriteria criteria)
        {
            IEnumerable<PlayerListItem> players = ctx.Set<PlayerListItem>().Include(p=>p.profilePic).Include(p=>p.city.country);

            if (criteria != null)
            {

                if (criteria.playerName != null && criteria.playerName.Trim() != string.Empty)
                {
                    players = players.Where(u => u.userName.ToLower().Contains(criteria.playerName.ToLower()));
                }

                if (criteria.sex != null)
                {
                    players = players.Where(u => u.sex == criteria.sex);
                }

                if (criteria.heightFtMin != null && criteria.heightInMin != null)
                {
                    players = players.Where(u => u.heightFt >= criteria.heightFtMin && u.heightIn >= criteria.heightInMin);
                }

                if (criteria.heightFtMax != null && criteria.heightInMax != null)
                {
                    players = players.Where(u => u.heightFt <= criteria.heightFtMax && u.heightIn <= criteria.heightInMax);
                }

                if (criteria.mvpsMin != null)
                {
                    players = players.Where(u => u.mvps >= criteria.mvpsMin);
                }

                if (criteria.mvpsMax != null)
                {
                    players = players.Where(u => u.mvps <= criteria.mvpsMax);
                }

                if (criteria.dribblingMin != null)
                {
                    players = players.Where(u => u.dribblingSkill >= criteria.dribblingMin);
                }

                if (criteria.dribblingMax != null)
                {
                    players = players.Where(u => u.dribblingSkill <= criteria.dribblingMax);
                }

                if (criteria.shootingMin != null)
                {
                    players = players.Where(u => u.shootingSkill >= criteria.shootingMin);
                }

                if (criteria.shootingMax != null)
                {
                    players = players.Where(u => u.shootingSkill <= criteria.shootingMax);
                }

                if (criteria.passingMin != null)
                {
                    players = players.Where(u => u.passingSkill >= criteria.passingMin);
                }

                if (criteria.passingMax != null)
                {
                    players = players.Where(u => u.passingSkill <= criteria.passingMax);
                }

                if (criteria.threePtsMin != null)
                {
                    players = players.Where(u => u.threePointSkill >= criteria.threePtsMin);
                }

                if (criteria.threePtsMax != null)
                {
                    players = players.Where(u => u.threePointSkill <= criteria.threePtsMax);
                }

                if (criteria.blockingMin != null)
                {
                    players = players.Where(u => u.blockingSkill >= criteria.blockingMin);
                }

                if (criteria.blockingMax != null)
                {
                    players = players.Where(u => u.blockingSkill <= criteria.blockingMax);
                }

                if (criteria.defenceMin != null)
                {
                    players = players.Where(u => u.defensiveSkill >= criteria.defenceMin);
                }

                if (criteria.defenceMax != null)
                {
                    players = players.Where(u => u.defensiveSkill <= criteria.defenceMax);
                }
            }

            return players;
        }
    }
}