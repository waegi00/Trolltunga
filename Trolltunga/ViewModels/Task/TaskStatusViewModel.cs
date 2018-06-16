using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Trolltunga.Models;

namespace Trolltunga.ViewModels.Task
{
    public class TaskStatusViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Old status")]
        public string OldStatus { get; set; }

        [Required, DisplayName("New status")]
        public Status NewStatus { get; set; }
    }
}