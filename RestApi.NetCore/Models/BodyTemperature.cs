using RestApi.NetCore.Areas.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.NetCore.Models
{
    public class BodyTemperature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public float Temperature { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
