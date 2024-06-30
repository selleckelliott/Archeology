using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archeology.Tags
{
    public class Channel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string BuildDate { get; set; }
        public Channel(string title, string link, string description, string buildDate) 
        {
            this.Title = title;
            this.Link = link;
            this.Description = description;
            this.BuildDate = buildDate;
        }
        public override string ToString()
        {
            return "\nTitle: " + this.Title +
                    "\nLink:" + this.Link +
                    "\nDescription" + this.Description +
                    "\nBuild Date:" + this.BuildDate;
        }
    }
}
