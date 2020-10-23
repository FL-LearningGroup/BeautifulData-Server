using System;
using System.Collections.Generic;
using System.Text;
using BDS.Framework;
using BDS.Pipeline.News.FuYang.GovernmentAnnouncement;

namespace BDS.Pipeline.News.FuYang
{
    public class Pipeline_GonvermentAnnouncement: WorkPipeline
    {
        public Pipeline_GonvermentAnnouncement():base()
        {
            Initialize();
        }

        private void Initialize()
        {
            ResGovernmentAnnouncementLink resGovernmentAnnouncementLink = new ResGovernmentAnnouncementLink();
            ResAnnouncement resAnnouncement = new ResAnnouncement();
            WorkSite announcementWorkSite = new WorkSite();
            WM_GetGovernmentAnnouncementInfo wm_GetGovernmentAnnouncementInfo = new WM_GetGovernmentAnnouncementInfo();
            announcementWorkSite.SetOrReplaceWorkSiteInput(resGovernmentAnnouncementLink)
                                .SetOrReplaceWorkSiteOutput(resAnnouncement)
                                .SetOrReplaceWorkMachine(wm_GetGovernmentAnnouncementInfo);
            announcementWorkSite.AddResourceEventHandler(resAnnouncement);
            announcementWorkSite.Status = WorkSiteStatus.Executable;
            AddWorkSite(announcementWorkSite);
            Status = WorkPipelineStatus.Executable;
        }
    }
}
