using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Trolltunga.Models;


namespace Trolltunga.ViewModels.Channel
{
    public class ChannelFormViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}