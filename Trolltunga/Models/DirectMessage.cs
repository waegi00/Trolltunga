using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolltunga.Models
{
    public class DirectMessage : Channel
    {
        public virtual ICollection<ApplicationUser>  ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}