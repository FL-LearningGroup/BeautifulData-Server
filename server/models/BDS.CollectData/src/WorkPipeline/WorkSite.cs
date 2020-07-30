namespace BDS.CollectData
{
    using System;
    using System.Collections.Generic;
    using BDS.CollectData.Models;
    /// <summary>
    ///  The WorkSite performs process data through Worker method.
    /// </summary>
    public class WorkSite: IWorkSite {
        WorkSiteStatus status = WorkSiteStatus.Build;

        public WorkSiteStatus Status {
            get {
                return this.status;
            }
        }
        Guid identifier;

        public Guid Identifier {
            get {
                return this.identifier;
            }
        }
        
        IWorkSiteInput inputResource;
        IWorkSiteOutput outputResource;
        IWorkMachine workMachine;
        List<IWorkFilter> workFilterList;
        public WorkSite() {
            Initialize();
        }

        private void Initialize() {
            identifier = System.Guid.NewGuid();
        }
        public void Worker()
        {
            this.status = workMachine.worker(this.inputResource, this.outputResource, this.workFilterList);            
        }
        public void SetOrReplaceWorkSiteInput(IWorkSiteInput inputResource) {
            this.inputResource = inputResource;
        }
        public void SetOrReplaceWorkSiteOutput(IWorkSiteOutput outputResource) {
            this.outputResource = outputResource;
        }
        public void SetOrReplaceWorkMachine(IWorkMachine workMachine) {
            this.workMachine = workMachine;
        }
        public void SetOrReplaceWorkFilter(List<IWorkFilter> workFilter) {
            this.workFilterList = workFilter;
        }
    }
}