namespace BDS.CollectData
{
    using System;
    using System.Collections.Generic;
    using BDS.CollectData.Models;
    /// <summary>
    ///  The WorkSite performs process data through Worker method.
    /// </summary>
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
                if( _status == WorkSiteStatus.Success)
                {
                    workSiteStatusEventArgs.Status = WorkSiteStatus.Success;
                    publicStatusEvent(this, workSiteStatusEventArgs);
                }
            }

        }
        private string _identifier;

        public string Identifier {
            get {
                return _identifier;
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
        IWorkMachine _workMachine;
        List<IWorkFilter> _workFilterList;
        public WorkSite() {
            Initialize();
        }

        private void Initialize() {
            _identifier = System.Guid.NewGuid().ToString();
        }
        public void Worker()
        {
            Status = WorkSiteStatus.Running;
            Status = _workMachine.Worker(_inputResource, _outputResource, _workFilterList);            
        }
        public void SetOrReplaceWorkSiteInput(IWorkSiteInput inputResource) {
            _inputResource = inputResource;
        }
        public void SetOrReplaceWorkSiteOutput(IWorkSiteOutput outputResource) {
            _outputResource = outputResource;
        }
        public void SetOrReplaceWorkMachine(IWorkMachine workMachine) {
            _workMachine = workMachine;
        }
        public void SetOrReplaceWorkFilter(List<IWorkFilter> workFilter) {
            _workFilterList = workFilter;
        }
    }
}