using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class DribblyException: Exception
    {
        public DribblyException(string UserMessage) : base()
        {
            this.UserMessage = UserMessage;
        }

        public string UserMessage { get; set; }

        public string ErrorTitle { get; set; }
    }
}