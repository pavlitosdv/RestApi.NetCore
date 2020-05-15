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
        private readonly IBodyTemperaturesIntreface _repo;
        private readonly IFeverIntervalInterface _ifeverRepo;

        public BodyTemperaturesAPIController(IBodyTemperaturesIntreface repo, IFeverIntervalInterface ifeverRepo)
        {
            _repo = repo;
            _ifeverRepo = ifeverRepo;
        }
       
        /// <summary>
        /// Get By Id method. 
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Gives a record by using the Primary key</returns>
        // GET: api/BodyTemperaturesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodyTemperature>> GetBodyTemperatureByUserId(string userId)
        {
            var bodyTemperature = await _repo.GetBodyTemperaturesById(userId);

            if (bodyTemperature == null)
            {
                return NotFound();
            }

            return Ok(bodyTemperature);
        }


        // POST: api/BodyTemperaturesAPI     
        /// <summary>
        /// Method that adds a new record. Also according to some validation if the Temperature is above 37.5 then 
        /// it creates or updates a record. More spesific. if the added temperature is above 37.5 then 
        /// it checks into the Fever Interval's Table if the user has already a fever session. 
        /// If not it creates a new record and includes the date that the fever session started, 
        /// else if the fever session is lower than 37.5 and there is an open Fever session, 
        /// it will be updated by adding the End Date that the fever session ended.
        /// </summary>
        /// <param name="bodyTemperature"></param>
        /// <returns>creates or updates tables as described into summary</returns>
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

        

        #region Functionable but currently not needed

        // GET: api/BodyTemperaturesAPI
        /// <summary>
        /// Get all method
        /// </summary>
        /// <returns>it gives all the users records of the Body Temperatures Table </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodyTemperature>>> GetBodyTemperature()
        {
            var ok = await _repo.GetAllBodyTemperatures();

            return Ok(ok);
        }

        // DELETE: api/BodyTemperaturesAPI/5
        /// <summary>
        /// Delete Method using the Primary Id Key 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        #endregion



        #region Pending implementation But Currently Not Needed
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

    }
}
