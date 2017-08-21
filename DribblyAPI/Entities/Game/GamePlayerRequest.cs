using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    /// <summary>
    /// Represent a request that is sent by a player to join a game.
    /// </summary>
    public class GamePlayerRequest:BaseEntity
    {
        [Key]
        public int id { get; set; }

        public string playerId { get; set; }

        public int gameId { get; set; }

        /// <summary>
        /// The id of the team that the user wants to join.
        /// </summary>
        public int teamId { get; set; }

        /// <summary>
        /// The date and time when the user sent this request.
        /// </summary>
        public DateTime dateRequested { get; set; }

        /// <summary>
        /// Whether or not the user is banned from sending a request to join the game.
        /// </summary>
        public bool isBanned { get; set; }
    }
}