using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakPisir.CORE.Result
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        bool IsFailure { get; }
        bool IsWarning { get; set; }
        bool IsInfo { get; set; }
        string Message { get; set; }
        Exception Exception { get; set; }
        string Class { get; set; }
        int ReturnId { get; set; }
        object Ids { get; set; }
        string RedirectUrl { get; set; }
        string Condition { get; set; }
        string PageName { get; set; }


        Result Success();
        Result Success(string message);
        Result Success(string message, string redirectUrl);
        Result Success(string message, int returnId);
        Result Success(string message, object returnIdCollection);
        Result Info(string message);
        Result Warning(string message);
        Result Warning(string message, string condition);
        Result Warning(string message, string condition, object returnIdCollection);
        Result Fail(string message = "", string redirectUrl = "");
        Result Fail(string message, Exception exception);
    }
}
