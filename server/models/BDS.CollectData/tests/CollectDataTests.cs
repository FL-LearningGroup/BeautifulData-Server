using System;
using System.Text;
using System.Collections.Generic;
using Xunit;
using HtmlAgilityPack;
using BDS.CollectData;
using BDS.CollectData.Models;


namespace BDS.CollectData.Tests
{
    /// <summary>
    /// FuYang origin resource.
    /// </summary>
    internal sealed class FYPublicInfoBaseUrl: Resource, IResource
    {
        private string type;
        override public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        public string dataStore;
        public List<string> GetResourceData()
        {
            return new List<string>()
            {
                { this.dataStore }
            };
        }

        public System.Int64 StoreResourceData(List<string> data)
        {
            foreach(string item in data)
            {
                this.dataStore = item;
            }
            return 1;
        }

    }
    /// <summary>
    /// Collect public info url links data model.
    /// </summary>
    internal class FYPiblicInfoUrlLinksDM
    {
        public string url;
        public FYPiblicInfoUrlLinksDM(string url)
        {
            this.url = url;
        }
    }
    /// <summary>
    /// Collect public info url links class.
    /// </summary>
    internal class FYPublicInfoUrlLinksCls : Resource, IResource
    {
        private string type;
        override public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        public List<FYPiblicInfoUrlLinksDM> dataStore = new List<FYPiblicInfoUrlLinksDM>();
        public List<string> GetResourceData()
        {
            List<string> urlList = new List<string>();
            foreach (FYPiblicInfoUrlLinksDM item in this.dataStore)
            {
                urlList.Add(item.url);
            }
            return urlList;
        }
        public System.Int64 StoreResourceData(List<string> data)
        {
            //Custom defined format of string that parse into filed datastore.
            foreach (string item in data)
            {
                string[] strArray = item.Split('@');
                foreach(string url in strArray)
                {
                    this.dataStore.Add(new FYPiblicInfoUrlLinksDM(url));
                }
            }
            return this.dataStore.Count;
        }
    }
    /// <summary>
    /// Collect public information data model.
    /// </summary>
    internal class FYPublicInfoTitleDM
    {
        public string dataTime;
        public string url;
        public string title;

        public FYPublicInfoTitleDM(string dataTime, string url, string title)
        {
            this.dataTime = dataTime;
            this.url = url;
            this.title = title;
        }
    }
    /// <summary>
    /// Collect public information class.
    /// </summary>
    internal sealed class FYPublicInfoTitleCls : Resource, IResource
    {
        private string type;
        override public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        public List<FYPublicInfoTitleDM> dataStore = new List<FYPublicInfoTitleDM>();
        public List<string> GetResourceData()
        {
            List<string> urlList = new List<string>();
            string baseHost = "http://www.fy.gov.cn";
            foreach (FYPublicInfoTitleDM item in this.dataStore)
            {
                urlList.Add(baseHost + item.url);
            }
            return urlList;
        }
        public System.Int64 StoreResourceData(List<string> data)
        {
            //Custom defined format of string that parse into filed datastore.
            foreach(string item in data)
            {
                string[] strArray = item.Split('@');
                this.dataStore.Add(new FYPublicInfoTitleDM(strArray[0], strArray[1], strArray[2]));
            }
            return this.dataStore.Count;
        }
    }
    /// <summary>
    /// Filter condition of named WM001.
    /// </summary>
    public class WorkFilter : IWorkFilter
    {
        private string tagName;
        private string tagId;
        private string tagClass;

        public WorkFilter()
        {
            this.tagName = "";
            this.tagId = "";
            this.TagClass = "";
        }
        public string TagName
        {
            get
            {
                return this.tagName;
            }
            set
            {
                this.tagName = value;
            }
        }

        public string TagId
        {
            get
            {
                return this.tagId;
            }
            set
            {
                this.tagId = value;
            }
        }
        public string TagClass
        {
            get
            {
                return this.tagClass;
            }
            set
            {
                this.tagClass = value;
            }
        }
    }
    /// <summary>
    /// Get all Fu Yang public info url links.
    /// </summary>
    public class WM001_FYPublicInfoUrlLinks : IWorkMachine
    {
        public WorkSiteStatus worker(IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter)
        {
            HtmlWeb web = new HtmlWeb();
            List<string> urlList = input.GetResourceData();
            //Get url from url list.
            string xPath = System.String.Empty;
            bool rootTag = true;
            foreach(IWorkFilter filter in workFilter)
            {
                if (rootTag)
                {
                    xPath += "//" + filter.TagName;
                    rootTag = false;
                }
                else
                {
                    xPath += "/" + filter.TagName;
                }
                if (filter.TagId != System.String.Empty)
                {
                    xPath += "[@id='" + filter.TagId + "']";
                }
                if (filter.TagClass != System.String.Empty)
                {
                    xPath += "[@class='" + filter.TagClass + "']";
                }
            }
            HtmlDocument htmlDoc = web.Load(urlList[0]);   
            HtmlNode pageRangeNum = htmlDoc.DocumentNode.SelectSingleNode(xPath);
            string[] pageMinMax = pageRangeNum.InnerText.Trim().Split('/');
            System.Int16 lastPage = Convert.ToInt16(pageMinMax[1]);
            List<string> dataStore = new List<string>();
            for (int index = 1; index <= lastPage; index++)
            {
                string url = urlList[0] + "page-" + index + "/";
                dataStore.Add(url);
            }
            output.StoreResourceData(dataStore);
            return WorkSiteStatus.Success;
        }
    }
    /// <summary>
    /// Get all Fu Yang public info
    /// </summary>
    public class WM002_FYPublicInfoTitle : IWorkMachine
    {
        public WorkSiteStatus worker(IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter)
        {
 
            HtmlWeb web = new HtmlWeb();
            List<string> urlList = input.GetResourceData();
            string hostUrl = "http://www.fy.gov.cn";
            //Get url from url list.
            string xPath = System.String.Empty;
            bool rootTag = true;
            foreach (IWorkFilter filter in workFilter)
            {
                if (rootTag)
                {
                    xPath += "//" + filter.TagName;
                    rootTag = false;
                }
                else
                {
                    xPath += "/" + filter.TagName;
                }
                if (filter.TagId != System.String.Empty)
                {
                    xPath += "[@id='" + filter.TagId + "']";
                }
                if (filter.TagClass != System.String.Empty)
                {
                    xPath += "[@class='" + filter.TagClass + "']";
                }
            }
            List<string> dataStore = new List<string>();
            int collectNum = 0;
            foreach(string url in urlList)
            {
                HtmlDocument htmlDoc = web.Load(url);
                HtmlNodeCollection publicInfoTitleList = htmlDoc.DocumentNode.SelectNodes(xPath);
                foreach(HtmlNode publicInfoTitle in publicInfoTitleList)
                {

                        string publiInfo = publicInfoTitle.ChildNodes[0].InnerText +
                                           "@" + hostUrl + publicInfoTitle.ChildNodes[1].Attributes["href"].Value +
                                           "@" + publicInfoTitle.ChildNodes[1].InnerText;

                    dataStore.Add(publiInfo);
                }
                collectNum++;
                if(collectNum == 5)
                {
                    break;
                }
            }
            output.StoreResourceData(dataStore);
            return WorkSiteStatus.Success;
        }

    }
    public class CollectDataTests
    {
        [Fact]
        public void ProcessFuYangNewsTest()
        {
            //public info base url
            const string html = @"http://www.fy.gov.cn/content/channel/54509807dfdd2e8475a9e38b/";
            FYPublicInfoBaseUrl fyPublicInfoBaseUrl = new FYPublicInfoBaseUrl();
            fyPublicInfoBaseUrl.Type = "url";
            fyPublicInfoBaseUrl.dataStore = html;

            //Collect all public info url links.
            FYPublicInfoUrlLinksCls fyPublicInfoUrlLinks = new FYPublicInfoUrlLinksCls();
            fyPublicInfoUrlLinks.Type = "url";

            //Select public info link from html.
            List<IWorkFilter> wm001_filterList = new List<IWorkFilter>();
            wm001_filterList.Add(new WorkFilter() {
                TagName = "div", 
                TagId = "page-list"
            });
            wm001_filterList.Add(new WorkFilter()
            {
                TagName = "span",
                TagClass = "currentpage"
            });
            //Work Machine process.
            WM001_FYPublicInfoUrlLinks wm001_FYPublicInfoUrlLinks = new WM001_FYPublicInfoUrlLinks();
            //wm001_FYPublicInfoUrlLinks.worker(fyPublicInfoBaseUrl, fyPublicInfoUrlLinks, wm001_filterList);
            
            //Get all public info title
            FYPublicInfoTitleCls fyPublicInfoTitle = new FYPublicInfoTitleCls();
            fyPublicInfoTitle.Type = "url";

            List<IWorkFilter> wm002_filterList = new List<IWorkFilter>();
            wm002_filterList.Add(new WorkFilter()
            {
                TagName = "div",
                TagClass = "list-right"
            });
            wm002_filterList.Add(new WorkFilter()
            {
                TagName = "div",
                TagClass = "listright-box"
            });
            wm002_filterList.Add(new WorkFilter()
            {
                TagName = "ul",
            });
            wm002_filterList.Add(new WorkFilter()
            {
                TagName = "li",
            });
            WM002_FYPublicInfoTitle wm002_FYPublicInfoTitle = new WM002_FYPublicInfoTitle();
            //wm002_FYPublicInfoTitle.worker(fyPublicInfoUrlLinks, fyPublicInfoTitle, wm002_filterList);

            WorkSite workSite001 = new WorkSite();
            workSite001.SetOrReplaceWorkSiteInput(fyPublicInfoBaseUrl);
            workSite001.SetOrReplaceWorkSiteOutput(fyPublicInfoUrlLinks);
            workSite001.SetOrReplaceWorkFilter(wm001_filterList);
            workSite001.SetOrReplaceWorkMachine(wm001_FYPublicInfoUrlLinks);

            WorkSite workSite002 = new WorkSite();
            workSite002.SetOrReplaceWorkSiteInput(fyPublicInfoUrlLinks);
            workSite002.SetOrReplaceWorkSiteOutput(fyPublicInfoTitle);
            workSite002.SetOrReplaceWorkFilter(wm002_filterList);
            workSite002.SetOrReplaceWorkMachine(wm002_FYPublicInfoTitle);

            WorkPipeline workPipeline = new WorkPipeline();
            workPipeline.AddWorkSite(workSite001);
            workPipeline.AddWorkSite(workSite002);
            workPipeline.status = WorkPipelineStatus.Executable;
            workPipeline.Processor();

        }
    }
}