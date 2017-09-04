using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DribblyAPI.Entities;

namespace DribblyAPI.Repositories
{
    public class GamePlayerRepository : BaseRepository<GamePlayer>
    {
        public GamePlayerRepository(ApplicationDbContext _ctx) : base(_ctx)
        {
        }
    }
}