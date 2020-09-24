namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using BDS.CollectData;
    using BDS.CollectData.Models;

    public class Pipeline_FuYangNews
    {
        public static bool StartWork()
        {
            Logger.Info("Pipeline FuYang News: Start Work");
            Logger.Info("Start build pipeline.");
            Resource resourceHomePage = new Resource();
            Resource resourceTitleLinks = new Resource();
            resourceHomePage.Data.Add("HomePage", new List<string>() { @"http://www.fy.gov.cn/content/channel/54509807dfdd2e8475a9e38b/" });
            List<IWorkFilter> wf001 = new List<IWorkFilter>();
            wf001.Add(new WorkFilter()
            {
                TagName = "div",
                TagClass = "list-right"
            }
            );
            wf001.Add(new WorkFilter()
            {
                TagName = "div",
                TagClass = "listright-box"
            });
            wf001.Add(new WorkFilter()
            {
                TagName = "ul",
            });
            wf001.Add(new WorkFilter()
            {
                TagName = "li",
            });
            WM001_FYPublicInfoUrlLinks wm001_FYPublicInfoUrlLinks = new WM001_FYPublicInfoUrlLinks();
            WorkSite ws001 = new WorkSite();
            ws001.Name = "GetFyPublicInfoUrlLinks";
            ws001.Description = "Get page link of fy yang public info links.";
            ws001.SetOrReplaceWorkSiteInput(resourceHomePage).SetOrReplaceWorkSiteOutput(resourceTitleLinks).SetOrReplaceWorkFilter(wf001).SetOrReplaceWorkMachine(wm001_FYPublicInfoUrlLinks);
            ws001.takeElements = new List<string>() { "HomePage" };
            ws001.Status = WorkSiteStatus.Executable;

            /*
            Resource resourcePublicInfo = new Resource();
            List<IWorkFilter> wf002 = new List<IWorkFilter>();
            wf002.Add(new WorkFilter()
            {
                TagName = "div",
                TagClass = "list-right"
            });
            wf002.Add(new WorkFilter()
            {
                TagName = "div",
                TagClass = "listright-box"
            });
            wf002.Add(new WorkFilter()
            {
                TagName = "ul",
            });
            wf002.Add(new WorkFilter()
            {
                TagName = "li",
            });
            WM002_FYPublicInfoTitle wm002_FYPublicInfoTitle = new WM002_FYPublicInfoTitle();
            WorkSite ws002 = new WorkSite();
            ws002.Name = "GetPublicInfo";
            ws002.Description = "Get public info of fu yuang";
            ws002.SetOrReplaceWorkSiteInput(resourceTitleLinks).SetOrReplaceWorkSiteOutput(resourcePublicInfo).SetOrReplaceWorkFilter(wf002).SetOrReplaceWorkMachine(wm002_FYPublicInfoTitle);
            ws002.takeElements = new List<string>() { "AllTitleLinks" };
            ws002.Status = WorkSiteStatus.Executable;
            */
            WorkPipeline workPipeline = new WorkPipeline();
            workPipeline.AddWorkSite(ws001);
            //workPipeline.AddWorkSite(ws002);
            workPipeline.Status = WorkPipelineStatus.Executable;
            Logger.Info("Build Pipeline completed.");
            try
            {
                Logger.Info("Start up Pipeline.");
                workPipeline.Processor();
                Logger.Info("Pipeline execute completed.");
                return true;

            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Pipeline execute failed.{0}", ex.Message));
                return false;
            }
        }
    }
}
