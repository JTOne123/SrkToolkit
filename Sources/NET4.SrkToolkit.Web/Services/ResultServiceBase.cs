﻿
namespace SrkToolkit.Web.Services
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using SrkToolkit.Web.HttpErrors;

    /// <summary>
    /// Helps return generic HTTP responses.
    /// </summary>
    public class ResultServiceBase
    {
        private readonly HttpContextBase httpContext;

        public const string RouteDataExceptionKey = "error";
        public const string RouteDataMessageKey = "message";
        public const string RouteDataHttpCodeKey = "http code";

        public ResultServiceBase(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        /// <summary>
        /// Returns a standard JSON result for a successful operation.
        /// </summary>
        /// <returns></returns>
        public ActionResult JsonSuccess()
        {
            return new JsonNetResult
            {
                Data = new
                {
                    Success = true,
                    ErrorCode = default(string),
                    ErrorMessage = default(string),
                    Data = default(string),
                },
            };
        }

        /// <summary>
        /// Returns a standard JSON result containing data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult JsonSuccess(object data)
        {
            return new JsonNetResult
            {
                Data = new
                {
                    Success = true,
                    ErrorCode = default(string),
                    ErrorMessage = default(string),
                    Data = data,
                },
            };
        }

        /// <summary>
        /// Returns a standard JSON result containing an error.
        /// </summary>
        /// <returns></returns>
        public ActionResult JsonError()
        {
            return new JsonNetResult
            {
                Data = new
                {
                    Success = false,
                    ErrorCode = default(string),
                    ErrorMessage = default(string),
                    Data = default(string),
                },
            };
        }

        /// <summary>
        /// Returns a standard JSON result containing an error.
        /// </summary>
        /// <param name="errorCode">helps identify the the error</param>
        /// <returns></returns>
        public ActionResult JsonError(string errorCode)
        {
            return new JsonNetResult
            {
                Data = new
                {
                    Success = false,
                    ErrorCode = errorCode,
                    ErrorMessage = default(string),
                    Data = default(string),
                },
            };
        }

        /// <summary>
        /// Returns a standard JSON result containing an error.
        /// </summary>
        /// <param name="errorCode">helps identify the the error</param>
        /// <param name="errorMessage">the translated error message to display</param>
        /// <returns></returns>
        public ActionResult JsonError(string errorCode, string errorMessage)
        {
            return new JsonNetResult
            {
                Data = new
                {
                    Success = false,
                    ErrorCode = errorCode,
                    ErrorMessage = errorMessage,
                    Data = default(string),
                },
            };
        }

        /// <summary>
        /// Returns a standard JSON result containing an error.
        /// </summary>
        /// <param name="errorCode">helps identify the the error</param>
        /// <param name="errorMessage">the translated error message to display</param>
        /// <returns></returns>
        public ActionResult JsonError(string errorCode, string errorMessage, object data)
        {
            return new JsonNetResult
            {
                Data = new
                {
                    Success = false,
                    ErrorCode = errorCode,
                    ErrorMessage = errorMessage,
                    Data = data,
                },
            };
        }

        protected HttpContextBase HttpContext
        {
            get { return this.httpContext; }
        }
    }
}
