using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Trolltunga.Models
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Developer> Developers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public ApplicationDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
    }
}