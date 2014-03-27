using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventWebMvc
{
    public class JSONAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = new JsonResult();
            result.Data = ((ViewResult)filterContext.Result).Model;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            filterContext.Result = result;
        }
    }
}