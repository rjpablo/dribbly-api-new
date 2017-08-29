using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    /// <summary>
    /// A class that represents a user's relation to a game.
    /// </summary>
    public class UserToGameRelationship
    {

        public int gameId { get; set; }

        public string userId { get; set; }

        /// <summary>
        /// Whether or not the user is the creator of the game.
        /// </summary>
        public bool isCreator { get; set; }

        /// <summary>
        /// Whether or not the user has requested to join the game.
        /// </summary>
        public bool hasRequested { get; set; }

        /// <summary>
        /// Whether or not the user has been kicked out of the game. In which case,
        /// the user will not be allowed to join the game any more.
        /// </summary>
        public bool isKicked { get; set; }

        /// <summary>
        /// Whether or not the user has been approved to play in the game.
        /// </summary>
        public bool isPlaying { get; set; }

    }
}