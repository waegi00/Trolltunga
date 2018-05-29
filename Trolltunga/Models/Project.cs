using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trolltunga.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<Task> Tasks { get; set; } = new List<Task>();

        public virtual List<Developer> Developers { get; set; } = new List<Developer>();
    }
}