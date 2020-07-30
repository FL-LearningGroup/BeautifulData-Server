namespace BDS.CollectData
{
    public interface IDataStore
    {
        void select();
        void insert();
        void update();
        void delete();
        
    }
    
}