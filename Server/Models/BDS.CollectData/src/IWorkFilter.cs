namespace BDS.CollectData
{
    using System.Collections.Generic;
    /// <Summary> Work Filter</Summary>
    public interface IWorkFilter
    {
        string TagName { get; set; }
        string TagId { get; set; }
        string TagClass { get; set; }
    }
    
}