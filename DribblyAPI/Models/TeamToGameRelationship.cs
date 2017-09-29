using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class TeamToGameRelationship
    {
        public int teamId { get; set; }

        public int gameId { get; set; }

        /// <summary>
        /// Whether or not the team is playing in the game
        /// </summary>
        public bool isPlaying { get; set; }

        /// <summary>
        /// Whether or not the team has requested to join the game
        /// </summary>
        public bool hasRequested { get; set; }

        /// <summary>
        /// The date when the team sent a request to join the game
        /// </summary>
        public DateTime? dateRequested { get; set; }

        public bool isBanned { get; set; }
    }
}