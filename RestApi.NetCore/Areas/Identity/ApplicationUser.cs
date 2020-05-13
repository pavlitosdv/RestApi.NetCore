using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.NetCore.Areas.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Please enter a valid first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [PersonalData]
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Please enter a valid last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [PersonalData]
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Please enter a address")]
        public string Address { get; set; }
    }
}
