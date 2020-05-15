using RestApi.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.NetCore.Interfaces
{
   public interface IBodyTemperaturesIntreface
    {

        Task<IEnumerable<BodyTemperature>> GetAllBodyTemperatures();
        Task<BodyTemperature> GetBodyTemperaturesById(int id);

        //Task<IActionResult> PutBodyTemperature(int id, BodyTemperature bodyTemperature);

        Task<BodyTemperature> AddBodyTemperature(BodyTemperature bodyTemperature);

        //bool SaveAll();

        Task<BodyTemperature> DeleteBodyTemperature(int id);
    }
}
