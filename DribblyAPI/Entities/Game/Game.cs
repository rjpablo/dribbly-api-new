using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Game : BaseEntity
    {
        #region Default Values (IMPORTANT: Update migration files accordingly when changing default values)

        private bool _isClosed = false;
        private bool _isOver = false;
        private bool _isProtected = false;
        private int _allowedToJoinTeamA = 0;
        private int _allowedToJoinTeamB = 0;

        #endregion

        [Key]
        public int gameId { get; set; }

        public string creatorId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage ="Title must be at least 5 characters.")]
        public string title { get; set; }

        [Required(ErrorMessage ="Please set game schedule.")]
        public DateTime schedule { get; set; }

        public int teamAScore { get; set; }

        public int teamBScore { get; set; }

        [Required(ErrorMessage = "Please select a court.")]
        [ForeignKey("court")]
        public int courtId { get; set; }

        [ForeignKey("teamA")]
        public int? teamAId { get; set; }

        [ForeignKey("teamB")]
        public int? teamBId { get; set; }

        [ForeignKey("winningTeam")]
        public int? winningTeamId { get; set; }

        public DateTime dateCreated { get; set; }

        [MaxLength(15)]
        public string password { get; set; }

        /// <summary>
        /// Whether the game is password-protected
        /// </summary>
        public bool isProtected
        {
            get { return _isProtected; }
            set { _isProtected = value; }
        }

        /// <summary>
        /// Whether or not the game has been closed.
        /// </summary>
        public bool isClosed
        {
            get { return _isClosed; }
            set { _isClosed = value; }
        }

        /// <summary>
        /// Whether or not the game is over.
        /// </summary>
        public bool isOver
        {
            get { return _isOver; }
            set { _isOver = value; }
        }

        public Court court { get; set; }

        public Team teamA { get; set; }

        public Team teamB { get; set; }

        public Team winningTeam { get; set; }

        public ICollection<GameTeam> gameTeams { get; set; }

        [NotMapped]
        public UserView creator { get; set; }

        /// <summary>
        /// Who is allowed to join Team A? Can be 0(individual players only) or 1(teams only)
        /// </summary>
        public int allowedToJoinTeamA
        {
            get { return _allowedToJoinTeamA; }
            set { _allowedToJoinTeamA = value; }
        }

        /// <summary>
        /// Who is allowed to join Team B? Can be 0(individual players only) or 1(teams only)
        /// </summary>
        public int allowedToJoinTeamB
        {
            get { return _allowedToJoinTeamB; }
            set { _allowedToJoinTeamB = value; }
        }

    }
}