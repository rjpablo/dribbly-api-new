using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    /// <summary>
    /// A class that contains fields from IdentityUser class without the
    /// sensitive data. This is mapped to a database view
    /// </summary>
    public class UserView : BaseEntity
    {
        [Key]
        public string userId { get; set; }

        public string email { get; set; }

        public string userName { get; set; }

        public bool isActive { get; set; }

    }
}