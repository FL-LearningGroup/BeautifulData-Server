namespace BDS.CollectData.WorkPipeline.Models {
    /// <Summary> Work Pipeline Status </Summary>
    internal enum WorkPipelineStatus 
    {
        Build,
        Executable,
        Forbid,
        Failed
    }
}