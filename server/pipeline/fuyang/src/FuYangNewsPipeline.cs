namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using BDS.CollectData;
    using BDS.CollectData.Models;

    public class FuYangNewsPipeline
    {
        public static string StartWork()
        {
            //public info base url
            WorkSite workSite001 = new WorkSite();

            const string html = @"http://www.fy.gov.cn/content/channel/54509807dfdd2e8475a9e38b/";
            FYPublicInfoBaseUrl fyPublicInfoBaseUrl = new FYPublicInfoBaseUrl();
            fyPublicInfoBaseUrl.Type = "url";
            fyPublicInfoBaseUrl.dataStore = html;

            //Collect all public info url links.
            FYPublicInfoUrlLinksCls fyPublicInfoUrlLinks = new FYPublicInfoUrlLinksCls();
            fyPublicInfoUrlLinks.Type = "url";

            //Select public info link from html.
            List<IWorkFilter> wm001_filterList = new List<IWorkFilter>();
            wm001_filterList.Add(new FyYangPublicInfoFilter()
            {
                TagName = "div",
                TagId = "page-list"
            });
            wm001_filterList.Add(new FyYangPublicInfoFilter()
            {
                TagName = "span",
                TagClass = "currentpage"
            });
            //Work Machine process.
            WM001_FYPublicInfoUrlLinks wm001_FYPublicInfoUrlLinks = new WM001_FYPublicInfoUrlLinks();
            workSite001.SetOrReplaceWorkSiteInput(fyPublicInfoBaseUrl);
            workSite001.SetOrReplaceWorkSiteOutput(fyPublicInfoUrlLinks);
            workSite001.SetOrReplaceWorkFilter(wm001_filterList);
            workSite001.SetOrReplaceWorkMachine(wm001_FYPublicInfoUrlLinks);
            workSite001.Status = WorkSiteStatus.Executable;

            //Get all public info title
            WorkSite workSite002 = new WorkSite();

            FYPublicInfoTitleCls fyPublicInfoTitle = new FYPublicInfoTitleCls(workSite002);
            fyPublicInfoTitle.Type = "url";

            List<IWorkFilter> wm002_filterList = new List<IWorkFilter>();
            wm002_filterList.Add(new FyYangPublicInfoFilter()
            {
                TagName = "div",
                TagClass = "list-right"
            });
            wm002_filterList.Add(new FyYangPublicInfoFilter()
            {
                TagName = "div",
                TagClass = "listright-box"
            });
            wm002_filterList.Add(new FyYangPublicInfoFilter()
            {
                TagName = "ul",
            });
            wm002_filterList.Add(new FyYangPublicInfoFilter()
            {
                TagName = "li",
            });
            WM002_FYPublicInfoTitle wm002_FYPublicInfoTitle = new WM002_FYPublicInfoTitle();


            
            workSite002.SetOrReplaceWorkSiteInput(fyPublicInfoUrlLinks);
            workSite002.SetOrReplaceWorkSiteOutput(fyPublicInfoTitle);
            workSite002.SetOrReplaceWorkFilter(wm002_filterList);
            workSite002.SetOrReplaceWorkMachine(wm002_FYPublicInfoTitle);
            workSite002.Status = WorkSiteStatus.Executable;

            WorkPipeline workPipeline = new WorkPipeline();
            workPipeline.AddWorkSite(workSite001);
            workPipeline.AddWorkSite(workSite002);
            workPipeline.Status = WorkPipelineStatus.Executable;
            try
            {
                workPipeline.Processor();
                //fyPublicInfoTitle.GenerateDataFileAndPush().Wait();
                //fyPublicInfoTitle.ReportStoreData().Wait();
                //workSite002.Status = WorkSiteStatus.Success; // Can trigger event.
                //IWorkSite workSite = workSite002;
                //workSite.Status = WorkSiteStatus.Success; // Can trigger event.
                return JsonSerializer.Serialize(fyPublicInfoTitle.dataStore);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Work pipeline failed.";
            }
        }
    }
}
