using System;
using System.Text;
using System.Collections.Generic;
using Xunit;
using BDS.CollectData;
using BDS.CollectData.Models;
using BDS.CollectData.BDSException;


namespace BDS.CollectData.Tests
{
    internal class ResourceTest: Resource, IResource
    {
        private string _type;
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
    internal class WorkMachineTest: IWorkMachine
    {
        public WorkSiteStatus Worker(List<string> takeElments, IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter)
        {
            return WorkSiteStatus.Success;
        }
    }
    internal class WorkMachineExceptionTest : IWorkMachine
    {
        public WorkSiteStatus Worker(List<string> takeElments, IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter)
        {
            return WorkSiteStatus.Failed;
        }
    }
    public class CollectData
    {
        [Fact]
        public void WorkPipelineTest()
        {
            WorkPipeline wp = new WorkPipeline();

            Resource rg01 = new Resource();
            Resource rg02 = new Resource();
            List<IWorkFilter> wfList = new List<IWorkFilter>();
            wfList.Add(new WorkFilter()
            {
                TagName = "divTest",
                TagClass = "classTest"
            });
            WorkMachineTest vm = new WorkMachineTest();
            WorkSite ws01 = new WorkSite();
            ws01.SetOrReplaceWorkSiteInput(rg01).SetOrReplaceWorkSiteOutput(rg02).SetOrReplaceWorkFilter(wfList).SetOrReplaceWorkMachine(vm);
            Assert.NotNull(ws01.Identifier);

            // Add Works Site to Work Pipeline
            var oldWsCount = wp.WorkSiteLinked.Count;
            var wsCount = wp.AddWorkSite(ws01);
            Assert.Equal(oldWsCount + 1, wsCount);

            ResourceTest rg03 = new ResourceTest();
            WorkSite ws02 = new WorkSite();
            ws02.SetOrReplaceWorkSiteInput(rg02);
            ws02.SetOrReplaceWorkSiteOutput(rg03);
            ws02.SetOrReplaceWorkFilter(wfList);
            ws02.SetOrReplaceWorkMachine(vm);
            // Add Works Site to Work Pipeline
            oldWsCount = wp.WorkSiteLinked.Count;
            wsCount = wp.AddWorkSite(ws02);
            Assert.Equal(oldWsCount + 1, wsCount);
            Assert.Equal(wp.WorkSiteLinked.First.Value.Identifier, ws01.Identifier);

            ResourceTest rg04 = new ResourceTest();
            WorkSite ws03 = new WorkSite();
            ws03.SetOrReplaceWorkSiteInput(rg03);
            ws03.SetOrReplaceWorkSiteOutput(rg04);
            ws03.SetOrReplaceWorkFilter(wfList);
            ws03.SetOrReplaceWorkMachine(vm);
            // Add Works Site to Work Pipeline
            oldWsCount = wp.WorkSiteLinked.Count;
            wsCount = wp.AddWorkSite(ws03);
            Assert.Equal(oldWsCount + 1, wsCount);
            Assert.Equal(wp.WorkSiteLinked.Last.Value.Identifier, ws03.Identifier);

            //Test for insert Work Site
            ResourceTest rg05 = new ResourceTest();
            WorkSite ws04 = new WorkSite();
            ws04.SetOrReplaceWorkSiteInput(rg04);
            ws04.SetOrReplaceWorkSiteOutput(rg05);
            ws04.SetOrReplaceWorkFilter(wfList);
            ws04.SetOrReplaceWorkMachine(vm);
            // Add Works Site to Work Pipeline
            var operation = wp.InsertWorkSite(ws03.Identifier, ws04);
            Assert.True(operation);
            Assert.Equal(wp.WorkSiteLinked.Last.Previous.Value.Identifier, ws04.Identifier);

            //Test for remove Work Site.
            oldWsCount = wp.WorkSiteLinked.Count;
            wsCount = wp.RemoveWorkSite(ws03.Identifier);
            Assert.Equal(oldWsCount - 1, wsCount);
            Assert.Equal(wp.WorkSiteLinked.Last.Value.Identifier, ws04.Identifier);

            //Test for update Work Site.
            List<IWorkFilter> newWfList = new List<IWorkFilter>();
            newWfList.Add(new WorkFilterTest()
            {
                TagName = "newDivTest",
                TagClass = "newClassTest"
            });
            //left limit
            ws01.SetOrReplaceWorkFilter(newWfList);
            var updateOper = wp.UpdateWorkSite(ws01.Identifier, ws01);
            Assert.True(updateOper);

            //Right limit
            ws04.SetOrReplaceWorkFilter(newWfList);
            updateOper = wp.UpdateWorkSite(ws04.Identifier, ws04);
            Assert.True(updateOper);

            //Start Work Pipeline
            wp.Status = WorkPipelineStatus.Executable;
            Assert.Throws<WorkPipelineException>(() => wp.Processor());

            ws01.Status = WorkSiteStatus.Executable;
            ws02.Status = WorkSiteStatus.Executable;
            ws03.Status = WorkSiteStatus.Executable;
            ws04.Status = WorkSiteStatus.Executable;
            Assert.True(wp.Processor());

            WorkMachineExceptionTest workMachineExceptionTest = new WorkMachineExceptionTest();
            ws02.SetOrReplaceWorkMachine(workMachineExceptionTest);
            wp.Processor();
            Assert.Equal(wp.Status, WorkPipelineStatus.Failed);

            //Clear Work Site linked of Work Pipeline.
            wsCount = wp.ClearWorkPipeline();
            Assert.Equal(0, wsCount);
            Assert.Equal(wp.Status, WorkPipelineStatus.Build);
        }
    }
}