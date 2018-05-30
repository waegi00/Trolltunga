using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trolltunga.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> Participants { get; set; } = new List<ApplicationUser>();

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}