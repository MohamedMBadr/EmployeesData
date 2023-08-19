using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MVCExceedDBContext : IdentityDbContext<ApplicationUser>
    {

        public MVCExceedDBContext(DbContextOptions<MVCExceedDBContext> options) : base(options)
        {
                
        }
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<IdentityUser> users { get; set; }  
        //public DbSet<IdentityRole> roles { get; set; }
    }
}
