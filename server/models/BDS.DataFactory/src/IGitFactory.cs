namespace BDS.DataFactory
{
    using System;
    public interface IGitFactory
    {
        bool PullRepository();
        System.Int32 AddStageChanges();
        bool CommitChanges(string message);
        bool PushRepository();
    }
}
