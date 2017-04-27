using SitemapAnalyser.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitemapAnalyser.Controllers
{
    public class HomeController : Controller
    {
        public List<string> TheList = new List<string>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = "Sitemap.xml";
                    var path = Path.Combine(Server.MapPath("~/App_Data/SiteMapFiles"), fileName);
                    file.SaveAs(path);
                }
                ViewBag.Message = "Upladdningen lyckades";
                return View("index", ViewBag.Massage);
            }
            catch
            {
                ViewBag.Message = "Upladdningen misslyckades";
                return View("index", ViewBag.Massage);
            }
        }

        [HttpPost]
        public ActionResult PrintFile(string svgString)
        {
            using (System.IO.StreamWriter file =
             new System.IO.StreamWriter(@"C:\SiteMapSave\NoamRiterSVG.svg"))
            {
                file.WriteLine(svgString);
            }
            return RedirectToAction("SiteMapping");
        }

        public ActionResult SiteMapping(string path, string colorText, string colorLines, string colorRectWithText, string colorRectWithoutText, string linesConnection)
        {
            SiteMappingViewModel model = new SiteMappingViewModel(path, colorText, colorLines, colorRectWithText, colorRectWithoutText, linesConnection);
            if (colorText != null)
            {
                model.ColorText = colorText;
            }
            if (colorLines != null)
            {
                model.ColorLines = colorLines;
            }
            if (colorRectWithText != null)
            {
                model.ColorRectWithText = colorRectWithText;
            }
            if (colorRectWithoutText != null)
            {
                model.ColorRectWithoutText = colorRectWithoutText;
            }
            return View(model);
        }

        public void splitstring(string toSplit)
        {
            toSplit = toSplit.Replace("http://", "");
        }
    }
}