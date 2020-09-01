using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Runtime
{
    internal class Pipeline
    {
        private PiplelineScheduleTime _scheduleTime;
        private string _scheduleTimeStr;
        private DateTime _executeStartTime;
        private DateTime _executeEndTime ;
        private DateTime _lastExecuteTime;
        private DateTime _nextExecuteTime;
        private bool _firstExeFlag = true;
        private string _assemblyFullName;
        private string _loadDate;
        private string _unloadDate;
        private string _gcCollectDate;
        private string _assemblyPath;
        //Assemble name as key.
        private string _assemblyKey;
        private PipelineStatus _status = PipelineStatus.Wait;
        private Task _piplineTask = null;
        public string ScheduleTimeFormat { get { return _scheduleTimeStr; } }
        public string AssemblyFullName { get { return _assemblyFullName; } }
        public string LoadDate { get { return _loadDate; } }
        public string UnloadDate { get { return _unloadDate; } }
        public string GCCollectDate { get { return _gcCollectDate; } }
        public string AssemblyPath { get { return _assemblyPath; } }
        public string AssemblyKey { get { return _assemblyKey; } }
        public PipelineStatus Status { get { return _status; } }
        public string ScheduleDateFormat { get { return "yyyy/MM/dd hh:mm:ss"; } }
        public string DateFormat { get { return "yyyy/MM/dd hh:mm:ss.fffffff"; } }
        public string ExecuteStartTime { get { return _executeStartTime.ToString(DateFormat); } }
        public string ExecuteEndTime { get { return _executeEndTime.ToString(DateFormat); } }
        public string LastExecuteTime { get { return _lastExecuteTime.ToString(ScheduleDateFormat); } }
        public string NextExecuteTime { get { return _nextExecuteTime.ToString(ScheduleDateFormat); } }

        public Pipeline(string assemblyKey,string assemblyPath, string scheduleTimeStr)
        {
            _assemblyKey = assemblyKey;
            _assemblyPath = assemblyPath;
            _scheduleTimeStr = scheduleTimeStr;
        }

        private void StartWork(out WeakReference weakRef, ref bool loadAssemblyFlag)
        {
            weakRef = new WeakReference(new object());
            Logger.Info(String.Format("{1}-last execute time {0}", LastExecuteTime, _assemblyKey));
            Logger.Info(String.Format("{1}-next execute time {0}", NextExecuteTime, _assemblyKey));
            SetScheduleTime(_scheduleTimeStr);
            if (_firstExeFlag)
            {
                _nextExecuteTime = Convert.ToDateTime(_scheduleTime.StartTime);
            }
            if (DateTime.Now >= _nextExecuteTime)
            {
                _status = PipelineStatus.Running;
                PipelineAssemblyLoadContext assemblyLoadContext = new PipelineAssemblyLoadContext(_assemblyPath);
                loadAssemblyFlag = true;
                weakRef = new WeakReference(assemblyLoadContext);
                try
                {
                    Assembly assembly = assemblyLoadContext.LoadFromAssemblyPath(_assemblyPath);
                    _loadDate = DateTime.Now.ToString(DateFormat);
                    _assemblyFullName = assembly.FullName;
                    _assemblyPath = assembly.Location;
                    // Check _nextExecuteDate
                    foreach (Type type in assembly.GetExportedTypes())
                    {

                        if (type.Name.Contains("Pipeline_"))
                        {
                            object pipelineInstance = Activator.CreateInstance(type);
                            Logger.Info(String.Format("{0} invoke StartWork Method", _assemblyKey));
                            _executeStartTime = DateTime.Now;
                            var result = type.InvokeMember("StartWork", BindingFlags.InvokeMethod, null, pipelineInstance, null);
                            _executeEndTime = DateTime.Now;
                            _lastExecuteTime = _nextExecuteTime;
                            SetNextExecuteTime();
                            _status = PipelineStatus.Successed;
                            _firstExeFlag = false;
                        }
                    }
                    assemblyLoadContext.Unload();
                    Logger.Info(String.Format("{0} asssembly unloaded.", _assemblyFullName));
                    _unloadDate = DateTime.Now.ToString(DateFormat);
                }
                catch (Exception ex)
                {
                    _status = PipelineStatus.Failed;
                    Logger.Info(String.Format("{1} Invoke or Load assembly failed: {0}", ex.Message, _assemblyFullName));
                }
            }   

        }
        private void SetScheduleTime(string time)
        {
            try
            {
                var dtArray = time.Split('|');
                _scheduleTime = new PiplelineScheduleTime() { StartTime = dtArray[0], Model = dtArray[1], ApartTime = Convert.ToInt32(dtArray[2]) };
            }
            catch(Exception ex)
            {
                throw new Exception(String.Format("Conver faile that Schedule time", ex.Message));
            }
        }
        private void SetNextExecuteTime()
        {
            if (_scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.Y.ToString())
            {
                _nextExecuteTime = _lastExecuteTime.AddMonths(12);
            }
            if (_scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.M.ToString())
            {
                _nextExecuteTime = _lastExecuteTime.AddMonths(_scheduleTime.ApartTime);
            }
            if (_scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.W.ToString())
            {
                _nextExecuteTime = _lastExecuteTime.AddDays(7);
            }
            if (_scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.D.ToString())
            {
                _nextExecuteTime = _lastExecuteTime.AddDays(_scheduleTime.ApartTime);
            }
            if (_scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.HH.ToString())
            {
                _nextExecuteTime = _lastExecuteTime.AddHours(_scheduleTime.ApartTime);
            }
            if (_scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.MM.ToString())
            {
                _nextExecuteTime = _lastExecuteTime.AddMinutes(_scheduleTime.ApartTime);
            }
            if (_scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.SS.ToString())
            {
                _nextExecuteTime = _lastExecuteTime.AddSeconds(_scheduleTime.ApartTime);
            }
        }
        public async Task ExecutePipelineAsync()
        {
            if ((_piplineTask is null) || _piplineTask.IsCompleted)
            {
                _piplineTask = Task.Run(() =>
                {
                    WeakReference weakRef;
                    bool loadAssemblyFlag = false;
                    StartWork(out weakRef, ref loadAssemblyFlag);
                    if (loadAssemblyFlag)
                    {
                        for (int i = 0; weakRef.IsAlive; i++) //&& (i < 10)
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
                        _gcCollectDate = DateTime.Now.ToString(DateFormat);
                        Logger.Info(String.Format("{0} Unloaded asssembly successfully: {1}", _assemblyFullName, !weakRef.IsAlive));
                    }
   
                 });
                await _piplineTask;
             }
            else
            {
                Logger.Info(String.Format("{0} pipelline task running", _assemblyKey));
            }
        }
    }
}
