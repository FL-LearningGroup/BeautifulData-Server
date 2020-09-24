namespace BDS.CollectData
{
    using System;
    using System.Collections.Generic;
    using BDS.CollectData.Models;
    /// <summary>
    ///  The WorkSite performs process data through Worker method.
    /// </summary>
    [Serializable]
    public class WorkSite: IWorkSite {
        WorkSiteStatus _status = WorkSiteStatus.Build;
        public event EventHandler<WorkSiteStatusEventArgs> publicStatusEvent;
        public WorkSiteStatus Status {
            get {
                return _status;
            }
            set
            {
                WorkSiteStatusEventArgs workSiteStatusEventArgs = new WorkSiteStatusEventArgs();
                _status = value;
                if( _status == WorkSiteStatus.Success && publicStatusEvent != null)
                {
                    workSiteStatusEventArgs.Status = WorkSiteStatus.Success;
                    publicStatusEvent(this, workSiteStatusEventArgs);
                }
            }

        }
        public List<string> takeElements { get; set; }
        private string _identifier;

        public string Identifier {
            get 
            {
                return _identifier;
            }
            set
            {
                _identifier = value;
            }
        }
        
        IWorkSiteInput _inputResource;
        public IWorkSiteInput InputResource
        {
            get
            {
                return _inputResource;
            }
        }
        IWorkSiteOutput _outputResource;
        public IWorkSiteOutput OutputResource
        {
            get
            {
                return _outputResource;
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }

        IWorkMachine _workMachine;
        List<IWorkFilter> _workFilterList;
        public WorkSite() {
            Initialize();
        }

        private void Initialize() {
            _identifier = System.Guid.NewGuid().ToString();
        }
        public IWorkSite Worker()
        {
            Status = WorkSiteStatus.Running;
            Status = _workMachine.Worker(takeElements, _inputResource, _outputResource, _workFilterList);
            return this;
        }
        public IWorkSite SetOrReplaceWorkSiteInput(IWorkSiteInput inputResource) {
            _inputResource = inputResource;
            return this;
        }
        public IWorkSite SetOrReplaceWorkSiteOutput(IWorkSiteOutput outputResource) {
            _outputResource = outputResource;
            return this;
        }
        public IWorkSite SetOrReplaceWorkMachine(IWorkMachine workMachine) {
            _workMachine = workMachine;
            return this;
        }
        public IWorkSite SetOrReplaceWorkFilter(List<IWorkFilter> workFilter) {
            _workFilterList = workFilter;
            return this;
        }
    }
}