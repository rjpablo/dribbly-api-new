using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Game : BaseEntity
    {
        public int id { get; set; }

        public string title { get; set; }
    }
}