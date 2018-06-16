using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Trolltunga.Models
{
    public enum Status
    {
        [Description("To do")]
        Todo = 0,
        [Description("In progress")]
        InProgress = 1,
        [Description("To review")]
        ToReview = 2,
        [Description("Done")]
        Done = 3
    }
}