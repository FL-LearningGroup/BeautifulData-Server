using System;
using System.Collections.Generic;
using System.Text;
using BDS.Framework;
using BDS.Pipeline.News.FuYang.GovernmentAnnouncement;

namespace BDS.Pipeline.News.FuYang
{
    public class Pipeline_GonvermentAnnouncement
    {
        public static bool StartWork()
        {
            ResGovernmentAnnouncementLink resGovernmentAnnouncementLink = new ResGovernmentAnnouncementLink();
            ResAnnouncement resAnnouncement = new ResAnnouncement();
            WorkSite announcementWorkSite = new WorkSite();
            WM_GetGovernmentAnnouncementInfo wm_GetGovernmentAnnouncementInfo = new WM_GetGovernmentAnnouncementInfo();
            announcementWorkSite.SetOrReplaceWorkSiteInput(resGovernmentAnnouncementLink).SetOrReplaceWorkSiteOutput(resAnnouncement).SetOrReplaceWorkMachine(wm_GetGovernmentAnnouncementInfo);
            announcementWorkSite.AddResourceEventHandler(resAnnouncement);
            announcementWorkSite.Status = WorkSiteStatus.Executable;
            WorkPipeline announcementPieline = new WorkPipeline();
            announcementPieline.AddWorkSite(announcementWorkSite);
            announcementPieline.Status = WorkPipelineStatus.Executable;
            try
            {
               WorkPipelineStatus workPipelineStatus =  announcementPieline.Processor();
                if (workPipelineStatus == WorkPipelineStatus.Failed)
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
               
                return true;
            }

        }
    }
}
