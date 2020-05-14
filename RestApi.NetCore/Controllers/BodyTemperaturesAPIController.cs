using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.NetCore.Data;
using RestApi.NetCore.Models;

namespace RestApi.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyTemperaturesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BodyTemperaturesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BodyTemperaturesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodyTemperature>>> GetBodyTemperature()
        {
            return await _context.BodyTemperatures.ToListAsync();
        }

        // GET: api/BodyTemperaturesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodyTemperature>> GetBodyTemperature(int id)
        {
            var bodyTemperature = await _context.BodyTemperatures.FindAsync(id);

            if (bodyTemperature == null)
            {
                return NotFound();
            }

            return bodyTemperature;
        }

        // PUT: api/BodyTemperaturesAPI/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodyTemperature(int id, BodyTemperature bodyTemperature)
        {
            if (id != bodyTemperature.Id)
            {
                return BadRequest();
            }

            _context.Entry(bodyTemperature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodyTemperatureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BodyTemperaturesAPI        
        [HttpPost]
        public async Task<ActionResult<BodyTemperature>> PostBodyTemperature([FromBody]BodyTemperature bodyTemperature)
        {
            if (bodyTemperature.Temperature < 35 || bodyTemperature.Temperature > 42)
            {
                return BadRequest("Body Temperature limits should be between 35 and 42");
            }

            FeverIntervalMethod(bodyTemperature);

            _context.BodyTemperatures.Add(bodyTemperature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBodyTemperature", new { id = bodyTemperature.Id }, bodyTemperature);
        }

        // DELETE: api/BodyTemperaturesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BodyTemperature>> DeleteBodyTemperature(int id)
        {
            var bodyTemperature = await _context.BodyTemperatures.FindAsync(id);
            if (bodyTemperature == null)
            {
                return NotFound();
            }

            _context.BodyTemperatures.Remove(bodyTemperature);
            await _context.SaveChangesAsync();

            return bodyTemperature;
        }

        private bool BodyTemperatureExists(int id)
        {
            return _context.BodyTemperatures.Any(e => e.Id == id);
        }


        #region Methods for adding Fever Intervals (Temporary here. They Will be Moved into another Folder) 

        private void FeverIntervalMethod(BodyTemperature bodyTemperature)
        {
            FeverInterval fever = GetUserLastFeverInterval(bodyTemperature.UserId);
            if ((int)bodyTemperature.Temperature > 37.5)
            {
                if (fever == null)
                {
                    FeverInterval feverInterval = new FeverInterval();
                    feverInterval.UserId = bodyTemperature.UserId;
                    feverInterval.StartedTemperature = bodyTemperature.Temperature;
                    feverInterval.StartDate = DateTime.Now.Date;
                    //feverInterval.EndDate = null;

                    PostFeverInterval1(feverInterval);
                }
                else if (fever.EndDate != null)
                {
                    FeverInterval feverInterval = new FeverInterval();
                    feverInterval.UserId = bodyTemperature.UserId;
                    feverInterval.StartedTemperature = bodyTemperature.Temperature;
                    feverInterval.StartDate = DateTime.Now.Date;
                    //feverInterval.EndDate = null;

                    PostFeverInterval1(feverInterval);
                }
            }
            else
            {
                if (fever != null && fever.EndDate == null)
                {
                    FeverInterval feverInterval = new FeverInterval();
                    feverInterval.Id = fever.Id;
                    feverInterval.UserId = fever.UserId;
                    feverInterval.StartedTemperature = fever.StartedTemperature;
                    feverInterval.StartDate = fever.StartDate;
                    feverInterval.EndDate = DateTime.Now.Date;
                    PutFeverInterval(fever.Id, feverInterval);
                }

            }

        }

        public FeverInterval GetUserLastFeverInterval(string userId)
        {
            var feverInterval = _context.FeverIntervals.AsNoTracking().AsEnumerable().LastOrDefault(i => i.UserId == userId);

            return feverInterval;
        }


        public void PostFeverInterval1(FeverInterval feverInterval)
        {
            _context.FeverIntervals.Add(feverInterval);
            //await _context.SaveChangesAsync();
        }


        public void PutFeverInterval(int id, FeverInterval feverInterval)
        {
            _context.FeverIntervals.Attach(feverInterval);
            _context.Entry(feverInterval).State = EntityState.Modified;
            // _context.SaveChangesAsync();            

        }

        #endregion
    }
}
