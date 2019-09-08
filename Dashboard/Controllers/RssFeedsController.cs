using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RssFeedsController : ControllerBase
    {
        readonly CultureInfo culture = new CultureInfo("en-US");

        [HttpGet("[action]")]
        public IEnumerable<Feed> GetAll()
        {
            try
            {
                XDocument doc = XDocument.Load("https://www.c-sharpcorner.com/rss/latestcontentall.aspx");
                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                select new Feed
                {
                    Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                    Link = (item.Elements().First(i => i.Name.LocalName == "link").Value).StartsWith("/") ? "https://www.c-sharpcorner.com" + item.Elements().First(i => i.Name.LocalName == "link").Value : item.Elements().First(i => i.Name.LocalName == "link").Value,
                    PubDate = Convert.ToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value, culture),
                    PublishDate = Convert.ToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value, culture).ToString("dd-MMM-yyyy"),
                    Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                    FeedType = (item.Elements().First(i => i.Name.LocalName == "link").Value).ToLowerInvariant().Contains("blog") ? "Blog" : (item.Elements().First(i => i.Name.LocalName == "link").Value).ToLowerInvariant().Contains("news") ? "News" : "Article",
                    Author = item.Elements().First(i => i.Name.LocalName == "author").Value
                };

                return entries.OrderByDescending(o => o.PubDate);
            }
            catch
            {
                List<Feed> feeds = new List<Feed>();
                Feed feed = new Feed();
                feeds.Add(feed);
                return feeds;
            }
        }

        public class Feed
        {
            public string Link { get; set; }
            public string Title { get; set; }
            public string FeedType { get; set; }
            public string Author { get; set; }
            public string Content { get; set; }
            public DateTime PubDate { get; set; }
            public string PublishDate { get; set; }

            public Feed()
            {
                Link = "";
                Title = "";
                FeedType = "";
                Author = "";
                Content = "";
                PubDate = DateTime.Today;
                PublishDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }
    }
}
