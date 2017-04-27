using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapAnalyser.Models
{
    internal class Level
    {
        public Level()
        {
            XKantStart = 0;
            XKantEnd = 0;
        }

        public int LevelId { get; set; }

        public List<SiteMapObject> LevelObjectList { get; set; }
        public List<SiteMapObject> LevelChildTemp { get; set; }
        public List<SiteMapObject> LevelPerentTemp { get; set; }

        private int _XKantStart;

        public int XKantStart
        {
            get { return _XKantStart; }
            set { _XKantStart = value; }
        }

        private int _XKantEnd;

        public int XKantEnd
        {
            get { return _XKantEnd; }
            set { _XKantEnd = value; }
        }
    }
}