using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Enums
{
    public enum TeamActions
    {
        join = 0,
        invite = 1,
        respondToRequest = 2,
        respondToInvitation = 3,
        leave = 4,
        block = 5
    }
}