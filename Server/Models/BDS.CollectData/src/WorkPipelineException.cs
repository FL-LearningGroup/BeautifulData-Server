namespace BDS.CollectData.BDSException
{
    using System;

    [Serializable]
    public class WorkPipelineException: Exception
    {
        private string _context;
        private string _exMessage;
        public string Conext { get { return _context; } }
        public string ExMessage { get { return _exMessage; } }

        public WorkPipelineException()
        {
            
        }
        public WorkPipelineException(string context, string message)
            :base(String.Format("WorkPipeline Failed: || {0} || {1}", context, message))
        {
            _context = context;
            _exMessage = message;
        }
    }
}
