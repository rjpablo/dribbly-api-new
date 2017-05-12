using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Player : BaseEntity
    {
        [Key, ForeignKey("userProfile")]
        public string   userId { get; set; }

        public int mpvs { get; set; }

        public double winRate { get; set; }

        public double rating { get; set; }

        public List<Game> Games { get; set; }

        public virtual UserProfile userProfile { get; set; }

    }
}