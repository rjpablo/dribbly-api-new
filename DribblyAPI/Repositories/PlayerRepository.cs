using DribblyAPI.Entities;
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
    }
}