using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDS.Framework;
using BDS.Pipeline.News.FuYang.Models;

namespace BDS.Pipeline.News.FuYang.GovernmentAnnouncement
{
    public class ResGovernmentAnnouncementLink : Resource<DMGovernmentNewsLink>
    {
        private List<DMGovernmentNewsLink> _data;
        public override List<DMGovernmentNewsLink> Data
        {
            get
            {
                return _data;
            }
        }
        public ResGovernmentAnnouncementLink()
        {
            _data = new List<DMGovernmentNewsLink>();
            _data.Add(new DMGovernmentNewsLink { Announcement = @"http://www.fy.gov.cn/content/channel/54509807dfdd2e8475a9e38b/" });
        }

        public override List<string> TransferResourceDataToWorkSiteData()
        {
            return new List<string>() { _data.First().Announcement };
        }
        public override void TransferWorkSiteDataToResourceData(List<string> data)
        {
            
        }
    }
}
