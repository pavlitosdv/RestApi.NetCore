using RestApi.NetCore.Areas.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.NetCore.Models
{
    public class FeverInterval
    {
        [Key]
        public int Id { get; set; }

        public float StartedTemperature { get; set; }
        //public int FeverEnd { get; set; }
        [Display(Name = "Fever Starting Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Fever Ending Date")]
        public DateTime? EndDate { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
