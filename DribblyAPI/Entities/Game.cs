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
        public bool isProtected { get; set; }

        public Court court { get; set; }

        public Team teamA { get; set; }

        public Team teamB { get; set; }

        public Team winningTeam { get; set; }

    }
}