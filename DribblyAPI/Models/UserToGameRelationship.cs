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

        #region About the game

        public int gameId { get; set; }

        public bool gameIsOver { get; set; }

        /// <summary>
        /// How many teams has joined the game
        /// </summary>
        public int teamCount { get; set; }

        #endregion

        #region About the user

        public string userId { get; set; }

        /// <summary>
        /// Whether or not the user is the creator of the game.
        /// </summary>
        public bool isCreator { get; set; }

        /// <summary>
        /// Whether or not the user has requested to join the game as a team.
        /// </summary>
        public bool hasRequestedAsTeam { get; set; }

        /// <summary>
        /// Whether or not the user has been banned from the game. If he is,
        /// the user will not be allowed to join the game any more.
        /// </summary>
        public bool isBanned { get; set; }

        /// <summary>
        /// Whether or not the user has been approved to play in the game.
        /// </summary>
        public bool isPlaying { get; set; }

        /// <summary>
        /// Whether or not the user is following the game
        /// </summary>
        public bool isFollowing { get; set; }

        /// <summary>
        /// Whether or not the user manages one or more teams.
        /// </summary>
        public bool managesTeam { get; set; }

        /// <summary>
        /// Whether or not a team that is managed by the user is playing in the game
        /// </summary>
        public bool managedTeamIsPlaying { get; set; }

        #endregion

    }
}