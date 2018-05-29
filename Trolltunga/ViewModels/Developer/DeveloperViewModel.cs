using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trolltunga.Models;

namespace Trolltunga.ViewModels.Developer
{
    public class DeveloperViewModel : Models.Developer
    {
        public List<Project> AllProjects { get; set; } = new List<Project>();
    }
}