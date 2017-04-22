using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Configuration;
using System.Configuration;

namespace DribblyWebAPI.Controllers
{
    public class SettingsController : ApiController
    {
        public IHttpActionResult GET()
        {
            try
            {
                return Ok(WebConfigurationManager.AppSettings);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
