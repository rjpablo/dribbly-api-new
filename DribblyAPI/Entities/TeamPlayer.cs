using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class TeamPlayer : BaseEntity
    {
        [Key, Column(Order = 0)]
        [ForeignKey("player")]
        public string playerId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("team")]
        public int teamId { get; set; }
        
        public bool hasLeft { get; set; }

        public DateTime? dateJoined { get; set; }

        public DateTime? dateLeft { get; set; }

        public virtual Team team { get; set; }

        public virtual PlayerProfile player { get; set; }

    }
}