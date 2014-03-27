using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace WebBizLibrary
{
    public sealed class NewsSinglon
    {
        private static NewsSinglon _instance;

        private NewsSinglon() { }

        static object lockObj = new object();

        public static NewsSinglon Intance
        {
            get
            {
                lock (lockObj)
                {
                    if (_instance == null)
                        _instance = new NewsSinglon();
                }

                return _instance;
            }
        }

        public string getUrl(int n)
        {
            string[] urls = { "https://news.google.co.kr/news/feeds?pz=1&cf=all&ned=kr&hl=ko&output=rss",
                              "https://news.google.co.kr/news?pz=1&cf=all&ned=kr&hl=ko&topic=p&output=rss",
                              "https://news.google.co.kr/news?pz=1&cf=all&ned=kr&hl=ko&topic=b&output=rss",
                              "https://news.google.co.kr/news?pz=1&cf=all&ned=kr&hl=ko&topic=y&output=rss",
                              "https://news.google.co.kr/news?pz=1&cf=all&ned=kr&hl=ko&topic=l&output=rss",
                              "https://news.google.co.kr/news?pz=1&cf=all&ned=kr&hl=ko&topic=w&output=rss",
                              "https://news.google.co.kr/news?pz=1&cf=all&ned=kr&hl=ko&topic=t&output=rss",
                              "https://news.google.co.kr/news?pz=1&cf=all&ned=kr&hl=ko&topic=e&output=rss",
                              "https://news.google.co.kr/news?pz=1&cf=all&ned=kr&hl=ko&topic=s&output=rss"
                            };
            return urls[n];
        }

        public NewsEntity GetNews(string url)
        {
            string szXml = "";
            NewsEntity entities = new NewsEntity();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ReadWriteTimeout = 2000;
                        
            using(HttpWebResponse res = (HttpWebResponse)request.GetResponse())
            {
                Stream responseStream = res.GetResponseStream();
                StreamReader srReader = new StreamReader(responseStream, Encoding.UTF8);

                szXml = srReader.ReadToEnd();
            }
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(szXml);

            var json = JsonConvert.SerializeXmlNode(doc);
            XmlNode channel = doc.GetElementsByTagName("channel")[0];

            entities = new NewsEntity()
            {
                Title = channel["title"].InnerText,
                Language = channel["language"].InnerText,
                Link = channel["link"].InnerText,
                CopyRight = channel["copyright"].InnerText
            };
                           
            var dataList = doc.GetElementsByTagName("item");
            List<NewsItem> rows = new List<NewsItem>();

            foreach (var node in dataList)
            {
                XmlNode n = (XmlNode)node;
                var item = new NewsItem()
                {
                    Title = n["title"].InnerText,
                    PubDate = n["pubDate"].InnerText,
                    Description = n["description"].InnerText,
                    Link = n["link"].InnerText
                };

                rows.Add(item);                
            }

            entities.Items = rows.AsEnumerable();

            return entities;
        }
    }
}
