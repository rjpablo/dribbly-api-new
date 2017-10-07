using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    /// <summary>
    /// A class that represent a team's game.
    /// Properties like remaining timeouts, score and coachId will be added here
    /// </summary>
    public class GameTeam:BaseEntity
    {
        [Key]
        public int gameTeamId { get; set; }

        [ForeignKey("game")]
        public int gameId { get; set; }

        [ForeignKey("team")]
        public int teamId { get; set; }

        public Team team { get; set; }

        public Game game { get; set; }

        public ICollection<GamePlayer> gamePlayers { get; set; }

    }
}