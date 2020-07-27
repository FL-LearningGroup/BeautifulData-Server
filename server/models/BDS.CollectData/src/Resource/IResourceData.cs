namespace BDS.CollectData.Resource {
    /// <summary>Interface Resource Data </summary>
    interface IResourceData {
        // Get data of data factory.
        void GetResourceData();
        // Store data to data factory or local.
        void StoreResourceData(string path);
    }
}