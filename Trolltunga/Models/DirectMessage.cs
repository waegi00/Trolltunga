using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trolltunga.Models
{
    public class DirectMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<ApplicationUser>  ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}