using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class UserProfile : BaseEntity
    {
        [Key]
        public string   userId { get; set; }

        public string sex { get; set; }

        public string profilePic { get; set; }

        public string address { get; set; }

        /// <summary>
        /// Address latitude
        /// </summary>
        public double addressLat { get; set; }

        /// <summary>
        /// Address longitude
        /// </summary>
        public double addressLng { get; set; }

        /// <summary>
        /// Feet portion of height
        /// </summary>
        public int heightFt { get; set; }

        /// <summary>
        /// Inches portion of height
        /// </summary>
        public int heightIn { get; set; }

        public PlayerProfile playerProfile { get; set; }

        public DateTime dateJoined { get; set; }

    }
}