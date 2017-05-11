using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapAnalyser.Models
{
    public class SiteMapObject
    {
        public SiteMapObject()
        {
            ObjectID = Guid.NewGuid();
            Placed = false;
            Show = false;
        }

        public Guid ObjectID { get; set; }
        public int ObjectLevel { get; set; }
        public string ObjectName { get; set; }
        public string ObjectPath { get; set; }
        public Guid ObjectBeforID { get; set; }
        public int ObjectBeforLevel { get; set; }
        public bool Show { get; set; }
        public bool Placed { get; set; }
        public int XPoint { get; set; }
        public int YPoint { get; set; }
    }
}