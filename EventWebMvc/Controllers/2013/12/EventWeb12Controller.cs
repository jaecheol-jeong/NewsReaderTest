using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Runtime.CompilerServices;
//using WebBizLibrary;

namespace EventWebMvc.Controllers._2013._12
{
    public class EventWeb12Controller : EventBaseController
    {
        static int stcInt = 0;

        enum em
        {
            a = 1,
            b = 3,
            c = 5,
            d = 7
        };

        protected override void PrintLog(HttpContextBase hContext)
        {
            base.PrintLog(hContext);

            System.Diagnostics.Debug.WriteLine("-----");
        }

        //
        // GET: /EventWeb/
        public ActionResult Index()
        {
            this.PrintLog(this.ControllerContext.HttpContext);

            System.Diagnostics.Debug.WriteLine("==" + (em.a & em.c & em.d));
            System.Diagnostics.Debug.WriteLine("hash : " + StaticUtils.GetHash("대한민국"));

            Singleton ins1 = Singleton.Instance;
            System.Diagnostics.Debug.WriteLine("==" + ins1.GetHashMessage("singleton test1"));

            Singleton ins2 = Singleton.Instance;
            System.Diagnostics.Debug.WriteLine("==" + ins2.GetHashMessage("singleton test2"));

            stcInt++;
            System.Diagnostics.Debug.WriteLine("stcInt = " + stcInt.ToString());

			
            //UserProfile user = new WebBizLibrary.Classes1Repo().GetData(1);
            //System.Diagnostics.Debug.WriteLine("name = " + user.UserName);

            return View();
        }

        public ActionResult DoAuth()
        {
            return View();
        }

        
    }
}
