using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Team : BaseEntity
    {
        private int _maxPlayers = 15; //must also update migration file if changing this value

        private bool _requiresPassword = false; ////must also update migration file if changing this value

        [Key]
        public int teamId { get; set; }

        [Required]
        [StringLength(30,MinimumLength = 5,ErrorMessage ="Team Name must be 5 to 30 characters long.")]
        public string teamName { get; set; }

        public bool isTemporary { get; set; }

        public string logoUrl { get; set; }

        public DateTime dateCreated { get; set; }
        
        [Required(ErrorMessage ="Creator is required.")]
        public string creatorId { get; set; }

        [Required(ErrorMessage = "Manager is required.")]
        public string managerId { get; set; }

        public bool isActive { get; set; }

        public int? cityId { get; set; }

        public int? homeCourtId { get; set; }

        public int? coachId { get; set; }

        /// <summary>
        /// Maximum no. of players. Default value: 15
        /// </summary>
        public int maxPlayers
        {
            get { return _maxPlayers; }
            set { _maxPlayers = value; }
        }

        /// <summary>
        /// An optional password that a player has to provide to send a request to join this team.
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Whether or not a password is required to send a request to join this team.
        ///  Default value: false
        /// </summary>
        public bool requiresPassword
        {
            get { return _requiresPassword; }
            set { _requiresPassword = value; }
        }

        public ICollection<TeamPlayer> players { get; set; }

        public ICollection<GameTeam> gameTeams { get; set; }

    }
}