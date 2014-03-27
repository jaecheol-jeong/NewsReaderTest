using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBizLibrary;

namespace EventWebMvc.Controllers._2014._03
{
    public class EventWeb03Controller : EventBaseController
    {
        //
        // GET: /EventWeb03/

        public ActionResult Index(int id=0)
        {
            NewsSinglon News = NewsSinglon.Intance;

            NewsEntity NewsList = News.GetNews(News.getUrl(id));

            return View(NewsList);
        }

    }
}
