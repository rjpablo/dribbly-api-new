using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    /// <summary>
    /// Class that represents the relation between a user and a team
    /// </summary>
    public class UserToTeamRelation
    {
        public string userId { get; set; }

        public int teamId { get; set; }

        public bool? isOwner { get; set; }

        public bool? isMember { get; set; }

        public bool? isCurrentMember { get; set; }

        public bool? isInvited { get; set; }

        public bool? hasRequested { get; set; }
    }
}