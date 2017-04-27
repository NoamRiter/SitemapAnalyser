using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.XPath;

namespace SitemapAnalyser.Models
{
    public class SiteMapGenerator
    {
        private List<SiteMapObject> ListOfSiteMapObjects = new List<SiteMapObject>();

        public List<SiteMapObject> GetAllObjects(string path)
        {
            getXMLToObject(path);
            return ListOfSiteMapObjects;
        }

        private void getXMLToObject(string path)
        {
            int showLevel = 1;
            if (path != null)
            {
                showLevel = path.Split('/').Count();
            }
            XPathDocument doc = null;
            XPathNavigator nav = null;
            XPathNodeIterator itorer = null;
            string mapLoc = HttpContext.Current.Request.MapPath("../App_Data/SiteMapFiles/Sitemap.xml");

            try
            {
                doc = new XPathDocument(mapLoc);
                nav = doc.CreateNavigator();
                itorer = (XPathNodeIterator)nav.Evaluate("//*");

                foreach (XPathNavigator node in itorer)
                {
                    if (node.Name == "loc")
                    {
                        string fullPath = System.Uri.UnescapeDataString(node.Value);

                        fullPath = fullPath.Replace("http://", "ROOT/");
                        char lastOne = fullPath[fullPath.Length - 1];
                        if (lastOne == '/')
                        {
                            fullPath = fullPath.Remove(fullPath.Length - 1);
                        }

                        if (path != null)
                        {
                            string pathh = path + "/";
                            string fullPathh = fullPath + "/";
                            if (fullPathh.StartsWith(pathh))
                            {
                                ObjectifyFullPath(fullPath, showLevel);
                            }
                        }
                        else
                        {
                            string[] stringArray = fullPath.Split('/');
                            if (fullPath.StartsWith(stringArray[0]))
                            {
                                ObjectifyFullPath(fullPath, showLevel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private bool ObjectExists(string objectName)
        {
            bool result = false;
            if (ListOfSiteMapObjects != null)
            {
                for (int objectCount = 0; objectCount < ListOfSiteMapObjects.Count(); objectCount++)
                {
                    if (objectName == ListOfSiteMapObjects[objectCount].ObjectName)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private SiteMapObject getObjectBefor(int place, string fullPath)
        {
            string[] stringArray = fullPath.Split('/');

            SiteMapObject obj = ListOfSiteMapObjects.Where(x => x.ObjectName == stringArray[0]).Single();

            if (place > 1)
            {
                for (int i = 1; i <= place - 1; i++)
                {
                    SiteMapObject objnext = ListOfSiteMapObjects.Where(x => x.ObjectName == stringArray[i] && x.ObjectBeforID == obj.ObjectID).Single();
                    obj = objnext;
                }
            }
            return obj;
        }

        private SiteMapObject GetObjectBYID(Guid ID)
        {
            SiteMapObject objBefor = ListOfSiteMapObjects.Where(x => x.ObjectBeforID == ID).Single();
            return objBefor;
        }

        private bool IsObjectExistsInSamePath(string objName, string fullPath, int place)
        {
            bool result = false;
            List<SiteMapObject> objList = ListOfSiteMapObjects.Where(x => x.ObjectName == objName).ToList();

            for (int i = 0; i < objList.Count(); i++)
            {
                string newFullPath = objList[i].ObjectName;
                SiteMapObject objToList = ListOfSiteMapObjects.Where(x => x.ObjectID == objList[i].ObjectBeforID).Single();
                newFullPath = objToList.ObjectName + "/" + newFullPath;

                for (int z = objToList.ObjectLevel; z > 0; z--)
                {
                    objToList = ListOfSiteMapObjects.Where(x => x.ObjectID == objToList.ObjectBeforID).Single();
                    newFullPath = objToList.ObjectName + "/" + newFullPath;
                }

                if (string.Compare(fullPath, newFullPath) == 0)
                {
                    result = true;
                }
            }
            return result;
        }

        private string GetRelativePath(string fullPath, int level)
        {
            string result = "";
            string[] pathArray = fullPath.Split('/');
            for (int i = 0; i < level + 1; i++)
            {
                result += $"{pathArray[i]}/";
            }
            return result.Remove(result.Length - 1); ;
        }

        private void ObjectifyFullPath(string fullPath, int showLevel)
        {
            string[] stringArray = fullPath.Split('/');
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (ObjectExists(stringArray[i]))
                {
                    if (i != 0)
                    {
                        string newFullPath = stringArray[i];

                        for (int a = i - 1; a >= 0; a--)
                        {
                            newFullPath = stringArray[a] + "/" + newFullPath;
                        }
                        bool ObjectExistsInSamePath = IsObjectExistsInSamePath(stringArray[i], newFullPath, i);

                        if (!ObjectExistsInSamePath)
                        {
                            SiteMapObject obj = new SiteMapObject();
                            SiteMapObject objBefor = getObjectBefor(i, fullPath);
                            obj.ObjectLevel = i;
                            obj.ObjectPath = GetRelativePath(fullPath, i);
                            obj.ObjectName = stringArray[i];
                            obj.ObjectBeforID = objBefor.ObjectID;
                            obj.ObjectBeforLevel = objBefor.ObjectLevel;
                            if (i < (showLevel + 1))
                            {
                                obj.Show = true;
                            }

                            ListOfSiteMapObjects.Add(obj);
                        }
                    }
                }
                else
                {
                    SiteMapObject obj = new SiteMapObject();
                    if (i == 0)
                    {
                        obj.ObjectLevel = i;
                        obj.ObjectName = stringArray[i];
                        obj.ObjectBeforID = Guid.Empty;
                        obj.ObjectBeforLevel = i;
                        obj.ObjectPath = GetRelativePath(fullPath, i);
                        if (i < (showLevel + 1))
                        {
                            obj.Show = true;
                        }
                        ListOfSiteMapObjects.Add(obj);
                    }
                    else
                    {
                        SiteMapObject objBefor = getObjectBefor(i, fullPath);
                        obj.ObjectLevel = i;
                        obj.ObjectName = stringArray[i];
                        obj.ObjectBeforID = objBefor.ObjectID;
                        obj.ObjectBeforLevel = objBefor.ObjectLevel;
                        obj.ObjectPath = GetRelativePath(fullPath, i);
                        if (i < (showLevel + 1))
                        {
                            obj.Show = true;
                        }
                        ListOfSiteMapObjects.Add(obj);
                    }
                }
            }
        }
    }
}