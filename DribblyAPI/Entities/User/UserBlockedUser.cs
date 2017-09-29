using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class UserBlockedUser
    {
        [Column(Order = 0), Key]
        public string userId { get; set; }

        [Column(Order = 1), Key]
        public string bannedUserId { get; set; }

    }
}