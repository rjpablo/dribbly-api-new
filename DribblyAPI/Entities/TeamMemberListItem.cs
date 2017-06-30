using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class TeamMemberListItem:BaseEntity
    {
        [Key, Column(Order = 0)]
        public string playerId { get; set; }

        [Key, Column(Order = 1)]
        public int teamId { get; set; }

        public string profilePic { get; set; }

        public string userName { get; set; }

        public bool hasLeft { get; set; }

        public DateTime? dateJoined { get; set; }

        public DateTime? dateLeft { get; set; }
    }
}