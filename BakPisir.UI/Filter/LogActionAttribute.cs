using BakPisir.CORE.Helper;
using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using BakPisir.UI.Services;

namespace BakPisir.CORE.Filter
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Items.Contains("LogExecuted"))
            {
                LogDto newLog = new LogDto();
                newLog.ipAddress = HttpContext.Current.Request.UserHostAddress;
                newLog.logActivity = filterContext.HttpContext.Request.RawUrl;
                newLog.logDate = DateTime.Now;

                if (SessionHelper.LoggedUserInfo != null)
                {
                    newLog.logUsername = SessionHelper.LoggedUserInfo.username;
                    newLog.userId = SessionHelper.LoggedUserInfo.userId;
                }
                else
                {
                    newLog.logUsername = "Anonymous";
                }

                LogService logService = new LogService();
                logService.AddLog(newLog);

                filterContext.HttpContext.Items.Add("LogExecuted", true);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
