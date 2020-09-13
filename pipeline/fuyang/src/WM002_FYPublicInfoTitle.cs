
namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BDS.CollectData;
    using HtmlAgilityPack;
    using BDS.CollectData.Models;
    using System.Diagnostics;
    internal class WM002_FYPublicInfoTitle : IWorkMachine
    {
        public WorkSiteStatus Worker(IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                List<string> urlList = input.GetResourceData();
                string hostUrl = "http://www.fy.gov.cn";
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
                List<string> dataStore = new List<string>();
                int collectNum = 0;
                foreach (string url in urlList)
                {
                    HtmlDocument htmlDoc = web.Load(url);
                    HtmlNodeCollection publicInfoTitleList = htmlDoc.DocumentNode.SelectNodes(xPath);
                    foreach (HtmlNode publicInfoTitle in publicInfoTitleList)
                    {

                        string publiInfo = publicInfoTitle.ChildNodes[0].InnerText +
                                           "@" + hostUrl + publicInfoTitle.ChildNodes[1].Attributes["href"].Value +
                                           "@" + publicInfoTitle.ChildNodes[1].InnerText;

                        dataStore.Add(publiInfo);
                    }
                    collectNum++;
                    if (collectNum == 2)
                    {
                        break;
                    }
                }
                output.StoreResourceData(dataStore);
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
