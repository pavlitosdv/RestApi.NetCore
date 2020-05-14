using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestApi.NetCore.Areas.Identity;
using RestApi.NetCore.Models;

namespace RestApi.NetCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RestApi.NetCore.Models.BodyTemperature> BodyTemperature { get; set; }
        public DbSet<RestApi.NetCore.Models.FeverInterval> FeverInterval { get; set; }
    }
}
