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

        public WorkSiteStatus Status {
            get {
                return this._status;
            }
        }
        private string _identifier;

        public string Identifier {
            get {
                return this._identifier;
            }
        }
        
        IWorkSiteInput _inputResource;
        IWorkSiteOutput _outputResource;
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