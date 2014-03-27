using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebBizLibrary
{
	public class Classes1Repo
	{
		public string GetName(int id)
		{
			DataClasses1DataContext context = new DataClasses1DataContext();
			var data = context.UserProfile.Where(a => a.UserId == id).FirstOrDefault();

			return data.UserName;
		}

        public UserProfile GetData(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            var data = context.webpages_UsersInRoles.Where(a => a.UserId == id).FirstOrDefault()
                .UserProfile;


            return data;
        }
	}
}
