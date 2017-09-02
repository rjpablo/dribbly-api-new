using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    /// <summary>
    /// Represents a user's relationship to a team that has joined a particular game.
    /// </summary>
    public class UserToGameTeamRelation
    {

        /// <summary>
        /// The id of the game.
        /// </summary>
        public int gameId { get; set; }

        #region About the Team

        /// <summary>
        /// The team's id.
        /// </summary>
        public int teamId { get; set; }

        /// <summary>
        /// Whether or not the team is a temporary team.
        /// </summary>
        public bool teamIsTemporary { get; set; }

        /// <summary>
        /// Whether or not the team is password protected.
        /// </summary>
        public bool teamRequiresPassword { get; set; }

        #endregion

        #region About the User

        /// <summary>
        /// The id of the user.
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Whether the user is the manager of the team.
        /// </summary>
        public bool isManager { get; set; }

        /// <summary>
        /// Whether the user is a current member of the team.
        /// </summary>
        public bool isCurrentMember { get; set; }

        /// <summary>
        /// Whether or not the user has been invited to join the game with the given team.
        /// Relevant only if the team is a temporary team.
        /// </summary>
        public bool isInvited { get; set; }

        /// <summary>
        /// Whether or not the user has requested to join the game with the given team.
        /// Relevant only if the team is a temporary team.
        /// </summary>
        public bool hasRequested { get; set; }

        /// <summary>
        /// Whether or not the user has been accepted to play in the game with the specified team.
        /// </summary>
        public bool isPlaying { get; set; }

        /// <summary>
        /// Whether or not the user is the creator of the game.
        /// </summary>
        public bool isGameCreator { get; set; }

        #endregion

    }
}