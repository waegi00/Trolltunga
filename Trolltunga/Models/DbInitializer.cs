using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Trolltunga.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Users

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var janobob = new ApplicationUser
            {
                UserName = "werrnenj",
                Email = "jan@werren.com"
            };

            var wunter = new ApplicationUser
            {
                UserName = "wegmuellerlu",
                Email = "lukas00@bluewin.ch"
            };

            manager.Create(janobob, "Janobob$18");
            manager.Create(wunter, "Wunter$18");

            #endregion

            #region Projects

            var trolltunga = new Project
            {
                Name = "Trolltunga",
                Description = "This is our fancy project...",
                Participants = { janobob, wunter }
            };

            context.Projects.Add(trolltunga);
            context.SaveChanges();

            #endregion

            base.Seed(context);
        }
    }
}