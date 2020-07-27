namespace BDS.CollectData.Resource {
    using System;
    /// <summary> Defined resource </summary>
    internal class Resource: IResource {
        // Defined resource type.
        private string type;
        // Get resource type.
        public string Type 
        {
            get {
                return this.type;
            }
        }
        // Constructor with no parameters.
        public Resource(string type) 
        {
            this.type = type;
        }

        // Get data and store 
        IResourceData resourceData;

        // Set or replace IResourceData
        void SetOrReplaceResourceData(IResourceData resourceData) 
        {
            this.resourceData = resourceData;
        }

        // Display resource data
        IResourceView resourceView;

        // Set or replace IResourceView;
        void SetOrReplaceResourceView(IResourceView resourceView) {
            this.resourceView = resourceView;
        } 
    }
}