﻿namespace BDS.DataReport
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BDS.DataReport.Model;
    public interface IEmailClient
    {
        Task<bool> SendEamilAsync(EmailContext emailContext);
        void SendEamil(EmailContext emailContext);

    }
}