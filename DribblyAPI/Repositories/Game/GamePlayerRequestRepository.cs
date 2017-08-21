﻿using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DribblyAPI.Repositories
{
    public class GamePlayerRequestRepository : BaseRepository<GamePlayerRequest>
    {
        public GamePlayerRequestRepository(ApplicationDbContext _ctx) : base(_ctx)
        {

        }
    }
}