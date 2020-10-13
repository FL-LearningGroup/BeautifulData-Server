using System;
using System.Collections.Generic;
using System.Text;
using BDS.Framework;
using BDS.Pipeline.News.FuYang.Models;

namespace BDS.Pipeline.News.FuYang.GovernmentAnnouncement
{
    internal class ResGovernmentAnnouncementLink: IWorkSiteInput
    {
        public DMGovernmentNewsLink Data
        {
            get;
            set;
        }
        public ResGovernmentAnnouncementLink()
        {
            Data = new DMGovernmentNewsLink();
            Data.Announcement = @"http://www.fy.gov.cn/content/channel/54509807dfdd2e8475a9e38b/";
        }

        public List<string> TransferResourceDataToWorkSiteData()
        {
            return new List<string>() { Data.Announcement };
        }
    }
}
