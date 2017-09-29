using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class JoinGameAsTeamCredentials
    {
        public string userId { get; set; }

        public int teamId { get; set; }

        public int gameId { get; set; }

        public string password { get; set; }
    }
}