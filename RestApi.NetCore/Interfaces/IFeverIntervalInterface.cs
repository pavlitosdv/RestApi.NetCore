using RestApi.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.NetCore.Interfaces
{
    interface IFeverIntervalInterface
    {
        void FeverIntervalMethod(BodyTemperature bodyTemperature);
    }
}
