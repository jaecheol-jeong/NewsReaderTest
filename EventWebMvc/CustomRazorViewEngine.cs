using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventWebMvc
{
    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine()
            : base()
        {
            ViewLocationFormats = new string[] {                          
                         "~/Areas/Admin/Views/{0}/{1}.cshtml"
            };
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return base.CreateView(controllerContext, viewPath, masterPath);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return base.CreatePartialView(controllerContext, partialPath);
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return base.FileExists(controllerContext, virtualPath);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var _controllerName = controllerContext.RouteData.Values["controller"];
            var _eventMonth = controllerContext.RouteData.Values["eventMonth"];
            var _eventYear = controllerContext.RouteData.Values["eventYear"];
            var _actionName = controllerContext.RouteData.Values["action"];

            var uri = string.Format("~/Views/{0}/{1}/{2}/{3}.cshtml", _eventYear, _eventMonth, _controllerName, viewName);
            if (VirtualPathProvider.FileExists(uri))
            {
                return new ViewEngineResult(CreateView(controllerContext, uri, masterName), this);
            }
            else
            {
                foreach (string _uri in ViewLocationFormats)
                {
                    uri = string.Format(_uri, _controllerName, viewName);
                    if (VirtualPathProvider.FileExists(uri))
                        return new ViewEngineResult(CreateView(controllerContext, uri, masterName), this);
                }
                
                return new ViewEngineResult(CreateView(controllerContext, "~/Views/Shared/404Error.cshtml", masterName), this);
                //return base.FindView(controllerContext, viewName, masterName, useCache);
            }
        }

    }
}