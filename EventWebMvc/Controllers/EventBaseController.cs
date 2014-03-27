/*
 * 월별 진행하는 Event MVC plaform 구성
 * jeacheol jeong
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace EventWebMvc.Controllers
{
		
    public class EventBaseController : Controller
    {
        /// <summary>모바일기기의 요청여부</summary>
        public bool IsMobile { get; set; }
        /// <summary>Android 모바일기기의 요청여부</summary>
        public bool IsAndroid { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
											
            var _controllerName = filterContext.RouteData.Values["controller"];
            var _eventMonth = filterContext.RouteData.Values["eventMonth"];
            var _eventYear = filterContext.RouteData.Values["eventYear"];
            var _actionName = filterContext.RouteData.Values["action"];
						
					
			if (_eventYear == null || _eventMonth == null || _actionName == null)
            {
                InvalidPath();
				return;
			}

			#region set mobile check value
			string _isMobileCheckParams = filterContext.HttpContext.Request.Params["isMobile"];
            string _userAgent = filterContext.HttpContext.Request.UserAgent;

            if (string.IsNullOrWhiteSpace(_isMobileCheckParams) || !_isMobileCheckParams.ToUpper().Equals("Y"))
            {
                IsMobile = IsAppleDevice(_userAgent) || IsAndroidDevice(_userAgent);
                IsAndroid = IsAndroidDevice(_userAgent);
            }
            else
            {
                IsMobile = true;
            }
			#endregion


			Regex reNum = new Regex(@"^\d+$");
            bool isNumeric = reNum.Match(_eventYear.ToString()).Success;

            if (!(isNumeric && reNum.Match(_eventMonth.ToString()).Success))
            {
                InvalidPath();
            }

            /*
						string path = HttpContext.Server.MapPath(string.Format("/Controllers/{0}/{1}/{2}Controller", _eventYear, _eventMonth, _controllerName));
					  FileInfo fi = new FileInfo(path+".cs");
            if (!fi.Exists)
            {
                InvalidPath();
            }
						*/
        }
				
				/// <summary>잘못된 접근</summary>
        private void InvalidPath()
        {
            if (!IsMobile)
            {
                Response.Write("<script>alert('잘못된 접근입니다.');</script>");
            }
            else
            {
                Response.Write("<script>alert('잘못된 접근입니다.'); history.back(-1);</script>");
            }
            Response.End();
        }

        /// <summay>check apple device</summary>
        protected virtual bool IsAppleDevice(string userAgent)
        {
					if (!string.IsNullOrEmpty(userAgent))
					{
						if (userAgent.StartsWith("Mozilla/") &&
								 (userAgent.Contains("iPhone;") || userAgent.Contains("iPod;") || userAgent.Contains("iPad;")))
						{
							return true;
						}
					}
          
					return false;
        }

        /// <summary>check android device</summary>
        protected virtual bool IsAndroidDevice(string userAgent)
        {
					if (!string.IsNullOrEmpty(userAgent))
					{
						if (userAgent.StartsWith("Mozilla/") && userAgent.Contains("Android"))
							return true;
					}

          return false;
        }
				
				/// <summary>Log Write</summary>
				protected virtual void PrintLog(HttpContextBase hContext)
				{
					System.Diagnostics.Debug.WriteLine(hContext.Request.RawUrl);
				}
    }
}
