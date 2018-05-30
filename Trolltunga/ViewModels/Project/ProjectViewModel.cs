using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trolltunga.Models;


namespace Trolltunga.ViewModels.Project
{
    public class ProjectViewModel : Models.Project
    {
        public List<ApplicationUser> AllUsers { get; set; } = new List<ApplicationUser>();

        public List<Task> AllTasks { get; set; } = new List<Task>();
    }
}