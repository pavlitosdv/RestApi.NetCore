using Microsoft.EntityFrameworkCore;
using RestApi.NetCore.Data;
using RestApi.NetCore.Interfaces;
using RestApi.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.NetCore.Repositories
{
    public class BodyTemperatureRepository :IBodyTemperaturesIntreface
    {

        private readonly ApplicationDbContext _context;

        public BodyTemperatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BodyTemperature>> GetAllBodyTemperatures()
        {
            IEnumerable<BodyTemperature> bodyTemperatures;

            bodyTemperatures = await _context.BodyTemperatures.ToListAsync();

            //.Cast<BodyTemperatureRepository>()

            return bodyTemperatures;
        }

        public async Task<IEnumerable<BodyTemperature>> GetBodyTemperaturesById(string userId)
        {
            var bodyTemperature = await _context.BodyTemperatures.Where(i => i.UserId == userId).ToListAsync();
            
            if (bodyTemperature == null)
            {
                return bodyTemperature;
            }

            return bodyTemperature;
        }

        public async Task<BodyTemperature> AddBodyTemperature(BodyTemperature bodyTemperature)
        {

            _context.BodyTemperatures.Add(bodyTemperature);
            await _context.SaveChangesAsync();
            return bodyTemperature;
        }

        public async Task<BodyTemperature> DeleteBodyTemperature(int id)
        {
            var bodyTemperature = await _context.BodyTemperatures.FindAsync(id);
            if (bodyTemperature == null)
            {
                return bodyTemperature;
            }

            _context.BodyTemperatures.Remove(bodyTemperature);
            await _context.SaveChangesAsync();

            return bodyTemperature;
        }


    }
}
