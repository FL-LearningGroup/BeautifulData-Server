namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BDS.CollectData;
    using HtmlAgilityPack;
    using BDS.CollectData.Models;
    public class WM001_FYPublicInfoUrlLinks : IWorkMachine
    {
        public WorkSiteStatus Worker(IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                List<string> urlList = input.GetResourceData();
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
                    if (filter.TagId != System.String.Empty)
                    {
                        xPath += "[@id='" + filter.TagId + "']";
                    }
                    if (filter.TagClass != System.String.Empty)
                    {
                        xPath += "[@class='" + filter.TagClass + "']";
                    }
                }
                HtmlDocument htmlDoc = web.Load(urlList[0]);
                HtmlNode pageRangeNum = htmlDoc.DocumentNode.SelectSingleNode(xPath);
                string[] pageMinMax = pageRangeNum.InnerText.Trim().Split('/');
                System.Int16 lastPage = Convert.ToInt16(pageMinMax[1]);
                List<string> dataStore = new List<string>();
                for (int index = 1; index <= lastPage; index++)
                {
                    string url = urlList[0] + "page-" + index + "/";
                    dataStore.Add(url);
                }
                output.StoreResourceData(dataStore);
            }catch(Exception ex)
            {
                Logger.Error(ex.Message);
                return WorkSiteStatus.Failed;
            }
            return WorkSiteStatus.Success;
        }
    }
}
