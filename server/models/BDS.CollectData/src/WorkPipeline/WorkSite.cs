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
                return this._status;
            }
            set
            {
                WorkSiteStatusEventArgs workSiteStatusEventArgs = new WorkSiteStatusEventArgs();
                this._status = value;
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
                return this._identifier;
            }
        }
        
        IWorkSiteInput _inputResource;
        public IWorkSiteInput InputResource
        {
            get
            {
                return this._inputResource;
            }
        }
        IWorkSiteOutput _outputResource;
        public IWorkSiteOutput OutputResource
        {
            get
            {
                return this._outputResource;
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
            this._status = _workMachine.Worker(this._inputResource, this._outputResource, this._workFilterList);            
        }
        public void SetOrReplaceWorkSiteInput(IWorkSiteInput _inputResource) {
            this._inputResource = _inputResource;
        }
        public void SetOrReplaceWorkSiteOutput(IWorkSiteOutput _outputResource) {
            this._outputResource = _outputResource;
        }
        public void SetOrReplaceWorkMachine(IWorkMachine _workMachine) {
            this._workMachine = _workMachine;
        }
        public void SetOrReplaceWorkFilter(List<IWorkFilter> workFilter) {
            this._workFilterList = workFilter;
        }
    }
}