using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapAnalyser.Models
{
    internal class PositionBuilder
    {
        public PositionBuilder(List<SiteMapObject> objectList, int sizeAndLevel, string linesConnection)
        {
            ObjectList = objectList;

            switch (sizeAndLevel)
            {
                case 1:
                    columnsDiff = 15;
                    columnsChildDiff = 3;
                    rowDiff = 90;
                    recHight = 12;
                    recWidht = 12;

                    recHightText = 60;
                    recWidhtText = 120;
                    break;

                case 2:
                    columnsDiff = 60;
                    columnsChildDiff = 4;
                    rowDiff = 90;
                    recHight = 12;
                    recWidht = 12;

                    recHightText = 60;
                    recWidhtText = 120;
                    break;

                default:
                    columnsDiff = 60;
                    columnsChildDiff = 5;
                    rowDiff = 90;
                    recHight = 12;
                    recWidht = 12;

                    recHightText = 60;
                    recWidhtText = 120;
                    break;
            }
            LinesPatern = linesConnection;
            int CountLevels = (ObjectList.Max(r => r.ObjectLevel) + 1);
            HeightSize = (CountLevels * recHight) + ((CountLevels + 1) * rowDiff);
            WidthSize = 0;
        }

        private List<Level> LevelList = new List<Level>();

        private List<SiteMapLineObject> _SiteMapLineObjects = new List<SiteMapLineObject>();

        public List<SiteMapLineObject> SiteMapLineObjects()
        {
            return _SiteMapLineObjects;
        }

        private List<SiteMapObject> _ObjectList;

        public List<SiteMapObject> ObjectList
        {
            get { return _ObjectList; }
            set { _ObjectList = value; }
        }

        private string _linesPatern;

        public string LinesPatern
        {
            get { return _linesPatern; }
            set { _linesPatern = value; }
        }

        private int _columnsDiff;

        public int columnsDiff
        {
            get { return _columnsDiff; }
            set { _columnsDiff = value; }
        }

        private int _columnsChildDiff;

        public int columnsChildDiff
        {
            get { return _columnsChildDiff; }
            set { _columnsChildDiff = value; }
        }

        private int _rowDiff;

        public int rowDiff
        {
            get { return _rowDiff; }
            set { _rowDiff = value; }
        }

        private int _recHight;

        public int recHight
        {
            get { return _recHight; }
            set { _recHight = value; }
        }

        private int _recWidht;

        public int recWidht
        {
            get { return _recWidht; }
            set { _recWidht = value; }
        }

        private int _recHightText;

        public int recHightText
        {
            get { return _recHightText; }
            set { _recHightText = value; }
        }

        private int _recWidhtText;

        public int recWidhtText
        {
            get { return _recWidhtText; }
            set { _recWidhtText = value; }
        }

        private int _heightSize;

        public int HeightSize
        {
            get { return _heightSize; }
            set { _heightSize = value; }
        }

        private int _widthSize;

        public int WidthSize
        {
            get { return _widthSize + columnsDiff; }
            set { _widthSize = value; }
        }

        public void getObjectListToLevels()
        {
            int numberOfLevels = ObjectList.Max(r => r.ObjectLevel) + 1;

            for (int i = 0; i < numberOfLevels; i++)
            {
                Level newLevel = new Level();
                newLevel.LevelId = i;
                newLevel.LevelObjectList = ObjectList.Where(x => x.ObjectLevel == i).ToList();
                newLevel.XKantEnd = columnsDiff;
                newLevel.XKantStart = columnsDiff;
                LevelList.Add(newLevel);
            }
        }

        public void ContactLinesToRect(SiteMapObject pappa)
        {
            List<SiteMapObject> childTempList = ObjectList.Where(x => x.ObjectBeforID == pappa.ObjectID).ToList();
            int y = pappa.YPoint + 20;
            int xStart = childTempList.Min(x => x.XPoint) + (recWidht / 2);
            int xEnd = childTempList.Max(x => x.XPoint) + (recWidht / 2);
            SiteMapLineObject middleObject = new SiteMapLineObject();
            middleObject.FromX = xStart;
            middleObject.FromY = y;
            middleObject.ToX = xEnd;
            middleObject.ToY = y;
            _SiteMapLineObjects.Add(middleObject);

            int pappaX = (pappa.XPoint) + (recWidht / 2);
            int pappaY;
            if (pappa.Show)
            {
                pappaY = (pappa.YPoint);
            }
            else
            {
                pappaY = (pappa.YPoint) + recHight;
            }

            SiteMapLineObject pappaObject = new SiteMapLineObject();
            pappaObject.FromX = pappaX;
            pappaObject.FromY = pappaY;
            pappaObject.ToX = pappaX;
            pappaObject.ToY = y;
            _SiteMapLineObjects.Add(pappaObject);

            foreach (var item in childTempList)
            {
                int childX = (item.XPoint) + (recWidht / 2);
                int childY = item.YPoint;
                if (item.Show)
                {
                    childX = (item.XPoint) + (recWidht / 2);
                    childY = item.YPoint - recHightText;
                }
                SiteMapLineObject newObject = new SiteMapLineObject();
                newObject.FromX = childX;
                newObject.FromY = y;
                newObject.ToX = childX;
                newObject.ToY = childY;
                _SiteMapLineObjects.Add(newObject);
            }
        }

        public void ContactLinesToRectDiagonal(SiteMapObject pappa)
        {
            List<SiteMapObject> childTempList = ObjectList.Where(x => x.ObjectBeforID == pappa.ObjectID).ToList();

            int pappaX = (pappa.XPoint) + (recWidht / 2);
            int pappaY;
            if (pappa.Show)
            {
                pappaY = (pappa.YPoint);
            }
            else
            {
                pappaY = (pappa.YPoint) + recHight;
            }

            foreach (var item in childTempList)
            {
                int childX = (item.XPoint) + (recWidht / 2);
                int childY = item.YPoint;
                if (item.Show)
                {
                    childX = (item.XPoint) + (recWidht / 2);
                    childY = item.YPoint - recHightText;
                }
                SiteMapLineObject newObject = new SiteMapLineObject();
                newObject.FromX = pappaX;
                newObject.FromY = pappaY;
                newObject.ToX = childX;
                newObject.ToY = childY;
                _SiteMapLineObjects.Add(newObject);
            }
        }

        private bool DosTheGroupOfChildrenHaveABrotherPlaced(SiteMapObject child)
        {
            bool result = false;
            List<SiteMapObject> tempChildrenList = GetChildrenByPappaId(child.ObjectBeforID);
            foreach (var item in tempChildrenList)
            {
                if (item.Placed == true)
                    result = true;
            }
            return result;
        }

        private bool DosTheGroupOfChildrenHaveABrotherPlacedAndShowTrue(SiteMapObject child)
        {
            bool result = false;
            List<SiteMapObject> tempChildrenList = GetChildrenByPappaId(child.ObjectBeforID);
            foreach (var item in tempChildrenList)
            {
                if (item.Placed == true && item.Show == true)
                    result = true;
            }
            return result;
        }

        private int XPointWhenGroupOfChildrenHaveABrotherPlaced(SiteMapObject child)
        {
            List<SiteMapObject> tempChildrenList = GetChildrenByPappaId(child.ObjectBeforID);
            if (tempChildrenList[0].Show == true)
            {
                return tempChildrenList.Max(x => x.XPoint) + recWidhtText;
            }
            else
            {
                return tempChildrenList.Max(x => x.XPoint) + recWidht;
            }
        }

        private List<SiteMapObject> GetChildrenByPappaId(Guid id)
        {
            return ObjectList.Where(x => x.ObjectBeforID == id).ToList();
        }

        private Level getLowestLevelWithBigestGroupOfChildrenInTempChild()
        {
            Level TempLevel = LevelList.OrderBy(x => x.LevelId).Last();

            if (TempLevel.LevelObjectList.Count() != 0)
            {
                var x = TempLevel.LevelObjectList.GroupBy(r => r.ObjectBeforID).OrderByDescending(t => t.Count()).SelectMany(s => s).ToList();
                TempLevel.LevelChildTemp = ObjectList.Where(e => e.ObjectBeforID == x[0].ObjectBeforID).ToList();
            }
            return TempLevel;
        }

        private int GetYPoint(int level)
        {
            return (level * recHight) + ((level + 1) * rowDiff);
        }

        private void WidthSizeCount(int newCanvasWidthSize)
        {
            if (newCanvasWidthSize > WidthSize)
            {
                WidthSize = newCanvasWidthSize;
            }
        }

        private void PlaceChildren(Level children)
        {
            if (children.XKantEnd == columnsDiff && WidthSize == 0)
            {
                //do nothing
            }
            else
            {
                if (DosTheGroupOfChildrenHaveABrotherPlaced(children.LevelChildTemp[0]))
                {
                    if (DosTheGroupOfChildrenHaveABrotherPlacedAndShowTrue(children.LevelChildTemp[0]))
                    {
                        children.XKantEnd = XPointWhenGroupOfChildrenHaveABrotherPlaced(children.LevelChildTemp[0]) + recWidhtText;
                    }
                    else
                    {
                        children.XKantEnd = XPointWhenGroupOfChildrenHaveABrotherPlaced(children.LevelChildTemp[0]) + columnsChildDiff;
                    }
                }
                else
                {
                    if (children.XKantEnd >= WidthSize)
                    {
                        children.XKantEnd += columnsDiff;
                    }
                    else
                    {
                        SiteMapObject tPappa = GetPappa(children.LevelChildTemp[0]);
                        if (tPappa.Show == true)
                        {
                            children.XKantEnd = WidthSize + (recWidhtText);
                        }
                        else
                        {
                            children.XKantEnd = WidthSize + columnsDiff;
                        }
                    }
                }
            }

            foreach (var Child in children.LevelChildTemp)
            {
                if (Child.Placed == false)
                {
                    if (Child.Show == true)
                    {
                        Child.XPoint = children.XKantEnd + (recWidhtText / 2);
                        TempChildrenLevel.XKantEnd += columnsChildDiff + recWidhtText;
                    }
                    else
                    {
                        Child.XPoint = children.XKantEnd;
                        TempChildrenLevel.XKantEnd += columnsChildDiff + recWidht;
                    }
                    Child.YPoint = GetYPoint(children.LevelId);
                    Child.Placed = true;
                }
            }
            TempChildrenLevel.XKantEnd -= columnsChildDiff;
            WidthSizeCount(children.XKantEnd - columnsChildDiff);
        }

        private SiteMapObject GetPappasPappa(SiteMapObject pappa)
        {
            return ObjectList.Where(x => x.ObjectID == pappa.ObjectBeforID).First();
        }

        private SiteMapObject GetPappa(SiteMapObject child)
        {
            return ObjectList.Where(x => x.ObjectID == child.ObjectBeforID).First();
        }

        private void placePappa(SiteMapObject pappa)
        {
            List<SiteMapObject> childTempList = ObjectList.Where(x => x.ObjectBeforID == pappa.ObjectID).ToList();
            if (pappa.Show == true)
            {
                if (childTempList[0].Show)
                {
                    pappa.XPoint = ((((childTempList.Min(x => x.XPoint)) + recWidhtText + (childTempList.Max(x => x.XPoint))) / 2) - (recWidhtText / 2));
                }
                else
                {
                    pappa.XPoint = ((((childTempList.Min(x => x.XPoint)) + recWidht + (childTempList.Max(x => x.XPoint))) / 2) - (recWidht / 2));
                }

                WidthSizeCount(pappa.XPoint + recWidhtText + (columnsChildDiff / 2));
            }
            else
            {
                pappa.XPoint = ((((childTempList.Min(x => x.XPoint)) + recWidht + (childTempList.Max(x => x.XPoint))) / 2) - (recWidht / 2));
            }
            pappa.YPoint = GetYPoint(pappa.ObjectLevel);
            pappa.Placed = true;

            switch (LinesPatern)
            {
                case "1":
                    ContactLinesToRect(pappa);

                    break;

                case "2":
                    ContactLinesToRectDiagonal(pappa);
                    break;

                case "3":
                    break;

                default:
                    break;
            }
        }

        private bool DosPappaHaveBrodersThatAreNotPlaced(SiteMapObject pappa)
        {
            Brothers = ObjectList.Where(x => x.ObjectBeforID == pappa.ObjectBeforID && x.Placed == false).ToList();

            if (Brothers.Count() > 0)
                return true;

            return false;
        }

        private Level getALevelByID(int levelID)
        {
            return LevelList.Where(item => item.LevelId == levelID).First();
        }

        private bool DosBrodersHaveChildrenThatAreNotPlaced()
        {
            foreach (var item in Brothers)
            {
                if (ObjectList.Where(x => x.ObjectBeforID == item.ObjectID && x.Placed == false).ToList().Count() > 0)
                    return true;
            }
            return false;
        }

        private List<SiteMapObject> GetmostTempChildrenFromSamePapp(List<SiteMapObject> children)
        {
            var x = children.GroupBy(r => r.ObjectBeforID).OrderByDescending(t => t.Count()).SelectMany(s => s).ToList();
            return ObjectList.Where(e => e.ObjectBeforID == x[0].ObjectBeforID && e.Placed == false).ToList();
        }

        private Level TempChildrenLevel;
        private SiteMapObject pappa;
        private List<SiteMapObject> Brothers;
        private List<SiteMapObject> TempBrothers;

        public List<SiteMapObject> Build()
        {
            bool workInProgress = true;

            getObjectListToLevels();
            TempChildrenLevel = getLowestLevelWithBigestGroupOfChildrenInTempChild();
            if (TempChildrenLevel.XKantStart == 0 && TempChildrenLevel.XKantEnd == 0)
            {
                TempChildrenLevel.XKantStart = columnsDiff;
                TempChildrenLevel.XKantEnd = columnsDiff;
            }

            pappa = GetPappa(TempChildrenLevel.LevelChildTemp[0]);

            while (workInProgress)
            {
                if (TempChildrenLevel.LevelChildTemp.Count > 0)
                {
                    PlaceChildren(TempChildrenLevel);
                    foreach (var item in TempChildrenLevel.LevelChildTemp)
                    {
                        if (item.ObjectBeforID == Guid.Empty)
                        {
                            workInProgress = false;
                        }
                    }
                }
                placePappa(pappa);
                if (pappa.ObjectBeforID == Guid.Empty)
                {
                    workInProgress = false;
                }
                if (DosPappaHaveBrodersThatAreNotPlaced(pappa) && workInProgress)
                {
                    if (DosBrodersHaveChildrenThatAreNotPlaced())
                    {
                        bool theLowestChildrenInThisTree = true;
                        while (theLowestChildrenInThisTree)
                        {
                            TempBrothers = new List<SiteMapObject>();
                            foreach (var brother in Brothers)
                            {
                                if (ObjectList.Where(x => x.ObjectBeforID == brother.ObjectID && x.Placed == false).ToList().Count() > 0)
                                {
                                    var br = ObjectList.Where(x => x.ObjectBeforID == brother.ObjectID && x.Placed == false).ToList();
                                    foreach (var child in br)
                                    {
                                        TempBrothers.Add(child);
                                    }
                                }
                            }
                            if (TempBrothers.Count() > 0)
                            {
                                Brothers = TempBrothers;
                            }
                            else
                            {
                                TempChildrenLevel = getALevelByID(Brothers[0].ObjectLevel);
                                TempChildrenLevel.LevelChildTemp = GetmostTempChildrenFromSamePapp(Brothers);
                                theLowestChildrenInThisTree = false;
                                pappa = GetPappa(TempChildrenLevel.LevelChildTemp[0]);
                            }
                        }
                    }
                    else
                    {
                        TempChildrenLevel = getALevelByID(Brothers[0].ObjectLevel);
                        TempChildrenLevel.LevelChildTemp = Brothers;
                        TempChildrenLevel.LevelChildTemp = Brothers;
                        pappa = GetPappa(TempChildrenLevel.LevelChildTemp[0]);
                    }
                }
                else
                {
                    if (pappa.ObjectBeforID != Guid.Empty)
                        pappa = GetPappasPappa(pappa);
                }
            }
            List<SiteMapObject> checkNoamDelete = ObjectList.Where(x => x.ObjectName == "greenlight").ToList();
            return ObjectList;
        }
    }
}