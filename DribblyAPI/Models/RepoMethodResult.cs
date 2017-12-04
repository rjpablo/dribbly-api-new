using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class RepoMethodResult
    {
        private RepoMethodResultType _resultType = RepoMethodResultType.Success;

        public RepoMethodResultType ResultType {
            get { return _resultType; }
            set { _resultType = value; }
        }

        /// <summary>
        /// The technical error message for logging.
        /// </summary>
        public string ErroMessage { get; set; }

        /// <summary>
        /// The error message that will be shown to the user.
        /// </summary>
        public string UserMessage { get; set; }

        public dynamic Content { get; set; }

        public void SetResult(RepoMethodResultType ResultType, string UserErrorMessage)
        {
            this.ResultType = ResultType;
            this.UserMessage = UserErrorMessage;
        }

    }

    public enum RepoMethodResultType
    {
        Success,
        Failed
    }
}