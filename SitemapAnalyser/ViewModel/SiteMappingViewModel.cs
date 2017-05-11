using SitemapAnalyser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapAnalyser.ViewModel
{
    public class SiteMappingViewModel
    {
        public SiteMappingViewModel(string path, string colorText, string colorLines, string colorRectWithText, string colorRectWithoutText, string linesConnection)
        {
            SiteMapGenerator smg = new SiteMapGenerator();
            List<SiteMapObject> allObj = smg.GetAllObjects(path);
            int sizeAndLevel = 1;
            if (path != null)
            {
                sizeAndLevel = path.Split('/').Count();
            }

            if (linesConnection == null)
            {
                LinesConnection = "1";
            }
            else{
                LinesConnection = linesConnection;
            }


            PositionBuilder cm = new PositionBuilder(allObj, sizeAndLevel, LinesConnection);
            AllObjects = cm.Build();

            HeightSize = cm.HeightSize;
            WidthSize = cm.WidthSize;

            RectWidth = cm.recWidht;
            RectHight = cm.recHight;

            RectWidthText = cm.recWidhtText;
            RectHightText = cm.recHightText;
            ColumnsDiff = cm.columnsDiff;
            RowDiff = cm.rowDiff;

            if (colorText == null)
            {
                ColorText = "#1a1aff";
            }
            else
            {
                ColorText = colorText;

            }
            if (colorLines == null)
            {
                ColorLines = "#ffff00";

            }
            else
            {
                ColorLines = colorLines;

            }

            if (colorRectWithText == null)
            {
                ColorRectWithText = "#ff0000";
            }
            else
            {
                ColorRectWithText = colorRectWithText;
            }

            if (colorRectWithoutText == null)
            {
                ColorRectWithoutText = "#ff00ff";

            }
            else
            {
                ColorRectWithoutText = colorRectWithoutText;
            }
           

            AllLineObjects = cm.SiteMapLineObjects();
        }

        public int HeightSize { get; set; }
        public int WidthSize { get; set; }
        public int RectWidth { get; set; }
        public int RectHight { get; set; }

        public int RectWidthText { get; set; }
        public int RectHightText { get; set; }
        public int ColumnsDiff { get; set; }
        public int RowDiff { get; set; }

        private string _colorText;

        public string ColorText
        {
            get { return _colorText; }
            set { _colorText = value; }
        }

        private string _colorLines;

        public string ColorLines
        {
            get { return _colorLines; }
            set { _colorLines = value; }
        }

        private string _colorRectWithText;

        public string ColorRectWithText
        {
            get { return _colorRectWithText; }
            set { _colorRectWithText = value; }
        }

        private string _colorRectWithoutText;

        public string ColorRectWithoutText
        {
            get { return _colorRectWithoutText; }
            set { _colorRectWithoutText = value; }
        }

        private string _linesConnection;

        public string LinesConnection
        {
            get { return _linesConnection; }
            set { _linesConnection = value; }
        }

        public IEnumerable<SiteMapObject> AllObjects { get; set; }
        public IEnumerable<SiteMapLineObject> AllLineObjects { get; set; }
    }
}