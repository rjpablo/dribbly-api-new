using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class GameResult
    {
        public int gameId{ get; set; }

        public int teamAScore { get; set; }

        public int teamBScore { get; set; }

        public int winningTeamId { get; set; }

    }
}