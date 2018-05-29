using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trolltunga.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public State State { get; set; }

        [Required]
        public virtual Developer Developer { get; set; }

        [Required]
        public virtual Project Project { get; set; }
    }
}