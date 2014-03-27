using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebBizLibrary
{
    public class NewsEntity
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Language { get; set; }

        public string CopyRight { get; set; }

        public IEnumerable<NewsItem> Items { get; set; } 
    }

    public class NewsItem
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string PubDate { get; set; }

        public string Description { get; set; }
    }
}
