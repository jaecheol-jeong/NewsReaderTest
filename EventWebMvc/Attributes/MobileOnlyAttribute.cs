using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventWebMvc.Models;

namespace EventWebMvc
{
	public class MobileOnlyAttribute : ActionFilterAttribute
	{
		/// <summary> 모바일기기가 아닌것에서 접근시 이동할 URL </summary>
		public string RedirectUrl { get; set; }

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);

			string _isMobileCheckParams = filterContext.HttpContext.Request.Params["isMobile"];
			string _userAgent = filterContext.HttpContext.Request.UserAgent;

			if (string.IsNullOrWhiteSpace(_isMobileCheckParams) || !_isMobileCheckParams.ToUpper().Equals("Y"))
			{
				if (!(IsAppleDevice(_userAgent) || IsAndroidDevice(_userAgent)))
				{
					if (string.IsNullOrEmpty(RedirectUrl))
					{
						//throw new Exception("Mobile only!!");
						var _errorMessage = new string[]
					{
						"mobile only",
						filterContext.HttpContext.Request.Url.AbsoluteUri
					};

						string _model = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(_errorMessage);
						filterContext.Result = new RedirectToRouteResult(
							new System.Web.Routing.RouteValueDictionary { { "controller", "Error" }, { "model", _model } }
						);
					}
					else
					{
						filterContext.HttpContext.Response.Redirect(RedirectUrl);
					}
					
				}
			}
		}

		/// <summay>check apple device</summary>
		protected bool IsAppleDevice(string userAgent)
		{
			if (userAgent.StartsWith("Mozilla/") &&
					 (userAgent.Contains("iPhone;") || userAgent.Contains("iPod;") || userAgent.Contains("iPad;")))
			{
				return true;
			}
			return false;
		}

		/// <summary>check android device</summary>
		protected bool IsAndroidDevice(string userAgent)
		{
			if (userAgent.StartsWith("Mozilla/") && userAgent.Contains("Android"))
				return true;

			return false;
		}
	}
}