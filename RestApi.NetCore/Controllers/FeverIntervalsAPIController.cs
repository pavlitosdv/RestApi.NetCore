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
    public class FeverIntervalsAPIController : ControllerBase
    {
        private readonly IFeverIntervalInterface _repo;
        public FeverIntervalsAPIController(IFeverIntervalInterface repo)
        {
            _repo = repo;
        }

                
        /// <summary>
        /// Gets the User's Fever Sessions
        /// </summary>
        /// <param name="id">The parameter Id is string type</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FeverInterval>> GetFeversIntervalByUserId(string id)
        {
            var feverInterval = await _repo.GetFeversIntervalByUserId(id);

            if (feverInterval == null)
            {
                return NotFound();
            }

            return Ok(feverInterval);
        }

        
    }
}
