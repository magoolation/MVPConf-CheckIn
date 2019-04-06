using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVPConf.CheckIn.Services
{
    public interface IQRScanService
    {
        Task<string> Scan();
    }
}
