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
    }
}