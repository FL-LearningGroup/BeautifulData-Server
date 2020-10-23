using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Pipeline.News.FuYang.Models
{
    /// <summary>
    /// FuYang government announcement data model.
    /// </summary>
    [Serializable]
    public class DMGovermentAnnouncement
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string DetailedUrl { get; set; }
    }
}
