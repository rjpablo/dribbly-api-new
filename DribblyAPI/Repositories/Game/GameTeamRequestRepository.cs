using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DribblyAPI.Repositories
{
    public class GameTeamRequestRepository : BaseRepository<GameTeamRequest>
    {
        public GameTeamRequestRepository(ApplicationDbContext _ctx) : base(_ctx)
        {

        }
    }
}