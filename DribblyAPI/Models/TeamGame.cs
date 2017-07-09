using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class TeamGame
    {
        public int gameId { get; set; }

        public int teamId { get; set; }

        public int opponentTeamId { get; set; }

        public string teamName { get; set; }

        public int? score { get; set; }

        public string opponentTeamName { get; set; }

        public int? opponentScore { get; set; }

        public string title { get; set; }

        public bool? isWon { get; set; }

        public int courtId { get; set; }

        public string courtName { get; set; }

        public DateTime schedule { get; set; }

    }
}