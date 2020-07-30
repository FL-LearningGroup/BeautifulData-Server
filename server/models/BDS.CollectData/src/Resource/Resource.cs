namespace BDS.CollectData {
    using System;
    using System.Collections.Generic;
    /// <summary> Defined resource </summary>
    public abstract class Resource {
        // Defined resource type.
        abstract public string Type {
            get;
            set;
        }
    }
}