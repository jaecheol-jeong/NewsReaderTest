using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.Linq.Expressions;


namespace EventWebMvc
{
	public class EnumHelper
	{
		public static IEnumerable<SelectListItem> ToSelectList(Enum enumValue)
		{
			List<SelectListItem> items = new List<SelectListItem>();
			items.Add(new SelectListItem() { Selected = true, Text = "--전체--", Value = "" });
			var slt = (from Enum e in Enum.GetValues(enumValue.GetType())
								 select new SelectListItem
								{
									Selected = false,
									Text = e.ToString(),
									Value = e.ToString()
								}).ToList();

			items.AddRange(slt);
			
			return items.AsEnumerable();
		}
	}
}