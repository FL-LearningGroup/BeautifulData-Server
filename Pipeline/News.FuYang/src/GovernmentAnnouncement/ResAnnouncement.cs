using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using BDS.Framework;
using BDS.Framework.Models;
using BDS.Pipeline.News.FuYang.Models;

namespace BDS.Pipeline.News.FuYang.GovernmentAnnouncement
{
    public class ResAnnouncement : Resource<DMGovermentAnnouncement>, IResourceEventHandle
    {
        private List<DMGovermentAnnouncement> _data;
        public override List<DMGovermentAnnouncement> Data { get { return _data; } }
        public ResAnnouncement()
        {
            _data = new List<DMGovermentAnnouncement>();
            mailServerLoginInfo = new MailServerLoginInfo { Host = "smtp-mail.outlook.com", Port = 587, SecureSocketOptions = true, UserName = "LucasYao93@outlook.com", Password = "yaodi@960903" };
            mailInfo = new MailInfo (
                subject: "BDS-ReportData-FuYang",
                fromPerson: new List<MailContactPerson>() { new MailContactPerson (name: "BDS-CollectData", address: "LucasYao93@outlook.com" ) },
                toPerson: new List<MailContactPerson>() { new MailContactPerson ( name: "LucasYao", address: "LucasYao93@outlook.com" ) }
            );
        }
        public void ResourceDataEvent(object sender, WorkSiteStatusTriggerEventArgs e)
        {
            if (e.Status == WorkSiteStatus.Success)
            {
                //MailServerLoginInfo mailServerLoginInfo, MailInfo mailInfo, MailBodyType mailBodyType
                this.SendEmailAsync<DMGovermentAnnouncement>(mailServerLoginInfo, mailInfo, MailBodyType.HTMLTable);
            }
        }
        public override void TransferWorkSiteDataToResourceData(List<string> data)
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
        public override List<string> TransferResourceDataToWorkSiteData()
        {
            return null;
        }

    }
}
