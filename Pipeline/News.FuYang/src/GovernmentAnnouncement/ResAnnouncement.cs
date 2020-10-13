using System;
using System.Collections.Generic;
using System.Text;
using BDS.Pipeline.News.FuYang.Models;
using BDS.Framework;
using System.Reflection;

namespace BDS.Pipeline.News.FuYang.GovernmentAnnouncement
{
    internal class ResAnnouncement: IWorkSiteOutput, IResourceEventHandle
    {
        private List<DMGovermentAnnouncement> _data;
        public List<DMGovermentAnnouncement> Data { get { return _data; } }
        public ResAnnouncement()
        {
            _data = new List<DMGovermentAnnouncement>();
        }
        public void ResourceDataEvent(object sender, WorkSiteStatusTriggerEventArgs e)
        {
            if (e.Status == WorkSiteStatus.Success)
            {
                Console.WriteLine(_data.ToString());
            }
        }
        public void TransferWorkSiteDataToResourceData(List<string> data)
        {
            //String format: xxxx?_?
            foreach (string item in data)
            {
                string[] dataModelElements = item.Split(GlobalConstant.TransferDataSplitValue);
                PropertyInfo[] propertyInfos = typeof(DMGovermentAnnouncement).GetProperties();
                if (dataModelElements.Length >= propertyInfos.Length)
                {
                    _data.Add(new DMGovermentAnnouncement { Date = dataModelElements[0], DetailedUrl = dataModelElements[1], Title = dataModelElements[2] });
                }       
                else
                {
                    _data.Add(new DMGovermentAnnouncement { Title = GlobalConstant.TransferDataDefaultValue, DetailedUrl = GlobalConstant.TransferDataDefaultValue, Date = GlobalConstant.TransferDataDefaultValue });
                }

            }
        }

    }
}
