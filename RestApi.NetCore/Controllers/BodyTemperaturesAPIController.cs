using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.NetCore.Data;
using RestApi.NetCore.Interfaces;
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

        private readonly IBodyTemperaturesIntreface _repo;
        private readonly IFeverIntervalInterface _ifeverRepo;

        public BodyTemperaturesAPIController(IBodyTemperaturesIntreface repo, IFeverIntervalInterface ifeverRepo)
        {
            _repo = repo;
            _ifeverRepo = ifeverRepo;
        }

        // GET: api/BodyTemperaturesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodyTemperature>>> GetBodyTemperature()
        {
            var ok = await _repo.GetAllBodyTemperatures();

            return Ok(ok);
        }

        // GET: api/BodyTemperaturesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodyTemperature>> GetBodyTemperature(int id)
        {
            var bodyTemperature = await _repo.GetBodyTemperaturesById(id);

            if (bodyTemperature == null)
            {
                return NotFound();
            }

            return Ok(bodyTemperature);
        }


        #region Pending implementation TO DO IT   PUT ACTION
        //// PUT: api/BodyTemperaturesAPI/5        
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBodyTemperature(int id, BodyTemperature bodyTemperature)
        //{
        //    if (id != bodyTemperature.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(bodyTemperature).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BodyTemperatureExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        //private bool BodyTemperatureExists(int id)
        //{
        //    return _context.BodyTemperatures.Any(e => e.Id == id);
        //}

        #endregion

        // POST: api/BodyTemperaturesAPI        
        [HttpPost]
        public async Task<ActionResult<BodyTemperature>> PostBodyTemperature([FromBody]BodyTemperature bodyTemperature)
        {
            if (bodyTemperature.Temperature < 35 || bodyTemperature.Temperature > 42)
            {
                return BadRequest("Body Temperature limits should be between 35 and 42");
            }

            _ifeverRepo.FeverIntervalMethod(bodyTemperature);

            await _repo.AddBodyTemperature(bodyTemperature);

            return CreatedAtAction("GetBodyTemperature", new { id = bodyTemperature.Id }, bodyTemperature);
        }

        // DELETE: api/BodyTemperaturesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BodyTemperature>> DeleteBodyTemperature(int id)
        {
            var bodyTemperature = await _repo.DeleteBodyTemperature(id);
            if (bodyTemperature == null)
            {
                return NotFound();
            }

            return bodyTemperature;
        }



    }
}
