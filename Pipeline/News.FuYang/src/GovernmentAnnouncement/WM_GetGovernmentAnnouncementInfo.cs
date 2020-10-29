using BDS.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
using System.IO.Pipes;
using System.Globalization;
using BDS.Pipeline.News.FuYang.Models;

namespace BDS.Pipeline.News.FuYang.GovernmentAnnouncement
{
    public class WM_GetGovernmentAnnouncementInfo: IWorkMachine
    {
        public WorkSiteStatus Worker(IWorkSiteInput inputResource, IWorkSiteOutput outputResource)
        {
            List<string> resourceData = inputResource.TransferResourceDataToWorkSiteData();
            if (resourceData.Count == 0)
            {
                return WorkSiteStatus.Failed;
            }
            HtmlWeb htmlWeb = new HtmlWeb();
            try
            {
                //Get announcement first page by announcement link.
                string queryXpath = "//div[@class='is-main container']/div[@class='list-right']/div[@class='listright-box']/ul/li";
                string hostUrl = "http://www.fy.gov.cn";
                HtmlDocument announcementFirstPage;
                HtmlNodeCollection announcementNodes;
                DateTime firstAnnouncementDate;
                DateTime lastAnnouncementDate;
                DateTime today = System.DateTime.Now.Date.AddDays(-1);
                List<HtmlNodeCollection> dailyAnnouncement = new List<HtmlNodeCollection>();
                int announcementPageNum = 0;
                do
                {
                    announcementPageNum++;
                    Uri webUri = new Uri(resourceData.First() + String.Format("page-{0}/", announcementPageNum), UriKind.Absolute);
                    announcementFirstPage = htmlWeb.Load(webUri);
                    announcementNodes = announcementFirstPage.DocumentNode.SelectNodes(queryXpath);
                    firstAnnouncementDate = DateTime.ParseExact(announcementNodes.First().FirstChild.InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lastAnnouncementDate = DateTime.ParseExact(announcementNodes.Last().FirstChild.InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    if (today > firstAnnouncementDate && announcementPageNum == 1)
                    {
                        outputResource.TransferWorkSiteDataToResourceData(new List<string>());
                        return WorkSiteStatus.Success;
                    }
                    dailyAnnouncement.Add(announcementNodes);
                } while (lastAnnouncementDate >= today);

                List<string> announcemensData = new List<string>();
                foreach (HtmlNodeCollection announcementCollection in dailyAnnouncement)
                {
                    foreach (HtmlNode announcementNode in announcementCollection)
                    {
                        if (DateTime.ParseExact(announcementNode.ChildNodes[0].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture) < today)
                        {
                            break;
                        }
                        string publiInfo = announcementNode.ChildNodes[0].InnerText +
                                           GlobalConstant.TransferDataSplitValue + hostUrl + announcementNode.ChildNodes[1].Attributes["href"].Value +
                                           GlobalConstant.TransferDataSplitValue + announcementNode.ChildNodes[1].InnerText;
                        announcemensData.Add(publiInfo);
                    }
                }
                outputResource.TransferWorkSiteDataToResourceData(announcemensData);
                return WorkSiteStatus.Success;
            }
            catch (Exception ex)
            {
                return WorkSiteStatus.Failed;
            }
        }
    }
}
