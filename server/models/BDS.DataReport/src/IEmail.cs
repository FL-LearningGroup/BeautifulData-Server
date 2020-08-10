namespace BDS.DataReport
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BDS.DataReport.model;
    public interface IEmail
    {
        Task<bool> SendEamilAsync(List<ContactPerson> fromPersonList, List<ContactPerson> toPersonList, List<ContactPerson> ccPersonList, string message = null);
        void SendEamil(List<ContactPerson> fromPersonList, List<ContactPerson> toPersonList, List<ContactPerson> ccPersonList, string message = null);

    }
}
