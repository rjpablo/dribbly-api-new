using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class PlayerProfile : BaseEntity
    {
        [Key]
        public string   userId { get; set; }

        public int mvps { get; set; }

        public double winRate { get; set; }

        public double rating { get; set; }

        public double dribblingSkill { get; set; }

        public double passingSkill { get; set; }

        public double threePointSkill { get; set; }

        public double blockingSkill { get; set; }

        public double defensiveSkill { get; set; }

        public double shootingSkill { get; set; }

        public DateTime dateCreated { get; set; }

        public bool isActive { get; set; }

        [NotMapped]
        public IEnumerable<Game> games { get; set; }

    }
}