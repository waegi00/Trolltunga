using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Trolltunga.Models;


namespace Trolltunga.ViewModels.Project
{
    public class ProjectFormViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<string> Participants { get; set; } = new List<string>();

        public ICollection<ApplicationUser> AllUsers { get; set; } = new List<ApplicationUser>();
    }
}