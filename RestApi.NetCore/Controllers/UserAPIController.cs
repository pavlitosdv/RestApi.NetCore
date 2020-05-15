using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.NetCore.Areas.Identity;

namespace RestApi.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAPIController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
      

        /// <summary>
        /// A user can get his/her details
        /// </summary>
        /// <param name="id">The parameter it is string type</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUserById(string id)
        {
            //var user = await _userManager.FindByIdAsync(userId);/* .Users.FirstOrDefaultAsync(i=>i.Id ==userId);*/
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        /// <summary>
        /// With this Method the user can upadate his/her details in the Database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UserUpdate(string id, [FromBody]ApplicationUser user)
        {

            if (id != user.Id)
            {
                return BadRequest();
            }

            var oldUserData = await _userManager.Users.SingleOrDefaultAsync(i => i.Id == user.Id);

            oldUserData.FirstName = user.FirstName;
            oldUserData.LastName = user.LastName;
            oldUserData.Address = user.Address;                      
            oldUserData.Email = user.Email;

            try
            {
                //await _userManager.SaveChangesAsync();
                await _userManager.UpdateAsync(oldUserData);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        private bool UserExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }



        #region Functionable GetAllUsers Method but currently not needed. It is commended out
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers()
        //{
        //    var user = await _userManager.Users.ToListAsync();

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        #endregion


    }
}