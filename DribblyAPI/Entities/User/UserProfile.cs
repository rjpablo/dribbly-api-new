﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class UserProfile : BaseEntity
    {
        [Key]
        public string userId { get; set; }

        public string sex { get; set; }

        [ForeignKey("profilePic")]
        public int? profilePicId { get; set; }

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

        public DateTime dateJoined { get; set; }

        [ForeignKey("city")]
        public int? cityId { get; set; }

        public virtual City city { get; set; }

        public virtual UserPhoto profilePic { get; set; }

    }
}