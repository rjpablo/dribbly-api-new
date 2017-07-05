using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class MemberRequest:BaseEntity
    {
        [Key, Column(Order =0)]
        public string playerId { get; set; }

        [Key, Column(Order = 1)]
        public int teamId { get; set; }

        public int? position { get; set; }

        public DateTime dateRequested { get; set; }

    }
}