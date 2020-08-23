
namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using BDS.CollectData;
    using BDS.DataFactory;
    using BDS.DataReport;
    using BDS.DataReport.Model;
    using System.Threading.Tasks;

    /// <summary>
    /// Class: Defined Fu Yang public info title.
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

        private GitFactory gitFactory = new GitFactory(Config.SelectDataFactoyInfo("WorkSite01", "GitRepo","local"), Config.SelectDataFactoyInfo("WorkSite01", "GitRepo", "remote"), Config.SelectDataFactoyInfo("WorkSite01", "GitRepo", "userName"), Config.SelectDataFactoyInfo("WorkSite01", "GitRepo", "password"), Config.SelectDataFactoyInfo("WorkSite01", "GitRepo", "email"));
        private EmailClient emailClient = new EmailClient(EmailHostType.Outlook, Config.SelectEmailHostInfo("WorkSite01", "outlook", "address"), 587, Config.SelectEmailHostInfo("WorkSite01", "outlook", "userAddress"), Config.SelectEmailHostInfo("WorkSite01", "outlook", "userPassword"));
        
        
        public FYPublicInfoTitleCls(WorkSite workSite)
        {
            workSite.publicStatusEvent += StoreDataEvent;
        }
        /// <summary>
        /// Get data from property dataStore.
        /// </summary>
        /// <returns>list string</returns
        public List<string> GetResourceData()
        {
            List<string> urlList = new List<string>();
            string baseHost = "http://www.fy.gov.cn";
            foreach (FYPublicInfoTitleDM item in this.dataStore)
            {
                urlList.Add(baseHost + item.Url);
            }
            return urlList;
        }
        /// <summary>
        /// Store data to property dataStore
        /// </summary>
        /// <param name="data">Need data of store</param>
        /// <returns>data counr</returns>
        public System.Int64 StoreResourceData(List<string> data)
        {
            //Custom defined format of string that parse into filed datastore.
            foreach (string item in data)
            {
                string[] strArray = item.Split('@');
                this.dataStore.Add(new FYPublicInfoTitleDM(strArray[0], strArray[1], strArray[2]));
            }
            return this.dataStore.Count;
        }
        public async Task GenerateDataFileAndPush()
        {
            try
            {
                // Pull repository from remote.
                gitFactory.PullRepository();
                var titleGroupList =
                    from data in dataStore
                    group data by data.DateTime into newData
                    orderby newData.Key
                    select newData;
                string jsonFile = string.Empty;
                // Generate data json file.
                foreach (var titleGroup in titleGroupList)
                {
                    jsonFile = gitFactory.Local + "title-" + titleGroup.Key + ".json";
                    await FileFactory.SerializationJsonAsync(jsonFile, titleGroup);
                }
                // Add changes to stage.
                Int32 changeCount = gitFactory.AddStageChanges();
                // Commit changes.
                if (changeCount != 0)
                {
                    gitFactory.CommitChanges("Add public title.");
                }
                // push changes to remote.
                gitFactory.PushRepository();

            }
            catch(GitFactoryException ex)
            {
                Logger.Error(ex.Message);
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }
        public async Task<bool> ReportStoreData()
        {
            var titleGroupList =
                from data in dataStore
                group data by data.DateTime into newData
                orderby newData.Key descending
                select newData;
            string messageTitle = String.Empty;
            string message = String.Empty;
            foreach (var titleGroup in titleGroupList)
            {
                messageTitle = "FuYang Information: " + titleGroup.Key;
                foreach (var title in titleGroup)
                {
                    message += string.Format(@"<tr>
                                                <td>{0}</td>
                                                <td>{1}</td>
                                                <td><a href=""{2}"">{1}</a></td>
                                                </tr>
                                            ", title.DateTime, title.Title, title.Url);
                    break;
                }
                break;
            }
            try
            {
                EmailContext emailContext = new EmailContext(EmailContextType.Html);
                //emailContext.FromPerson.Add(new ContactPerson() { Name = "BDS-CollectData", Email = "LucasYao93@outlook.com" });
                emailContext.FromPerson = Config.SelectEmailContactPerson("WorkSite01", "outlook", "fromPerson");
                emailContext.ToPerson = Config.SelectEmailContactPerson("WorkSite01", "outlook", "toPerson"); ;
                emailContext.Subject = "BDS-ReportData-FuYang";
                emailContext.Message = String.Format(@"<!DOCTYPE html>
                                    <html>
                                    <head>
                                    <style>
                                    table {{
                                        font-family: arial, sans-serif;
                                        border-collapse: collapse;
                                    }}

                                    td, th {{
                                        border: 1px solid #dddddd;
                                        text-align: left;
                                        padding: 8px;
                                    }}

                                    tr:nth-child(even) {{
                                        background-color: #dddddd;
                                    }}
                                    </style>
                                    </head>
                                    <body>

                                    <h2>{0}</h2>

                                    <table>
                                        <tr>
                                        <th>时间</th>
                                        <th>标题</th>
                                        <th>详细连接</th>
                                        </tr>
                                        {1}
                                    </table>
                                    </body>
                                    </html>
                                    ", messageTitle, message);
                return await emailClient.SendEamilAsync(emailContext);
            }
            catch(FileFactoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }
        private void StoreDataEvent(object source, WorkSiteStatusEventArgs args)
        {
            if (args.Status == CollectData.Models.WorkSiteStatus.Success)
            { 
                GenerateDataFileAndPush();
                ReportStoreData();
            }
        }
    }
}
