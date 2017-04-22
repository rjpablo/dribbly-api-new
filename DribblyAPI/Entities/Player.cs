using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Player
    {
        public int Id { get; set; }

        public List<Game> Games { get; set; }

    }
}