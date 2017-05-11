using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapAnalyser.ViewModel
{
    public class SiteMappingOptionsViewModel
    {
        public string Path { get; set; }

        public string WebSiteAdrress { get; set; }
        public string Replacement { get; set; }

        public string Message { get; set; }
    }
}