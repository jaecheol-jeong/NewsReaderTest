using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventWebMvc
{
    public sealed class Singleton
    {
        static int index = 0;
        static object newObj = new object();

        private static Singleton _instance;

        public string msg { get; set; }

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                lock (newObj)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                        index++;
                    }
                }

                System.Diagnostics.Debug.WriteLine("instance :" + index.ToString());
                return _instance;
            }
        }

        public string GetHashMessage(string szMessage)
        {
            return StaticUtils.GetHash(szMessage);
        }
    }
}