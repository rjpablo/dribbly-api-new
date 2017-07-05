using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DribblyAPI.Entities
{
    public class JoinTeamInvitationListItem:BaseEntity
    {
        [Key, Column(Order = 0)]
        public string playerId { get; set; }

        [Key, Column(Order = 1)]
        public int teamId { get; set; }

        public int? position { get; set; }

        public DateTime dateInvited { get; set; }

        public string profilePic { get; set; }
    }
}