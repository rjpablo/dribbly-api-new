using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class TeamFollower
    {
        [Column(Order = 0), Key]
        public int teamId { get; set; }

        [Column(Order = 1), Key]
        public string userId { get; set; }
    }
}