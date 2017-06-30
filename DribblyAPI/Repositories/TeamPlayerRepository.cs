using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Repositories
{
    public class TeamPlayerRepository:BaseRepository<TeamPlayer>
    {
        public TeamPlayerRepository(ApplicationDbContext _ctx) : base(_ctx)
        {

        }
    }
}