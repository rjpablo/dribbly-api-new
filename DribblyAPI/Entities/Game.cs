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

        public virtual Court court { get; set; }

        public virtual Team teamA { get; set; }

        public virtual Team teamB { get; set; }

        public virtual Team winningTeam { get; set; }

    }
}