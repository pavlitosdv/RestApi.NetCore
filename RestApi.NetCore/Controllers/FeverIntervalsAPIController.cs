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
    public class FeverIntervalsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeverIntervalsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FeverIntervalsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeverInterval>>> GetFeverInterval()
        {
            return await _context.FeverInterval.ToListAsync();
        }

        // GET: api/FeverIntervalsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeverInterval>> GetFeverInterval(int id)
        {
            var feverInterval = await _context.FeverInterval.FindAsync(id);

            if (feverInterval == null)
            {
                return NotFound();
            }

            return feverInterval;
        }

        // PUT: api/FeverIntervalsAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeverInterval(int id, FeverInterval feverInterval)
        {
            if (id != feverInterval.Id)
            {
                return BadRequest();
            }

            _context.Entry(feverInterval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeverIntervalExists(id))
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

        // POST: api/FeverIntervalsAPI
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FeverInterval>> PostFeverInterval(FeverInterval feverInterval)
        {
            _context.FeverInterval.Add(feverInterval);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeverInterval", new { id = feverInterval.Id }, feverInterval);
        }

        // DELETE: api/FeverIntervalsAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeverInterval>> DeleteFeverInterval(int id)
        {
            var feverInterval = await _context.FeverInterval.FindAsync(id);
            if (feverInterval == null)
            {
                return NotFound();
            }

            _context.FeverInterval.Remove(feverInterval);
            await _context.SaveChangesAsync();

            return feverInterval;
        }

        private bool FeverIntervalExists(int id)
        {
            return _context.FeverInterval.Any(e => e.Id == id);
        }
    }
}
