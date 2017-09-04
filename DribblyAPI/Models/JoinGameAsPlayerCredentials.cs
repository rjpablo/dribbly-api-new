using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class JoinGameAsPlayerCredentials
    {
        public string playerId { get; set; }

        public int teamId { get; set; }

        public int gameId { get; set; }

        public string password { get; set; }

    }
}