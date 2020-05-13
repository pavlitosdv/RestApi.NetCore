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
            return await _context.BodyTemperature.ToListAsync();
        }

        // GET: api/BodyTemperaturesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodyTemperature>> GetBodyTemperature(int id)
        {
            var bodyTemperature = await _context.BodyTemperature.FindAsync(id);

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
            _context.BodyTemperature.Add(bodyTemperature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBodyTemperature", new { id = bodyTemperature.Id }, bodyTemperature);
        }

        // DELETE: api/BodyTemperaturesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BodyTemperature>> DeleteBodyTemperature(int id)
        {
            var bodyTemperature = await _context.BodyTemperature.FindAsync(id);
            if (bodyTemperature == null)
            {
                return NotFound();
            }

            _context.BodyTemperature.Remove(bodyTemperature);
            await _context.SaveChangesAsync();

            return bodyTemperature;
        }

        private bool BodyTemperatureExists(int id)
        {
            return _context.BodyTemperature.Any(e => e.Id == id);
        }
    }
}
