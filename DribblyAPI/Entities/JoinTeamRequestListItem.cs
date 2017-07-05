using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DribblyAPI.Entities
{
    public class JoinTeamRequestListItem:BaseEntity
    {
        [Key, Column(Order = 0)]
        public string playerId { get; set; }

        [Key, Column(Order = 1)]
        public int teamId { get; set; }

        public int? position { get; set; }

        public DateTime dateRequested { get; set; }

        public string profilePic { get; set; }

    }
}