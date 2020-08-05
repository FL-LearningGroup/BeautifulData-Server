namespace BDS.CollectData.BDSException
{
    using System;
    using System.Text;
    
    [Serializable]
    public class WorkPipelineException: Exception
    {
        public WorkPipelineException()
        {
            
        }
        public WorkPipelineException(string name, string message)
            :base(String.Format("WorkPipeline Failed: name-{0}, message-{1}", name, message))
        {

        }
    }
}
