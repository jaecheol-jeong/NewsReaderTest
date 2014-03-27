using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventWebMvc.Models;
using System.Data.Common;
using System.Diagnostics;

namespace EventWebMvc.Controllers._2013._10
{
	[DebuggerDisplay("isMobile={IsMobile}, isAndroid={IsAndroid}")]
	public class EventWebController : EventBaseController
	{
		//
		// GET: /EventWeb/
		[OutputCache(Duration = 10)]
		public ActionResult Index()
		{

			return View();
		}

		[JSON]
		public ActionResult Callback(string messagecode, string winNo, string custNo)
		{
			var result = new { MessageCode = messagecode, WinNo = winNo, CustNo = custNo };
			System.Diagnostics.Debug.WriteLine(messagecode + "," + winNo + "," + custNo);
			
			return View(result);
		}

		public ActionResult Dir()
		{
			string _path = Server.MapPath("/");

			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(_path);

			List<string> _list = strDirsName(di.FullName);
			
			return View(_list);
		}


		private List<string> strDirsName(string path)
		{
			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);

			List<string> list = new List<string>();
			var _dirs = di.GetDirectories();
			foreach (System.IO.DirectoryInfo _di in _dirs)
			{
				foreach (string s in getDir(_di.FullName))
				{
					list.Add(s);
				}
			}

			return list;
		}

		private List<string> getDir(string path)
		{
			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
			List<string> _list = new List<string>();

			_list.Add(di.Name);
			System.IO.DirectoryInfo[] _dis = di.GetDirectories();
			for (int i = 0; i < _dis.Length; i++ )
			{
				foreach (string _s in getDir(_dis[i].FullName))
				{
					_list.Add("\\--\\--" + _s);
				}
				
			}

			return _list;

		}


		[MobileOnly]
		public ActionResult MobileIndex()
		{
			return View();
		}

		[JSON]
		public ActionResult ShowJson()
		{
			var data = new
			{
				name = "홍길동",
				no = 10,
				age = 20,
				sex = "M",
				data = new { y = 100, u = 10 },
				path = HttpContext.Server.MapPath("/Controller/2013/10/EventWeb/")
			};

			var routeData = Request.RequestContext.RouteData;

			return View(data);
		}

		
		public ActionResult EncodingTest()
		{			
			//var encoding = HttpContext.Request.ContentEncoding;
			var encoding = HttpContext.Request.ServerVariables; // HttpContext.Request.Headers;
			
			return View(encoding);
		}


		public ActionResult DbModelTest()
		{
			
			return View();
		}
		
		private static IEnumerable<object[]> Read(DbDataReader reader)
		{
			while (reader.Read())
			{
				var values = new List<object>();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					values.Add(reader.GetValue(i));
				}
				yield return values.ToArray();
			}
		}
	}
}
