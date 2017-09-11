using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{

    public class NewGameViewModel
    {

        public string creatorId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters.")]
        public string title { get; set; }

        [Required(ErrorMessage = "Please set game schedule.")]
        public DateTime schedule { get; set; }

        [Required(ErrorMessage = "Please select a court.")]
        public int courtId { get; set; }

        public DateTime dateCreated { get; set; }

        /// <summary>
        /// The password required for a team to join the game
        /// </summary>
        [MaxLength(15)]
        public string password { get; set; }

        /// <summary>
        /// Whether the game is password-protected
        /// </summary>
        public bool isProtected { get; set; }

        /// <summary>
        /// Who is allowed to join Team A? Can be 0(individual players only) or 1(teams only)
        /// </summary>
        public int allowedToJoinTeamA { get; set; }

        /// <summary>
        /// Who is allowed to join Team B? Can be 0(individual players only) or 1(teams only)
        /// </summary>
        public int allowedToJoinTeamB { get; set; }
        
        /// <summary>
        /// The password that a player needs to provide to join team A.
        /// </summary>
        public string teamAPassword { get; set; }

        /// <summary>
        /// The password that a player needs to provide to join team B.
        /// </summary>
        public string teamBPassword { get; set; }

    }
}