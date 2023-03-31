using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class News
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }        
        public string image_url { get; set; }
        public string source { get; set; }

        public News(string title, string description, string url, string image_url, string source)
        {
            this.title = title;
            this.description = description;
            this.url = url;
            this.image_url = image_url;
            this.source = source;
        }
    }
}
