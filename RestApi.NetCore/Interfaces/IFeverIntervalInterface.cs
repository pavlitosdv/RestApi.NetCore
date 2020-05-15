using RestApi.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.NetCore.Interfaces
{
   public interface IFeverIntervalInterface
    {
        void FeverIntervalMethod(BodyTemperature bodyTemperature);
    }
}
