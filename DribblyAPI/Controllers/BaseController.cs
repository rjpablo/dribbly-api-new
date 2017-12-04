using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DribblyAPI;
using DribblyAPI.Models;
using DribblyAPI.Interfaces;

namespace DribblyAPI.Controllers
{
    public class BaseController : ApiController
    {
        public IHttpActionResult handleRepoMethodResult(RepoMethodResult result)
        {

            if(result.ResultType == RepoMethodResultType.Success)
            {
                return Ok(result.Content);
            }
            else
            {
                return BadRequest(result.UserMessage);
            }
        }
        
        /// <summary>
        /// Takes an exception and returns a new exception with a more user-friendly error message with the original exception as its inner exception.
        /// </summary>
        /// <param name="ex">The original exception</param>
        /// <param name="UserErrorMessage">A user-friendly message that will be shown to the user.</param>
        /// <returns></returns>
        public IHttpActionResult handleRepoMethodException(Exception ex, String UserErrorMessage)
        {
            Exception e = new Exception(UserErrorMessage, ex);
            return InternalServerError(e);
        }
    }
}