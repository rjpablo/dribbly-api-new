using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    [Table("PlayerListItem")]
    public class PlayerListItem
    {
        [Key]
        public string userId { get; set; }

        public string userName { get; set; }

        [ForeignKey("profilePic")]
        public int? profilePicId { get; set; }

        public DateTime dateJoined { get; set; }

        public string sex { get; set; }

        public int heightFt { get; set; }

        public int heightIn { get; set; }

        public bool isActive { get; set; }

        public double winRate { get; set; }

        public double rating { get; set; }

        public int mvps { get; set; }

        public double dribblingSkill { get; set; }

        public double passingSkill { get; set; }

        public double threePointSkill { get; set; }

        public double blockingSkill { get; set; }

        public double defensiveSkill { get; set; }

        public double shootingSkill { get; set; }

        public UserPhoto profilePic { get; set; }


    }
}