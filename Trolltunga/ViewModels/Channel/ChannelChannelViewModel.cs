using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trolltunga.Models;

namespace Trolltunga.ViewModels.Channel
{
    public class ChannelChannelViewModel
    {
        public Guid ChannelId { get; set; }

        public string Name { get; set; }

        public Models.Project Project { get; set; }

        public ICollection<Message> Messages { get; set; }

        public string Content { get; set; }
    }
}