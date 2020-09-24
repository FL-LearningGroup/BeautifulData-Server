
//namespace bds.pipeline.fuyang
//{
//    using system;
//    using system.collections.generic;
//    using system.text;
//    using system.linq;
//    using bds.collectdata;
//    using bds.datafactory;
//    using bds.datareport;
//    using bds.datareport.model;
//    using system.threading.tasks;

//    /// <summary>
//    /// class: defined fu yang public info title.
//    /// </summary>
//    internal sealed class fypublicinfotitlecls : resource, iresource
//    {
//        private string type;
//        override public string type
//        {
//            get
//            {
//                return this.type;
//            }
//            set
//            {
//                this.type = value;
//            }
//        }
//        public list<fypublicinfotitledm> datastore = new list<fypublicinfotitledm>();

//        private gitfactory gitfactory = new gitfactory(config.selectdatafactoyinfo("worksite01", "gitrepo","local"), config.selectdatafactoyinfo("worksite01", "gitrepo", "remote"), config.selectdatafactoyinfo("worksite01", "gitrepo", "username"), config.selectdatafactoyinfo("worksite01", "gitrepo", "password"), config.selectdatafactoyinfo("worksite01", "gitrepo", "email"));
//        private emailclient emailclient = new emailclient(emailhosttype.outlook, config.selectemailhostinfo("worksite01", "outlook", "address"), 587, config.selectemailhostinfo("worksite01", "outlook", "useraddress"), config.selectemailhostinfo("worksite01", "outlook", "userpassword"));


//        public fypublicinfotitlecls(worksite worksite)
//        {
//            worksite.publicstatusevent += storedataevent;
//        }
//        /// <summary>
//        /// get data from property datastore.
//        /// </summary>
//        /// <returns>list string</returns
//        public list<string> getresourcedata()
//        {
//            list<string> urllist = new list<string>();
//            string basehost = "http://www.fy.gov.cn";
//            foreach (fypublicinfotitledm item in this.datastore)
//            {
//                urllist.add(basehost + item.url);
//            }
//            return urllist;
//        }
//        /// <summary>
//        /// store data to property datastore
//        /// </summary>
//        /// <param name="data">need data of store</param>
//        /// <returns>data counr</returns>
//        public system.int64 storeresourcedata(list<string> data)
//        {
//            //custom defined format of string that parse into filed datastore.
//            foreach (string item in data)
//            {
//                string[] strarray = item.split('@');
//                this.datastore.add(new fypublicinfotitledm(strarray[0], strarray[1], strarray[2]));
//            }
//            return this.datastore.count;
//        }
//        public async task generatedatafileandpush()
//        {
//            try
//            {
//                // pull repository from remote.
//                gitfactory.pullrepository();
//                var titlegrouplist =
//                    from data in datastore
//                    group data by data.datetime into newdata
//                    orderby newdata.key
//                    select newdata;
//                string jsonfile = string.empty;
//                // generate data json file.
//                foreach (var titlegroup in titlegrouplist)
//                {
//                    jsonfile = gitfactory.local + "title-" + titlegroup.key + ".json";
//                    await filefactory.serializationjsonasync(jsonfile, titlegroup);
//                }
//                // add changes to stage.
//                int32 changecount = gitfactory.addstagechanges();
//                // commit changes.
//                if (changecount != 0)
//                {
//                    gitfactory.commitchanges("add public title.");
//                }
//                // push changes to remote.
//                gitfactory.pushrepository();

//            }
//            catch(gitfactoryexception ex)
//            {
//                logger.error(ex.message);
//            }
//            catch(exception ex)
//            {
//                logger.error(ex.message);
//            }

//        }
//        public async task<bool> reportstoredata()
//        {
//            var titlegrouplist =
//                from data in datastore
//                group data by data.datetime into newdata
//                orderby newdata.key descending
//                select newdata;
//            string messagetitle = string.empty;
//            string message = string.empty;
//            foreach (var titlegroup in titlegrouplist)
//            {
//                messagetitle = "fuyang information: " + titlegroup.key;
//                foreach (var title in titlegroup)
//                {
//                    message += string.format(@"<tr>
//                                                <td>{0}</td>
//                                                <td>{1}</td>
//                                                <td><a href=""{2}"">{1}</a></td>
//                                                </tr>
//                                            ", title.datetime, title.title, title.url);
//                    break;
//                }
//                break;
//            }
//            try
//            {
//                emailcontext emailcontext = new emailcontext(emailcontexttype.html);
//                //emailcontext.fromperson.add(new contactperson() { name = "bds-collectdata", email = "lucasyao93@outlook.com" });
//                emailcontext.fromperson = config.selectemailcontactperson("worksite01", "outlook", "fromperson");
//                emailcontext.toperson = config.selectemailcontactperson("worksite01", "outlook", "toperson"); ;
//                emailcontext.subject = "bds-reportdata-fuyang";
//                emailcontext.message = string.format(@"<!doctype html>
//                                    <html>
//                                    <head>
//                                    <style>
//                                    table {{
//                                        font-family: arial, sans-serif;
//                                        border-collapse: collapse;
//                                    }}

//                                    td, th {{
//                                        border: 1px solid #dddddd;
//                                        text-align: left;
//                                        padding: 8px;
//                                    }}

//                                    tr:nth-child(even) {{
//                                        background-color: #dddddd;
//                                    }}
//                                    </style>
//                                    </head>
//                                    <body>

//                                    <h2>{0}</h2>

//                                    <table>
//                                        <tr>
//                                        <th>时间</th>
//                                        <th>标题</th>
//                                        <th>详细连接</th>
//                                        </tr>
//                                        {1}
//                                    </table>
//                                    </body>
//                                    </html>
//                                    ", messagetitle, message);
//                return await emailclient.sendeamilasync(emailcontext);
//            }
//            catch(filefactoryexception ex)
//            {
//                logger.error(ex.message);
//                return false;
//            }
//            catch(exception ex)
//            {
//                logger.error(ex.message);
//                return false;
//            }
//        }
//        private void storedataevent(object source, worksitestatuseventargs args)
//        {
//            if (args.status == collectdata.models.worksitestatus.success)
//            { 
//                generatedatafileandpush();
//                reportstoredata();
//            }
//        }
//    }
//}
