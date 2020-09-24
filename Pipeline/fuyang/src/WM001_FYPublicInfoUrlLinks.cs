namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BDS.CollectData;
    using HtmlAgilityPack;
    using BDS.CollectData.Models;
    using System.Linq;

    internal class WM001_FYPublicInfoUrlLinks : IWorkMachine
    {
        public WorkSiteStatus Worker(List<string> takeElements, IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                List<string> urlList = new List<string>();
                foreach (string element in takeElements)
                {
                    urlList = input.GetResourceData(element);
                }
                //Get url from url list.
                string xPath = System.String.Empty;
                bool rootTag = true;
                foreach (IWorkFilter filter in workFilter)
                {
                    if (rootTag)
                    {
                        xPath += "//" + filter.TagName;
                        rootTag = false;
                    }
                    else
                    {
                        xPath += "/" + filter.TagName;
                    }
                    if (!System.String.IsNullOrEmpty(filter.TagId))
                    {
                        xPath += "[@id='" + filter.TagId + "']";
                    }
                    if (!System.String.IsNullOrEmpty(filter.TagClass))
                    {
                        xPath += "[@class='" + filter.TagClass + "']";
                    }
                }
                HtmlDocument htmlDoc = web.Load(urlList[0]);
                HtmlNodeCollection publicInfoCollection = htmlDoc.DocumentNode.SelectNodes(xPath);
                string lastDateTimeStr = publicInfoCollection.Last().FirstChild.InnerText;
                DateTime lastPublicInfoDate = DateTime.ParseExact(publicInfoCollection.Last().FirstChild.InnerText, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
                int pageIndex = 1;
                while (lastPublicInfoDate >= DateTime.Now.AddDays(-20).Date)
                {
                    pageIndex++;
                    string url = urlList[0] + "page-" + pageIndex + "/";
                    htmlDoc = web.Load(url);
                    foreach(HtmlNode htmlNode in htmlDoc.DocumentNode.SelectNodes(xPath))
                    {
                        publicInfoCollection.Append(htmlNode);
                    }
                    lastDateTimeStr = publicInfoCollection.Last().FirstChild.InnerText;
                    lastPublicInfoDate = DateTime.ParseExact(publicInfoCollection.Last().FirstChild.InnerText, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
                }
                IEnumerable<HtmlNode> currentDayPublicInfo = from publicInfo in publicInfoCollection
                                           let date = publicInfo.FirstChild.InnerText
                                           where date == DateTime.Now.ToString("yyyy-MM-dd")
                                           select publicInfo
                                           ;

                //output.StoreResourceData("AllTitleLinks",dataStore);
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
                return WorkSiteStatus.Failed;
            }
            return WorkSiteStatus.Success;
        }
    }
}
