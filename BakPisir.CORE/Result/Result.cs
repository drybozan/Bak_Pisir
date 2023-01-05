using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakPisir.CORE.Result
{
    public class Result : IResult
    {
        private static Result _instance;
        private static readonly object Padlock = new object();

        public Result()
        {
            IsSuccess = false;
            ReturnId = 0;
            RedirectUrl = "";
        }

        public static Result Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new Result());
                }
            }
        }

        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public bool IsWarning { get; set; }
        public bool IsInfo { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public string Class { get; set; }
        public int ReturnId { get; set; }
        public object Ids { get; set; }
        public string RedirectUrl { get; set; }
        public string Condition { get; set; }
        public string PageName { get; set; }

        /// <summary>
        /// Sets class to successs and IsSuccess to true
        /// </summary>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Success()
        {
            return new Result
            {
                IsSuccess = true,
                Class = "success"
            };
        }

        /// <summary>
        /// Sets class to successs and IsSuccess to true with info message.
        /// </summary>
        /// <param name="message">requires info message</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Success(string message)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                Class = "success"
            };
        }



        /// <summary>
        /// Sets class to successs and IsSuccess to true with info message and redirect url.
        /// </summary>
        /// <param name="message">info message parameter</param>
        /// <param name="redirectUrl">redirect url parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Success(string message, string redirectUrl)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                RedirectUrl = redirectUrl,
                Class = "success"
            };
        }

        /// <summary>
        /// Sets class to successs and IsSuccess to true with info message and redirect url.
        /// </summary>
        /// <param name="message">info message parameter</param>
        /// <param name="returnId">record id patameter</param>
        /// <param name="redirectUrl">redirect url parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Success(string message, int returnId, string redirectUrl)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                ReturnId = returnId,
                RedirectUrl = redirectUrl,
                Class = "success"
            };
        }

        /// <summary>
        /// Sets class to successs and IsSuccess to true with info message and id.
        /// </summary>
        /// <param name="message">info message parameter</param>
        /// <param name="returnId">id parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Success(string message, int returnId)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                ReturnId = returnId,
                Class = "success"
            };
        }

        /// <summary>
        /// Sets class to successs and IsSuccess to true with info message and id.
        /// </summary>
        /// <param name="message">info message parameter</param>
        /// <param name="returnId">id parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public static Result Success2(string message, int returnId)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                ReturnId = returnId,
                Class = "success"
            };
        }

        /// <summary>
        /// Sets class to successs and IsSuccess to true with info message and id.
        /// </summary>
        /// <param name="message">info message parameter</param>
        /// <param name="pageName">page name parameter</param>
        /// <param name="returnId">id parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Success(string message, string pageName, int returnId)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                ReturnId = returnId,
                PageName = pageName,
                Class = "success"
            };
        }

        /// <summary>
        /// Sets class to successs and IsSuccess to true with info message and object collection. 
        /// </summary>
        /// <param name="message">info message parameter</param>
        /// <param name="returnIdCollection">object collection parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Success(string message, object returnIdCollection)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                Ids = returnIdCollection,
                Class = "success"
            };
        }

        /// <summary>
        /// Sets class to successs and IsSuccess to true with info message and object collection. 
        /// </summary>
        /// <param name="message">info message parameter</param>
        /// <param name="pageName">page name parameter</param>
        /// <param name="returnIdCollection">object collection parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Success(string message, string pageName, object returnIdCollection)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                Ids = returnIdCollection,
                PageName = pageName,
                Class = "success"
            };
        }


        /// <summary>
        /// Sets class to info and IsSuccess and IsInfo to true with an info message.
        /// </summary>
        /// <param name="message">info message parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Info(string message)
        {
            return new Result
            {
                IsSuccess = true,
                IsInfo = true,
                Message = message,
                RedirectUrl = "",
                Class = "info"
            };
        }

        /// <summary>
        /// Sets class to warning, IsSuccess to false and IsWarning to true with a warning message.
        /// </summary>
        /// <param name="message">warning message parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Warning(string message)
        {
            return new Result
            {
                IsSuccess = false,
                IsWarning = true,
                Message = message,
                Class = "warning"
            };
        }

        /// <summary>
        /// Sets class to warning, IsSuccess to false and IsWarning to true with a warning message and condition.
        /// </summary>
        /// <param name="message">warning message parameter</param>
        /// <param name="condition">Url or whatsoever as string parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Warning(string message, string condition)
        {
            return new Result
            {
                IsSuccess = false,
                IsWarning = true,
                Message = message,
                Condition = condition,
                Class = "warning"
            };
        }

        /// <summary>
        /// Sets class to warning, IsSuccess to false and IsWarning to true with a warning message, condition and id collection.
        /// </summary>
        /// <param name="message">warning message parameter</param>
        /// <param name="condition">Url or whatsoever as string parameter</param>
        /// <param name="returnIdCollection">object collection parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Warning(string message, string condition, object returnIdCollection)
        {
            return new Result
            {
                IsSuccess = false,
                IsWarning = true,
                Message = message,
                Ids = returnIdCollection,
                Condition = condition,
                Class = "warning"
            };
        }

        /// <summary>
        /// Sets class to error, IsSuccess to false and IsFailure to true with a fail message and failure message.
        /// </summary>
        /// <param name="message">failure message parameter</param>
        /// <param name="redirectUrl">redirect url parameter</param>
        /// <returns></returns>
        public Result Fail(string message = "", string redirectUrl = "")
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                RedirectUrl = redirectUrl,
                Class = "error"
            };
        }

        /// <summary>
        /// Sets class to error, IsSuccess to false and IsFailure to true with a fail message and failure message.
        /// </summary>
        /// <param name="message">failure message parameter</param>
        /// <param name="redirectUrl">redirect url parameter</param>
        /// <returns></returns>
        public static Result Fail2(string message = "", string redirectUrl = "")
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                RedirectUrl = redirectUrl,
                Class = "error"
            };
        }

        /// <summary>
        /// Sets class to error, IsSuccess to false and IsFailure to true with a fail message and exception.
        /// </summary>
        /// <param name="message">failure message parameter</param>
        /// <param name="exception">[Exception] exception message parameter</param>
        /// <returns>It returns fully qualified Result object.</returns>
        public Result Fail(string message, Exception exception)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                Exception = exception,
                Class = "error"
            };
        }
    }
}
