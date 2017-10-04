using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class GameTeamRequestViewModel
    {
        public int id { get; set; }

        public int gameId { get; set; }

        public int teamId { get; set; }

        public string teamName { get; set; }

        public string teamLogoUrl { get; set; }

        public DateTime dateRequested { get; set; }

        /// <summary>
        /// Whether or not the team is ready, which also means that all of the team members are ready.
        /// </summary>
        public bool isReady { get; set; }
    }
}