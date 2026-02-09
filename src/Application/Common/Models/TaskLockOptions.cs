using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public sealed class TaskLockOptions
    {
        public int LeaseMinutes { get; set; } = 1;
    }
}
