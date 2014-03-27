using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace EventWebMvc
{
	public static class StaticUtils
	{
		public static int ToInt(this DateTime dateTime, string format)
		{
			string strInt = dateTime.ToString(format);

			return Convert.ToInt16(strInt);
		}

        public static string GetHash(string msg)
        {
            string hash = "";
            string key = "MICKEY";
            byte[] byteMsg = System.Text.Encoding.Default.GetBytes(msg + key);
            HMACSHA256 hmacHash = new HMACSHA256();
            var computeMsg = hmacHash.ComputeHash(byteMsg);
            hash = Convert.ToBase64String(computeMsg);
            return hash;
        }
	}
}