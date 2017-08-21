using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    /// <summary>
    /// Represents a request sent by a team to join a game.
    /// </summary>
    public class GameTeamRequest:BaseEntity
    {
        [Key]
        public int id { get; set; }

        public int gameId { get; set; }

        public int teamId { get; set; }

        public DateTime dateRequested { get; set; }

        /// <summary>
        /// Whether or not the team is banned from sending a request to join the game.
        /// </summary>
        public bool isBanned { get; set; }

    }
}