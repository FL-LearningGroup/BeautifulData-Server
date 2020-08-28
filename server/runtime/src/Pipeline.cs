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
        private DateTime _executeStartTime;
        private DateTime _executeEndTime;
        private DateTime _lastExecuteTime;
        private DateTime _nextExecuteTime;
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
        public string ExecuteStartTime { get { return _executeStartTime.ToString(DateFormat); } }
        public string ExecuteEndTime { get { return _executeEndTime.ToString(DateFormat); } }
        public string LastExecuteTime { get { return _lastExecuteTime.ToString(DateFormat); } }
        public string NextExecuteTime { get { return _nextExecuteTime.ToString(DateFormat); } }


        public Pipeline(string assemblyKey,string assemblyPath)
        {
            _assemblyKey = assemblyKey;
            _assemblyPath = assemblyPath;
        }

        public void StartWork(out WeakReference weakRef)
        {

            _status = PipelineStatus.Running;
            PipelineAssemblyLoadContext assemblyLoadContext = new PipelineAssemblyLoadContext(_assemblyPath);
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
                        //var result = type.InvokeMember("StartWork", BindingFlags.InvokeMethod, null, pipelineInstance, null);
                        _executeEndTime = DateTime.Now;
                        Task.Delay(5000);
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
