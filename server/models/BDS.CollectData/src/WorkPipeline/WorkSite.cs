namespace BDS.CollectData.WorkPipeline
{
    using System;
    using BDS.CollectData.WorkPipeline.Models;
    using BDS.CollectData.Resource;
    /// <summary>
    ///  The WorkSite performs process data through Worker method.
    /// </summary>
    internal class WorkSite: IWorkSite {
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
        
        IResource sourceResource;
        IResource targetResource;
        IWorkMachine workMachine;
        IWorkFilter workFilter;
        public WorkSite() {
            Initialize();
        }

        private void Initialize() {
            identifier = System.Guid.NewGuid();
        }
        public void Worker()
        {
            this.status = workMachine.worker(this.sourceResource, this.targetResource, this.workFilter);            
        }

        public void SetOrReplaceWorkMachine(IWorkMachine workMachine) {
            this.workMachine = workMachine;
        }
        public void SetOrReplaceWorkFilter(IWorkFilter workFilter) {
            this.workFilter = workFilter;
        }
    }
}