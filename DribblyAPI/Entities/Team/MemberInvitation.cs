using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DribblyAPI.Entities
{
    public class MemberInvitation:BaseEntity
    {
        [Key]
        public int id { get; set; }

        public int teamId { get; set; }

        public string playerId { get; set; }

        public int? position { get; set; }

        public DateTime dateInvited { get; set; }

        public int? response { get; set; }

    }
}