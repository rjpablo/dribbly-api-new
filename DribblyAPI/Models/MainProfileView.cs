using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DribblyAPI.Entities;

namespace DribblyAPI.Models
{
    public class MainProfileView
    {
        public string userId { get; set; }

        public IEnumerable<UserPhoto> photos { get; set; }

    }
}