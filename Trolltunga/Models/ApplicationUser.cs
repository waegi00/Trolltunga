using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Trolltunga.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Project> Projects { get; set; } = new List<Project>();

        public virtual List<Task> Tasks { get; set; } = new List<Task>();

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public virtual ICollection<DirectMessage> DirectMessages { get; set; } = new List<DirectMessage>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}