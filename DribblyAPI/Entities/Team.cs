using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Team : BaseEntity
    {
        [Key]
        public int teamId { get; set; }

        public string teamName { get; set; }

        public bool isTemporary { get; set; }

    }
}