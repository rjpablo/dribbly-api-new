using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DribblyAPI.Repositories
{
    public class GameRepository : BaseRepository<Game>
    {
        public GameRepository(DbContext _ctx) : base(_ctx)
        {

        }

        public List<Game> GetGames()
        {
            return ctx.Set<Game>().Include(g => g.teamA).Include(g => g.teamB).Include(g => g.court).ToList();
        }

        public Game GetGameDetails(int gameId)
        {
            return ctx.Set<Game>().Include(g => g.teamA).Include(g => g.teamB).Include(g => g.court).Where(g=>g.gameId == gameId).SingleOrDefault();
        }
    }
}