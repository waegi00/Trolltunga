using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Trolltunga.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Developers

            var janobob = new Developer
            {
                Name = "Janobob"
            };

            var wunter = new Developer
            {
                Name = "Wunter"
            };

            context.Developers.Add(janobob);
            context.Developers.Add(wunter);
            context.SaveChanges();

            #endregion

            #region Projects

            var trolltunga = new Project
            {
                Name = "Trolltunga",
                Developers = { janobob, wunter }
            };

            context.Projects.Add(trolltunga);
            context.SaveChanges();

            #endregion

            #region Tasks

            var initializeProject = new Task
            {
                Name = "Initialize Project",
                Developer = wunter,
                State = State.InProgress,
                Project = trolltunga
            };

            context.Tasks.Add(initializeProject);
            context.SaveChanges();

            #endregion

            base.Seed(context);
        }
    }
}