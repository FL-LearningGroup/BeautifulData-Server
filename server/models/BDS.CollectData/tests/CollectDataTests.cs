using System;
using System.Text;
using System.Collections.Generic;
using Xunit;
using BDS.CollectData;
using BDS.CollectData.Models;


namespace BDS.CollectData.Tests
{
    internal class ResourceTest: Resource, IResource
    {
        private string _type;
        public override string Type
        {
            get
            {
                return "test";
            }
            set
            {
                this._type = "test";
            }

        }
        public List<string> GetResourceData()
        {
            return new List<string>()
            {
                "test"
            };
        }
        public System.Int64 StoreResourceData(List<string> data)
        {
            return 10;
        }

    }

    internal class WorkFilterTest: IWorkFilter
    {
        public string TagName
        {
            get;
            set;
        }
        public string TagClass
        {
            get;
            set;
        }
        public string TagId
        {
            get;
            set;
        }
    }
    internal class WokMachineTest: IWorkMachine
    {
        public WorkSiteStatus Worker(IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter)
        {
            return WorkSiteStatus.Success;
        }
    }
    public class CollectData
    {
        [Fact]
        public void WorkPipelineTest()
        {
            WorkPipeline wp = new WorkPipeline();

            ResourceTest rg01 = new ResourceTest();
            ResourceTest rg02 = new ResourceTest();
            List<IWorkFilter> wfList = new List<IWorkFilter>();
            wfList.Add(new WorkFilterTest()
            {
                TagName = "divTest",
                TagClass = "classTest"
            });
            WokMachineTest vm = new WokMachineTest();
            WorkSite ws01 = new WorkSite();
            ws01.SetOrReplaceWorkSiteInput(rg01);
            ws01.SetOrReplaceWorkSiteOutput(rg02);
            ws01.SetOrReplaceWorkFilter(wfList);
            ws01.SetOrReplaceWorkMachine(vm);
            Assert.NotNull(ws01.Identifier);

            // Add Works Site to Work Pipeline
            var wsCount = wp.AddWorkSite(ws01);
            Assert.Equal(1, wsCount);

            ResourceTest rg03 = new ResourceTest();
            WorkSite ws02 = new WorkSite();
            ws02.SetOrReplaceWorkSiteInput(rg02);
            ws02.SetOrReplaceWorkSiteOutput(rg03);
            ws02.SetOrReplaceWorkFilter(wfList);
            ws02.SetOrReplaceWorkMachine(vm);
            // Add Works Site to Work Pipeline
            wsCount = wp.AddWorkSite(ws02);
            Assert.Equal(2, wsCount);
            Assert.Equal(wp.WorkSiteLinked.First.Value.Identifier, ws01.Identifier);

            ResourceTest rg04 = new ResourceTest();
            WorkSite ws03 = new WorkSite();
            ws03.SetOrReplaceWorkSiteInput(rg03);
            ws03.SetOrReplaceWorkSiteOutput(rg04);
            ws03.SetOrReplaceWorkFilter(wfList);
            ws03.SetOrReplaceWorkMachine(vm);
            // Add Works Site to Work Pipeline
            wsCount = wp.AddWorkSite(ws03);
            Assert.Equal(3, wsCount);
            Assert.Equal(wp.WorkSiteLinked.Last.Value.Identifier, ws03.Identifier);

            ResourceTest rg05 = new ResourceTest();
            WorkSite ws04 = new WorkSite();
            ws04.SetOrReplaceWorkSiteInput(rg04);
            ws04.SetOrReplaceWorkSiteOutput(rg05);
            ws04.SetOrReplaceWorkFilter(wfList);
            ws04.SetOrReplaceWorkMachine(vm);
            // Add Works Site to Work Pipeline
            var operation = wp.InsertWorkSite(ws03, ws04);
            Assert.True(operation);
            Assert.Equal(wp.WorkSiteLinked.Last.Previous.Value.Identifier, ws04.Identifier);
        }
    }
}