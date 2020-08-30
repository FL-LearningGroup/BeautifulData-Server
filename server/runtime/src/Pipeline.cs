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
        private static string _time = "AC:Y0:M0:W0:D0:h0:m0:s60";
        private PiplelineScheduleTime _scheduleTime;
        private DateTime _executeStartTime;
        private DateTime _executeEndTime ;
        private DateTime _lastExecuteTime;
        private DateTime _nextExecuteTime;
        private bool _firstExeFlag = true;
        private string _assemblyFullName;
        private string _loadDate;
        private string _unloadDate;
        private string _assemblyPath;
        //Assemble name as key.
        private string _assemblyKey;
        private PipelineStatus _status = PipelineStatus.Wait;
        public string AssemblyFullName { get { return _assemblyFullName; } }
        public string LoadDate { get { return _loadDate; } }
        public string UnloadDate { get { return _unloadDate; } }
        public string AssemblyPath { get { return _assemblyPath; } }
        public string AssemblyKey { get { return _assemblyKey; } }
        public PipelineStatus Status { get { return _status; } }
        public string DateFormat { get { return "yyyy/MM/dd hh:mm:ss.fffffff"; } }
        public string CompareDateFormat { get { return "yyyy:MM:dd:hh:mm:ss"; } }
        public string ExecuteStartTime { get { return _executeStartTime.ToString(DateFormat); } }
        public string ExecuteEndTime { get { return _executeEndTime.ToString(DateFormat); } }
        public string LastExecuteTime { get { return _lastExecuteTime.ToString(DateFormat); } }
        public string NextExecuteTime { get { return _nextExecuteTime.ToString(DateFormat); } }


        public Pipeline(string assemblyKey,string assemblyPath)
        {
            _assemblyKey = assemblyKey;
            _assemblyPath = assemblyPath;
        }

        private void StartWork(out WeakReference weakRef)
        {
            _status = PipelineStatus.Running;
            PipelineAssemblyLoadContext assemblyLoadContext = new PipelineAssemblyLoadContext();
            weakRef = new WeakReference(assemblyLoadContext);
            try
            {
                Assembly assembly = assemblyLoadContext.LoadFromAssemblyPath(_assemblyPath);
                _loadDate = DateTime.Now.ToString(DateFormat);
                _assemblyFullName = assembly.FullName;
                _assemblyPath = assembly.Location;
                foreach (Type type in assembly.GetExportedTypes())
                {
                    if (type.Name.Contains("Pipeline_"))
                    {

                        var pipelineInstance = Activator.CreateInstance(type);
                        _lastExecuteTime = DateTime.Now;
                        _nextExecuteTime = _lastExecuteTime.AddMinutes(2);
                        _executeStartTime = DateTime.Now;
                        Console.WriteLine("{0} invoke startwork", _assemblyKey);
                        //var result = type.InvokeMember("StartWork", BindingFlags.InvokeMethod, null, pipelineInstance, null);
                        _executeEndTime = DateTime.Now;
                        Task.Delay(2000);
                        _status = PipelineStatus.Successed;

                    }
                }
                assemblyLoadContext.Unload();
            }
            catch (Exception e)
            {
                _status = PipelineStatus.Failed;
            }
        }
        private void ExecuteScheduleTime(string time)
        {
            var dtArray = time.Split(':');
            if (dtArray[0].ToUpper() == PiplelineScheduleTimeType.FC.ToString().ToUpper())
            {
                _scheduleTime = new PiplelineScheduleTime(PiplelineScheduleTimeType.FC);
            }
            else if (dtArray[0].ToUpper() == PiplelineScheduleTimeType.EC.ToString().ToUpper())
            {
                _scheduleTime = new PiplelineScheduleTime(PiplelineScheduleTimeType.EC);
            }
            else if (dtArray[0].ToUpper() == PiplelineScheduleTimeType.AC.ToString().ToUpper())
            {
                _scheduleTime = new PiplelineScheduleTime(PiplelineScheduleTimeType.AC);
            }
            else
            {
                throw new Exception("The pipeline schedule model does not match");
            }
            try
            {
                for (System.UInt32 i = 1; i < dtArray.Length; i++)
                {
                    _scheduleTime[i - 1] = Convert.ToUInt32(dtArray[i].Substring(1));
                }
            }
            catch(Exception ex)
            {
                throw new Exception(String.Format("Conver faile that Schedule time to number. {0}", ex.Message));
            }
        }
        private bool CompareExecuteTime()
        {
            DateTime callTime = DateTime.Now;
            string[] compareTimeArray = callTime.ToString(CompareDateFormat).Split(':');
            if (_scheduleTime.Model == PiplelineScheduleTimeType.FC)
            {
                if (_scheduleTime.Week >= 0)
                { 
                    if (_scheduleTime.Week == Convert.ToUInt32(callTime.DayOfWeek))
                    {
                        _lastExecuteTime = callTime;
                        for (uint i = 0; i < compareTimeArray.Length; i++)
                        {
                            if (_scheduleTime[i] > 0)
                            {
                                if (_scheduleTime[i] != Convert.ToUInt32(compareTimeArray[i]))
                                {
                                    return false;
                                }
                            }
                        }
                        _lastExecuteTime = callTime;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    for (uint i = 0; i < compareTimeArray.Length; i++)
                    {
                        if (_scheduleTime[i] > 0)
                        {
                            if (_scheduleTime[i] != Convert.ToUInt32(compareTimeArray[i]))
                            {
                                return false;
                            }
                        }
                    }
                }
                _lastExecuteTime = callTime;
                return true;
            }
            else if (_scheduleTime.Model == PiplelineScheduleTimeType.EC)
            {
                for (uint i = 3; i < compareTimeArray.Length; i++)
                {
                    if (_scheduleTime[i] != Convert.ToUInt32(compareTimeArray[i]))
                    {
                        return false;
                    }
                }
                _lastExecuteTime = callTime;
                if (_scheduleTime.Year > 0)
                {
                    callTime.AddMonths(12);
                }
                if (_scheduleTime.Month > 0)
                {
                    callTime.AddMonths(unchecked((int)_scheduleTime.Month));
                }
                if (_scheduleTime.Week > 0)
                {
                    callTime.AddDays(unchecked((int)_scheduleTime.Month) * 7);
                }
                if (_scheduleTime.Day > 0)
                {
                    callTime.AddDays(unchecked((int)_scheduleTime.Day));
                }
                _nextExecuteTime = callTime;
                return true;

            }
            else if (_scheduleTime.Model == PiplelineScheduleTimeType.AC)
            {
                if (_firstExeFlag)
                {
                    _lastExecuteTime = callTime;
                    if (_scheduleTime.Year > 0)
                    {
                        callTime.AddMonths(12);
                    }
                    if (_scheduleTime.Month > 0)
                    {
                        callTime.AddMonths(unchecked((int)_scheduleTime.Month));
                    }
                    if (_scheduleTime.Week > 0)
                    {
                        callTime.AddDays(unchecked((int)_scheduleTime.Month) * 7);
                    }
                    if (_scheduleTime.Day > 0)
                    {
                        callTime.AddDays(unchecked((int)_scheduleTime.Day));
                    }
                    if (_scheduleTime.Hour > 0)
                    {
                        callTime.AddHours(unchecked((int)_scheduleTime.Hour));
                    }
                    if (_scheduleTime.Minute > 0)
                    {
                        callTime.AddMinutes(unchecked((int)_scheduleTime.Minute));
                    }
                    if (_scheduleTime.Second > 0)
                    {
                        callTime.AddSeconds(unchecked((int)_scheduleTime.Second));
                    }
                    _nextExecuteTime = callTime;
                    return true;
                }
                else
                {
                    if (callTime >= _nextExecuteTime)
                    {
                        _lastExecuteTime = _nextExecuteTime;
                        if (_scheduleTime.Year > 0)
                        {
                            _nextExecuteTime.AddMonths(12);
                        }
                        if (_scheduleTime.Month > 0)
                        {
                            _nextExecuteTime.AddMonths(unchecked((int)_scheduleTime.Month));
                        }
                        if (_scheduleTime.Week > 0)
                        {
                            _nextExecuteTime.AddDays(unchecked((int)_scheduleTime.Month) * 7);
                        }
                        if (_scheduleTime.Day > 0)
                        {
                            _nextExecuteTime.AddDays(unchecked((int)_scheduleTime.Day));
                        }
                        if (_scheduleTime.Hour > 0)
                        {
                            _nextExecuteTime.AddHours(unchecked((int)_scheduleTime.Hour));
                        }
                        if (_scheduleTime.Minute > 0)
                        {
                            _nextExecuteTime.AddMinutes(unchecked((int)_scheduleTime.Minute));
                        }
                        if (_scheduleTime.Second > 0)
                        {
                            _nextExecuteTime.AddSeconds(unchecked((int)_scheduleTime.Second));
                        }
                        return true;
                    }
                }

            }
            else
            {
                return false;
            }
        }
        public async Task ExecutePipelineAsync()
        {
            await Task.Run(() =>
            {
                WeakReference weakRef;
                StartWork(out weakRef);
                for (int i = 0; weakRef.IsAlive; i++) //&& (i < 10)
                {
                    Console.WriteLine("asssembly loading");
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                Console.WriteLine("Unloaded asssembly successfully: {0}", !weakRef.IsAlive);
            });
        }
    }
}
